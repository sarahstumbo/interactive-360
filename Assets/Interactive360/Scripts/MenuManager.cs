using UnityEngine;
using UnityEngine.UI;


namespace Interactive360
{

    public class MenuManager : MonoBehaviour
    {

        public Button[] m_buttonsInScene; //A reference to all the buttons in the scene that would load new scenes
        public GameObject m_menu; //A reference to the menu being rendered
        public GameObject m_playButton; //A reference to the button that toggles the video content to play
        public GameObject m_pauseButton; //A reference to the button that toggle the video content to pause

        [SerializeField] string m_oculusMenuToggle = "Button2"; //The name of the oculus button input that will toggle the scene on and off

        private AudioSource m_menuToggleAudio; //Audio clip to play when the menu is closed




        // on Start, bind all buttons to their respective scenes and call DontDestroyOnLoad to keep the Main Menu in every scene
        void Start()
        {
            DontDestroyOnLoad(gameObject);
            BindButtonsToScenes();
            m_menuToggleAudio = GetComponent<AudioSource>();
        }

        //call the checkForInput method once per fram
        void Update()
        {
            checkForInput();
        }

        //if the menu is active, turn it off. If it is inactive, turn it on
        public void toggleMenu()
        {
            m_menu.SetActive(!m_menu.activeInHierarchy);
        }

        //If we detect input, call the toggleMenu method 
        private void checkForInput()
        {
            //check for input from specified Oculus Touch button or the App button on Google Daydream Controller
            if (Input.GetButtonDown(m_oculusMenuToggle) || GvrControllerInput.AppButtonDown)
            {
                toggleMenu();

                //if we have an audio source to play with menu toggle, play it now
                if (m_menuToggleAudio)
                    m_menuToggleAudio.Play();
            }

        }

        //Toggle between showing play and pause button when once is pressed
        public void toggleButton()
        {

            m_pauseButton.SetActive(!m_pauseButton.activeInHierarchy);
            m_playButton.SetActive(!m_playButton.activeInHierarchy);

        }


        // Each button will match up to a respective scene. Button 1 in the Menu Manager will match up to Scene 1 in the Video Manager
        public void BindButtonsToScenes()
        {
            //check to see if there are the same buttons in the menu as scenes to load. if not, return an error

            if (m_buttonsInScene.Length != GameManager.instance.scenesToLoad.Length)
            {
                Debug.Log("Amount of buttons and scenes do not match!");
                return;
            }

            //otherwise bind Button 1-i from Menu Manager, to load Scene 1-i in Video Manager 
            else
            {
                for (int i = 0; i < m_buttonsInScene.Length; i++)
                {
                    string sceneName = GameManager.instance.scenesToLoad[i];
                    m_buttonsInScene[i].onClick.AddListener(() => GameManager.instance.SelectScene(sceneName));

                }
            }

        }
    }
        
    

}

