using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace Interactive360
{

    [InitializeOnLoad]
    internal class VRInputManagerAssetGenerator
    {
        static List<AxisPreset> axisPresets = new List<AxisPreset>();

        static VRInputManagerAssetGenerator()
        {
            if (!CheckAxisPresets())
            {
                if (EditorUtility.DisplayDialog(
                    "Setup InputManager",
                    "Must patch InputManager settings for VR support for input system prototype.",
                    "Apply", "Cancel"))
                {
                    ApplyAxisPresets();
                }
            }
        }

        static bool CheckAxisPresets()
        {
            SetupAxisPresets();

            var axisArray = GetInputManagerAxisArray();

            if (axisArray.arraySize != axisPresets.Count)
                return false;

            for (int i = 0; i < axisPresets.Count; i++)
            {
                var axisEntry = axisArray.GetArrayElementAtIndex(i);
                if (!axisPresets[i].EqualTo(axisEntry))
                    return false;
            }

            return true;
        }


        static void ApplyAxisPresets()
        {
            SetupAxisPresets();

            var inputManagerAsset = AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0];
            var serializedObject = new SerializedObject(inputManagerAsset);
            var axisArray = serializedObject.FindProperty("m_Axes");

            axisArray.arraySize = axisPresets.Count;
            serializedObject.ApplyModifiedProperties();

            for (int i = 0; i < axisPresets.Count; i++)
            {
                var axisEntry = axisArray.GetArrayElementAtIndex(i);
                axisPresets[i].ApplyTo(ref axisEntry);
            }

            serializedObject.ApplyModifiedProperties();

            AssetDatabase.Refresh();
        }


        static void SetupAxisPresets()
        {
            axisPresets.Clear();
            CreateRequiredAxisPresets();
            ImportExistingAxisPresets();
            CreateCompatibilityAxisPresets();
        }


        static void CreateRequiredAxisPresets()
        {
            axisPresets.Add(new AxisPreset
            {
                name = "Button1",
                positiveButton = "joystick button 0",
                gravity = 1000f,
                sensitivity = 1000f,
                type = 0
            });
            axisPresets.Add(new AxisPreset
            {
                name = "TouchButton1",
                positiveButton = "joystick button 10",
                gravity = 1000f,
                sensitivity = 1000f,
                type = 0
            });
            axisPresets.Add(new AxisPreset
            {
                name = "Button2",
                positiveButton = "joystick button 1",
                gravity = 1000f,
                sensitivity = 1000f,
                type = 0
            });
            axisPresets.Add(new AxisPreset
            {
                name = "TouchButton2",
                positiveButton = "joystick button 11",
                gravity = 1000f,
                sensitivity = 1000f,
                type = 0
            });
            axisPresets.Add(new AxisPreset
            {
                name = "Button3",
                positiveButton = "joystick button 2",
                gravity = 1000f,
                sensitivity = 1000f,
                type = 0
            });
            axisPresets.Add(new AxisPreset
            {
                name = "TouchButton3",
                positiveButton = "joystick button 12",
                gravity = 1000f,
                sensitivity = 1000f,
                type = 0
            });
            axisPresets.Add(new AxisPreset
            {
                name = "Button4",
                positiveButton = "joystick button 3",
                gravity = 1000f,
                sensitivity = 1000f,
                type = 0
            });
            axisPresets.Add(new AxisPreset
            {
                name = "TouchButton4",
                positiveButton = "joystick button 13",
                gravity = 1000f,
                sensitivity = 1000f,
                type = 0
            });
            axisPresets.Add(new AxisPreset
            {
                name = "Start",
                positiveButton = "joystick button 7",
                gravity = 1000f,
                sensitivity = 1000f,
                type = 0
            });
            axisPresets.Add(new AxisPreset
            {
                name = "PrimaryThumbstick",
                positiveButton = "joystick button 8",
                gravity = 0f,
                deadZone = 0f,
                sensitivity = 0.1f,
                type = 0
            });
            axisPresets.Add(new AxisPreset
            {
                name = "TouchPrimaryThumbstick",
                positiveButton = "joystick button 16",
                gravity = 0f,
                deadZone = 0f,
                sensitivity = 0.1f,
                type = 0
            });
            axisPresets.Add(new AxisPreset
            {
                name = "NearTouchPrimaryThumbstick",
                deadZone = 0.19f,
                type = 2,
                axis = 14,
                joyNum = 0
            });
            axisPresets.Add(new AxisPreset
            {
                name = "SecondaryThumbstick",
                positiveButton = "joystick button 9",
                gravity = 0f,
                deadZone = 0f,
                sensitivity = 0.1f,
                type = 0
            });
            axisPresets.Add(new AxisPreset
            {
                name = "TouchSecondaryThumbstick",
                positiveButton = "joystick button 17",
                gravity = 0f,
                deadZone = 0f,
                sensitivity = 0.1f,
                type = 0
            });
            axisPresets.Add(new AxisPreset
            {
                name = "NearTouchSecondaryThumbstick",
                deadZone = 0.19f,
                type = 2,
                axis = 15,
                joyNum = 0
            });
            axisPresets.Add(new AxisPreset
            {
                name = "TouchPrimaryThumbrest",
                positiveButton = "joystick button 18",
                gravity = 0f,
                deadZone = 0f,
                sensitivity = 0.1f,
                type = 0
            });
            axisPresets.Add(new AxisPreset
            {
                name = "NearTouchPrimaryThumbrest",
                deadZone = 0.19f,
                type = 2,
                axis = 14,
                joyNum = 0
            });
            axisPresets.Add(new AxisPreset
            {
                name = "TouchSecondaryThumbrest",
                positiveButton = "joystick button 19",
                gravity = 0f,
                deadZone = 0f,
                sensitivity = 0.1f,
                type = 0
            });
            axisPresets.Add(new AxisPreset
            {
                name = "NearTouchSecondaryThumbrest",
                deadZone = 0.19f,
                type = 2,
                axis = 15,
                joyNum = 0
            });
            axisPresets.Add(new AxisPreset
            {
                name = "PrimaryIndexTrigger",
                deadZone = 0.19f,
                type = 2,
                axis = 8,
                joyNum = 0
            });
            axisPresets.Add(new AxisPreset
            {
                name = "TouchPrimaryIndexTrigger",
                positiveButton = "joystick button 14",
                gravity = 0f,
                deadZone = 0f,
                sensitivity = 0.1f,
                type = 0
            });
            axisPresets.Add(new AxisPreset
            {
                name = "NearTouchPrimaryIndexTrigger",
                deadZone = 0.19f,
                type = 2,
                axis = 12,
                joyNum = 0
            });
            axisPresets.Add(new AxisPreset
            {
                name = "SecondaryIndexTrigger",
                deadZone = 0.19f,
                type = 2,
                axis = 9,
                joyNum = 0
            });
            axisPresets.Add(new AxisPreset
            {
                name = "TouchSecondaryIndexTrigger",
                positiveButton = "joystick button 15",
                gravity = 0f,
                deadZone = 0f,
                sensitivity = 0.1f,
                type = 0
            });
            axisPresets.Add(new AxisPreset
            {
                name = "NearTouchSecondaryIndexTrigger",
                deadZone = 0.19f,
                type = 2,
                axis = 13,
                joyNum = 0
            });
            axisPresets.Add(new AxisPreset
            {
                name = "PrimaryHandTrigger",
                deadZone = 0.19f,
                type = 2,
                axis = 10,
                joyNum = 0
            });
            axisPresets.Add(new AxisPreset
            {
                name = "SecondaryHandTrigger",
                deadZone = 0.19f,
                type = 2,
                axis = 11,
                joyNum = 0
            });
            axisPresets.Add(new AxisPreset
            {
                name = "PrimaryThumbstickHorizontal",
                deadZone = 0.19f,
                type = 2,
                axis = 0,
                joyNum = 0
            });
            axisPresets.Add(new AxisPreset
            {
                name = "PrimaryThumbstickVertical",
                deadZone = 0.19f,
                type = 2,
                axis = 1,
                joyNum = 0
            });
            axisPresets.Add(new AxisPreset
            {
                name = "SecondaryThumbstickHorizontal",
                deadZone = 0.19f,
                type = 2,
                axis = 3,
                joyNum = 0
            });
            axisPresets.Add(new AxisPreset
            {
                name = "SecondaryThumbstickVertical",
                deadZone = 0.19f,
                type = 2,
                axis = 4,
                joyNum = 0
            });
        }


        static void ImportExistingAxisPresets()
        {
            var axisArray = GetInputManagerAxisArray();
            for (int i = 0; i < axisArray.arraySize; i++)
            {
                var axisEntry = axisArray.GetArrayElementAtIndex(i);
                var axisPreset = new AxisPreset(axisEntry);
                if (!axisPreset.ReservedName)
                {
                    axisPresets.Add(axisPreset);
                }
            }
        }


        static void CreateCompatibilityAxisPresets()
        {
            if (!HasAxisPreset("Mouse ScrollWheel"))
            {
                axisPresets.Add(new AxisPreset("Mouse ScrollWheel", 1, 2, 0.1f));
            }

            if (!HasAxisPreset("Horizontal"))
            {
                axisPresets.Add(new AxisPreset()
                {
                    name = "Horizontal",
                    negativeButton = "left",
                    positiveButton = "right",
                    altNegativeButton = "a",
                    altPositiveButton = "d",
                    gravity = 3.0f,
                    deadZone = 0.001f,
                    sensitivity = 3.0f,
                    snap = true,
                    type = 0,
                    axis = 0,
                    joyNum = 0
                });

                axisPresets.Add(new AxisPreset()
                {
                    name = "Horizontal",
                    gravity = 0.0f,
                    deadZone = 0.19f,
                    sensitivity = 1.0f,
                    type = 2,
                    axis = 0,
                    joyNum = 0
                });
            }

            if (!HasAxisPreset("Vertical"))
            {
                axisPresets.Add(new AxisPreset()
                {
                    name = "Vertical",
                    negativeButton = "down",
                    positiveButton = "up",
                    altNegativeButton = "s",
                    altPositiveButton = "w",
                    gravity = 3.0f,
                    deadZone = 0.001f,
                    sensitivity = 3.0f,
                    snap = true,
                    type = 0,
                    axis = 0,
                    joyNum = 0
                });

                axisPresets.Add(new AxisPreset()
                {
                    name = "Vertical",
                    gravity = 0.0f,
                    deadZone = 0.19f,
                    sensitivity = 1.0f,
                    type = 2,
                    axis = 0,
                    invert = true,
                    joyNum = 0
                });
            }

            if (!HasAxisPreset("Submit"))
            {
                axisPresets.Add(new AxisPreset()
                {
                    name = "Submit",
                    positiveButton = "return",
                    altPositiveButton = "joystick button 0",
                    gravity = 1000.0f,
                    deadZone = 0.001f,
                    sensitivity = 1000.0f,
                    type = 0,
                    axis = 0,
                    joyNum = 0
                });

                axisPresets.Add(new AxisPreset()
                {
                    name = "Submit",
                    positiveButton = "enter",
                    altPositiveButton = "space",
                    gravity = 1000.0f,
                    deadZone = 0.001f,
                    sensitivity = 1000.0f,
                    type = 0,
                    axis = 0,
                    joyNum = 0
                });
            }

            if (!HasAxisPreset("Cancel"))
            {
                axisPresets.Add(new AxisPreset()
                {
                    name = "Cancel",
                    positiveButton = "escape",
                    altPositiveButton = "joystick button 1",
                    gravity = 1000.0f,
                    deadZone = 0.001f,
                    sensitivity = 1000.0f,
                    type = 0,
                    axis = 0,
                    joyNum = 0
                });
            }
        }


        static bool HasAxisPreset(string name)
        {
            for (int i = 0; i < axisPresets.Count; i++)
            {
                if (axisPresets[i].name == name)
                {
                    return true;
                }
            }

            return false;
        }


        static SerializedProperty GetInputManagerAxisArray()
        {
            var inputManagerAsset = AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0];
            var serializedObject = new SerializedObject(inputManagerAsset);
            return serializedObject.FindProperty("m_Axes");
        }


        static SerializedProperty GetChildProperty(SerializedProperty parent, string name)
        {
            SerializedProperty child = parent.Copy();
            child.Next(true);

            do
            {
                if (child.name == name)
                {
                    return child;
                }
            } while (child.Next(false));

            return null;
        }


        internal class AxisPreset
        {
            public string name;
            public string descriptiveName = "";
            public string descriptiveNegativeName = "";
            public string negativeButton = "";
            public string positiveButton = "";
            public string altNegativeButton = "";
            public string altPositiveButton = "";
            public float gravity = 0.0f;
            public float deadZone = 0.001f;
            public float sensitivity = 1.0f;
            public bool snap = false;
            public bool invert = false;
            public int type;
            public int axis;
            public int joyNum = 0;


            public AxisPreset()
            {
            }


            public AxisPreset(SerializedProperty axisPreset)
            {
                this.name = GetChildProperty(axisPreset, "m_Name").stringValue;
                this.descriptiveName = GetChildProperty(axisPreset, "descriptiveName").stringValue;
                this.descriptiveNegativeName = GetChildProperty(axisPreset, "descriptiveNegativeName").stringValue;
                this.negativeButton = GetChildProperty(axisPreset, "negativeButton").stringValue;
                this.positiveButton = GetChildProperty(axisPreset, "positiveButton").stringValue;
                this.altNegativeButton = GetChildProperty(axisPreset, "altNegativeButton").stringValue;
                this.altPositiveButton = GetChildProperty(axisPreset, "altPositiveButton").stringValue;
                this.gravity = GetChildProperty(axisPreset, "gravity").floatValue;
                this.deadZone = GetChildProperty(axisPreset, "dead").floatValue;
                this.sensitivity = GetChildProperty(axisPreset, "sensitivity").floatValue;
                this.snap = GetChildProperty(axisPreset, "snap").boolValue;
                this.invert = GetChildProperty(axisPreset, "invert").boolValue;
                this.type = GetChildProperty(axisPreset, "type").intValue;
                this.axis = GetChildProperty(axisPreset, "axis").intValue;
                this.joyNum = GetChildProperty(axisPreset, "joyNum").intValue;
            }


            public AxisPreset(string name, int type, int axis, float sensitivity)
            {
                this.name = name;
                this.descriptiveName = "";
                this.descriptiveNegativeName = "";
                this.negativeButton = "";
                this.positiveButton = "";
                this.altNegativeButton = "";
                this.altPositiveButton = "";
                this.gravity = 0.0f;
                this.deadZone = 0.001f;
                this.sensitivity = sensitivity;
                this.snap = false;
                this.invert = false;
                this.type = type;
                this.axis = axis;
                this.joyNum = 0;
            }


            public bool ReservedName
            {
                get
                {
                    if (Regex.Match(name, @"^(Touch)?Button\d+$").Success ||
                        Regex.Match(name, @"^((Touch)|(NearTouch))?((Primary)|(Secondary))((Thumbstick)|(IndexTrigger))$").Success ||
                        Regex.Match(name, @"^(Near)?Touch((Primary)|(Secondary))Thumbrest$").Success ||
                        Regex.Match(name, @"^((Primary)|(Secondary))((HandTrigger)|(Thumbstick((Horizontal)|(Vertical))))$").Success ||
                        name == "Start")
                    {
                        return true;
                    }
                    return false;
                }
            }


            public void ApplyTo(ref SerializedProperty axisPreset)
            {
                GetChildProperty(axisPreset, "m_Name").stringValue = name;
                GetChildProperty(axisPreset, "descriptiveName").stringValue = descriptiveName;
                GetChildProperty(axisPreset, "descriptiveNegativeName").stringValue = descriptiveNegativeName;
                GetChildProperty(axisPreset, "negativeButton").stringValue = negativeButton;
                GetChildProperty(axisPreset, "positiveButton").stringValue = positiveButton;
                GetChildProperty(axisPreset, "altNegativeButton").stringValue = altNegativeButton;
                GetChildProperty(axisPreset, "altPositiveButton").stringValue = altPositiveButton;
                GetChildProperty(axisPreset, "gravity").floatValue = gravity;
                GetChildProperty(axisPreset, "dead").floatValue = deadZone;
                GetChildProperty(axisPreset, "sensitivity").floatValue = sensitivity;
                GetChildProperty(axisPreset, "snap").boolValue = snap;
                GetChildProperty(axisPreset, "invert").boolValue = invert;
                GetChildProperty(axisPreset, "type").intValue = type;
                GetChildProperty(axisPreset, "axis").intValue = axis;
                GetChildProperty(axisPreset, "joyNum").intValue = joyNum;
            }


            public bool EqualTo(SerializedProperty axisPreset)
            {
                if (GetChildProperty(axisPreset, "m_Name").stringValue != name)
                    return false;
                if (GetChildProperty(axisPreset, "descriptiveName").stringValue != descriptiveName)
                    return false;
                if (GetChildProperty(axisPreset, "descriptiveNegativeName").stringValue != descriptiveNegativeName)
                    return false;
                if (GetChildProperty(axisPreset, "negativeButton").stringValue != negativeButton)
                    return false;
                if (GetChildProperty(axisPreset, "positiveButton").stringValue != positiveButton)
                    return false;
                if (GetChildProperty(axisPreset, "altNegativeButton").stringValue != altNegativeButton)
                    return false;
                if (GetChildProperty(axisPreset, "altPositiveButton").stringValue != altPositiveButton)
                    return false;
                if (!Mathf.Approximately(GetChildProperty(axisPreset, "gravity").floatValue, gravity))
                    return false;
                if (!Mathf.Approximately(GetChildProperty(axisPreset, "dead").floatValue, deadZone))
                    return false;
                if (!Mathf.Approximately(GetChildProperty(axisPreset, "sensitivity").floatValue, this.sensitivity))
                    return false;
                if (GetChildProperty(axisPreset, "snap").boolValue != snap)
                    return false;
                if (GetChildProperty(axisPreset, "invert").boolValue != invert)
                    return false;
                if (GetChildProperty(axisPreset, "type").intValue != type)
                    return false;
                if (GetChildProperty(axisPreset, "axis").intValue != axis)
                    return false;
                if (GetChildProperty(axisPreset, "joyNum").intValue != joyNum)
                    return false;

                return true;
            }
        }
    }
}
