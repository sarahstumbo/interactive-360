using UnityEngine;
using UnityEngine.VR;

namespace Interactive360.Utils
{
    // This class simply insures the head tracking behaves correctly when the application is paused.
    public class VRTrackingReset : MonoBehaviour
    {
        private void OnApplicationPause(bool pauseStatus)
        {
            UnityEngine.XR.InputTracking.Recenter();
        }
    }
}