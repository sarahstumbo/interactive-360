using UnityEngine;

//Add this script to any game object in your starting scene that you want to remain in all other scenes loaded
public class DontDestroyObject : MonoBehaviour
{

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

}
