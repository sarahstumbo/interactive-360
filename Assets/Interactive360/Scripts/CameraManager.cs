using UnityEngine;

namespace Interactive360
{

    public class CameraManager : MonoBehaviour
    {

        [Tooltip("Only needed for Gaze Based Interactions")]
        public GameObject m_cameraUI;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            checkForOtherCameras();
        }


        //This method will check to see if any additional cameras become active. If any cameras not tagged as "Main Camera" are found, 
        //we will turn them off to avoid having multiple cameras running at once.
        private void checkForOtherCameras()
        {
            if (Camera.allCamerasCount > 1)
                foreach (Camera c in Camera.allCameras)
                {
                    if (c.gameObject.CompareTag("MainCamera") == false)
                    {
                        c.gameObject.SetActive(false);
                    }
                }
        }


        //method to turn camera UI on
        public void turnUIOn()
        {
            if (m_cameraUI)
            {
                m_cameraUI.SetActive(true);
            }
        }

        //method to turn camera UI off
        public void turnUIoff()
        {
            if (m_cameraUI)
            {
                m_cameraUI.SetActive(false);
            }

        }


    }
}
