//This script allows the player to simulate have a Vr HMD by movng the camera with the mouse
//and the position with the keyboard

using UnityEngine;
using System.Collections;

namespace Interactive360.Utils
{

    public class CameraEditorControl : MonoBehaviour
    {
        [SerializeField] bool mouseControl = true;  //Should the mouse control the camera? Use this to easily disable this script
        [SerializeField] float camSpeed = 30f;      //The speed the camera rotates
        [SerializeField] float movementSpeed = 3f;  //The speed the player moves

        Transform vrCamera;                         //Reference to the HMD's position


        void Awake()
        {
            //If this is not the editor (it's a build), if we don't want mouse control, or if VR is enabled then
            //destroy this script and exit
            if (!Application.isEditor || !mouseControl || UnityEngine.XR.XRSettings.enabled)
            {
                Destroy(this);
                return;
            }

            //Otherwise, we want editor control and should lock the cursor
            LockCursor();

            //Get the HMD's transform and find all tracked hand objects in the hierarchy
            vrCamera = GetComponentInChildren<Camera>().transform;


            //Start manaing the player's movement
            StartCoroutine(ManageMovement());
        }

        //Detect mouse movements and move camera accordingly
        IEnumerator ManageMovement()
        {
            while (mouseControl)
            {
                //Get the movement of the mouse
                float horizontal = Input.GetAxis("Mouse X") * camSpeed * Time.deltaTime;
                float vertical = Input.GetAxis("Mouse Y") * camSpeed * Time.deltaTime;

                //Rotate the camera accordingly
                transform.Rotate(0f, horizontal, 0f, Space.World);
                vrCamera.Rotate(-vertical, 0f, 0f, Space.Self);

                //Get the player's body movement from the keyboard
                float moveLR = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
                float moveFB = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
                //Apply the movement
                transform.Translate(moveLR, 0f, moveFB);

                //If the user presses "escape", unlock the cursor
                if (Input.GetButtonDown("Cancel"))
                    UnlockCursor();
                //Exit until the next frame
                yield return null;
            }
        }

        void LockCursor()
        {
            //Lock the cursor to the middle of the screen and then hide it
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void UnlockCursor()
        {
            //Release the cursor and show it
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
