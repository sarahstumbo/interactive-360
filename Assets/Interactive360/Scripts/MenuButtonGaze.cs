using System.Collections;
using UnityEngine;
using Interactive360.Utils;
using UnityEngine.UI;


// This class will invoke an OnClick event for the 
// button as soon as the user gazes over it

namespace Interactive360
{
    [RequireComponent(typeof(VRInteractiveItem))]
    [RequireComponent(typeof(BoxCollider))]

    public class MenuButtonGaze : MonoBehaviour
    {

        private Button m_Button;                         // The button we are going to call onClick for
        private bool isOver = false;                     // Bool value to let us know whether or not the gaze is over the button
        private VRInteractiveItem m_InteractiveItem;     // The interactable object for where the user should look to cause on "onClick" event
        private Image m_Image;                           // The image representing the menu button that we are gazing at


        private void Awake()
        {
            m_Button = GetComponent<Button>();
            m_InteractiveItem = GetComponent<VRInteractiveItem>();
            m_Image = GetComponent<Image>();
        }

        private void OnEnable()
        {
            m_InteractiveItem.OnOver += HandleOver;
            m_InteractiveItem.OnOut += HandleOut;
        }


        private void OnDisable()
        {
            m_InteractiveItem.OnOver -= HandleOver;
            m_InteractiveItem.OnOut -= HandleOut;

        }

        // When the user looks at the rendering of the scene, wait 1 second
        // if we are still over after 1 full second, invoke click
        private IEnumerator WaitAndClick()
        {
            yield return new WaitForSeconds(1);
            if (isOver == true)
                m_Button.onClick.Invoke();
        }

        private void HandleOver()
        {
            isOver = true;
            StartCoroutine(WaitAndClick());

            m_Image.color = Color.cyan;
        }

        private void HandleOut()
        {
            isOver = false;
            m_Image.color = Color.white;
        }



    }
}

