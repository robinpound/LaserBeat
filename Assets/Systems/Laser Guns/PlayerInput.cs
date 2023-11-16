using UnityEngine;
using Liminal.SDK.VR;
using Liminal.SDK.VR.Input;


public class PlayerInput : MonoBehaviour
{
    private void Update() 
    {
        var rightHandInput = GetInput(VRInputDeviceHand.Right);
        var leftHandInput = GetInput(VRInputDeviceHand.Left);

        if (rightHandInput != null)
        {
            if (rightHandInput.GetButtonDown(VRButton.One))
            {
                Debug.Log("Call right hand input");
            }
        }

        if (leftHandInput != null)
        {
            if (leftHandInput.GetButtonDown(VRButton.One))
            {
                Debug.Log("Call left hand input");
            }
        }
    }

    private IVRInputDevice GetInput(VRInputDeviceHand hand)
    {
        var device = VRDevice.Device;
        return hand == VRInputDeviceHand.Left ? device.SecondaryInputDevice : device.PrimaryInputDevice;
    }

}
