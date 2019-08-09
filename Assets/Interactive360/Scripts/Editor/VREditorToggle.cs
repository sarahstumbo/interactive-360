//This script allows us to easily toggle VR Supported on and off using a top level menu item. This is
//an editor script meaning it modifies how the Unity editor actually works. This script has no effect on the
//game project itself
//REMEMBER: To toggle VR Supported normally, go to:
//		Edit -> Project Settings -> Player -> Other Settings

using UnityEditor;		//Enables the use of editor modifying code

namespace Interactive360
{

    public class VREditorToggle
    {
        const string ONNAME = "VR/Enable VR";       //The name of our Enable menu item
        const string OFFNAME = "VR/Disable VR";     //The name of our Disable menu item


        //This method creates the Enable menu item. When the menu item is clicked, the code
        //inside this method executes
        [MenuItem(ONNAME)]
        static void EnableVR()
        {
            //Turn VR Supported on
            PlayerSettings.virtualRealitySupported = true;
        }

        //This method "validates" the Enable menu item. It is used by the editor to format
        //the menu item for us
        [MenuItem(ONNAME, true)]
        static bool EnableValidate()
        {
            //If VR Supported is enabled, add a checkmark next to this menu item
            Menu.SetChecked(ONNAME, PlayerSettings.virtualRealitySupported);
            //Return the opposite of whether or not VR is supported. Thus, if VR Supported is enabled,
            //this returns "false". The result is that if VR Support is enabled, this menu item is grayed-out
            //and cannot be selected again
            return !PlayerSettings.virtualRealitySupported;
        }

        //This method creates the Disable menu item. When the menu item is clicked, the code
        //inside this method executes
        [MenuItem(OFFNAME)]
        static void DisableVR()
        {
            //Turn VR Supported off
            PlayerSettings.virtualRealitySupported = false;
        }

        //This method "validates" the Disable menu item. It is used by the editor to format
        //the menu item for us
        [MenuItem(OFFNAME, true)]
        static bool DisableValidate()
        {
            //If VR Supported is disabled, add a checkmark next to this menu item
            Menu.SetChecked(OFFNAME, !PlayerSettings.virtualRealitySupported);
            //Return the opposite of whether or not VR is supported. Thus, if VR Supported is disabled,
            //this returns "true". The result is that if VR Support is disabled, this menu item is grayed-out
            //and cannot be selected again
            return PlayerSettings.virtualRealitySupported;
        }
    }
}
