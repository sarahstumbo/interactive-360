//This script handles raycasting from the main camera into the scene. It is also responsible for knowing what the user is
//looking at and telling the interactable objects what the player is doing (looking at them, looking away, clicking them, and 
//releasing them). The script acheives this by simulating a tradition input using a raycast and then sending it to the event system
//as if it were a real input event

using UnityEngine;
using UnityEngine.EventSystems;

namespace Interactive360
{

    [RequireComponent(typeof(VRLineRenderer))]
    public class RaycastInput : MonoBehaviour
    {
        [SerializeField] string oculusPrimaryInputAxis = "SecondaryIndexTrigger";   //The name of the input axis used to determine input for Oculus


        [SerializeField] LayerMask whatIsInteractable;      //The layers that this raycast affects
        [SerializeField] float rayDistance = 20f;           //The distance that the input ray should be cast
        [SerializeField] bool drawDebugLine;                //Should we draw a debug ray?
        [SerializeField] VRLineRenderer line;               //A reference to the VR Line Renderer component
        [SerializeField] float defaultLineDistance = 10f;   //The distance in front of the player that the line ends if nothing is highlighted

        Ray ray;                                            //A container for the ray
        RaycastHit rayHit;                                  //The results of a raycast
        PointerEventData eventData;                         //The data for our simulated events


        void Reset()
        {
            //Set the Layer Mask to interact with everything but the Ignore Raycast layer using bitwise operations
            whatIsInteractable = ~(1 << LayerMask.NameToLayer("Ignore Raycast"));
            line = GetComponent<VRLineRenderer>();
        }

        void Start()
        {
            //Give the line renderer 2 vertices
            line.SetVertexCount(2);

            //Generate a new event data container
            eventData = new PointerEventData(EventSystem.current);
            eventData.pointerId = 0;

            //If VR is enabled, set the event position to the center of the HMD. Otherwise set it to the center of the Game window
            if (UnityEngine.XR.XRSettings.enabled)
                eventData.position = new Vector2(UnityEngine.XR.XRSettings.eyeTextureWidth / 2f, UnityEngine.XR.XRSettings.eyeTextureHeight / 2f);
            else
                eventData.position = new Vector2(Screen.width / 2f, Screen.height / 2f);

            //Set the press position to the same as the event data position
            eventData.pressPosition = eventData.position;
        }

        void Update()
        {
            //Every frame we look for interactables and check for hardware inputs
            LookForInteractables();
            CheckInput();
        }


        void LookForInteractables()
        {
            //Generate a new ray at our input object facing forward
            ray = new Ray(transform.position, transform.forward);

            //If we want, draw a debug line in the scene view
            if (drawDebugLine)
                Debug.DrawLine(transform.position, transform.position + transform.forward * rayDistance, Color.red);

            //Cast the ray
            Physics.Raycast(ray, out rayHit, rayDistance, whatIsInteractable);

            //Draw the ray so that the player can see it
            DrawLine();

            //We didn't hit anything
            if (rayHit.transform == null)
            {
                //Look away from anything we were previously looking at
                LookAway();
                return;
            }

            //We are looking at something, so record its data
            eventData.pointerCurrentRaycast = ConvertRaycastHitToRaycastResult(rayHit);

            //If we are looking at the same object that we were looking at, we don't need to do anything and can exit
            if (eventData.pointerEnter == rayHit.transform.gameObject)
                return;

            //Otherwise we are looking at something new and should look away from the old object
            LookAway();

            //Record this data and tell the object that we are pointing at them (OnPointerEnter)
            eventData.pointerEnter = rayHit.transform.gameObject;
            ExecuteEvents.ExecuteHierarchy(eventData.pointerEnter, eventData, ExecuteEvents.pointerEnterHandler);
        }

        void CheckInput()
        {
            //If we press the primary input axis...
            if (Input.GetAxis(oculusPrimaryInputAxis) >= .1 && eventData.pointerEnter != null)
            {
                //...tell the object that we have pressed it (OnPointerDown)
                eventData.pointerPressRaycast = eventData.pointerCurrentRaycast;
                eventData.pointerPress = ExecuteEvents.ExecuteHierarchy(eventData.pointerEnter, eventData, ExecuteEvents.pointerDownHandler);
            }
            //Otherwise, if we just released the primary input axis...
            else if (Input.GetAxis(oculusPrimaryInputAxis) == 0)
            {
                //...tell the object than we have stopped pressing it (OnPointerUp)
                if (eventData.pointerPress != null)
                    ExecuteEvents.ExecuteHierarchy(eventData.pointerPress, eventData, ExecuteEvents.pointerUpHandler);

                //...finally, if we pressed and released the same object, then we have clicked it (OnPointerClick)
                if (eventData.pointerPress == eventData.pointerEnter)
                    ExecuteEvents.ExecuteHierarchy(eventData.pointerEnter, eventData, ExecuteEvents.pointerClickHandler);

                eventData.pointerPress = null;
            }
        }

        void LookAway()
        {
            //If we are currently looking at something, stop looking at it and tell the object (OnPointerExit)
            if (eventData.pointerEnter != null)
            {
                ExecuteEvents.ExecuteHierarchy(eventData.pointerEnter, eventData, ExecuteEvents.pointerExitHandler);
                eventData.pointerEnter = null;
            }
        }

        //This method converts a RaycastHit data type that we get from a raycast into a RaycastResult type that is
        //used by the event system
        RaycastResult ConvertRaycastHitToRaycastResult(RaycastHit hit)
        {
            RaycastResult rayResult = new RaycastResult();
            rayResult.gameObject = hit.transform.gameObject;
            rayResult.distance = rayHit.distance;
            rayResult.worldPosition = rayHit.point;
            rayResult.worldNormal = rayHit.normal;

            return rayResult;
        }

        void DrawLine()
        {
            //If we are looking at something, draw the line to it. Otherwise, draw the line ending at the default distance in front of us
            Vector3 pos = rayHit.transform != null ? rayHit.point : ray.GetPoint(defaultLineDistance);
            line.SetPosition(0, ray.origin);
            line.SetPosition(1, pos);
        }
    }
}
