using UnityEngine;

namespace Interactive360
{

    public class Fade : MonoBehaviour
    {

        Animator anim; //a reference to the image's animator controller

        //Get reference to animator component
        void Awake()
        {
            anim = GetComponent<Animator>();
        }

        //We will fade in at the start of each scene
        void Start()
        {
            anim.SetTrigger("FadeIn");
        }

        //FadeOutImage will be called by our GameManager when it is time to change scenes
        public void FadeOutImage()
        {
            anim.SetTrigger("FadeOut");
        }
    }
}
