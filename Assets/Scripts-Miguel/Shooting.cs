using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Liminal.SDK.VR.Input;
using Liminal.SDK.VR;

public class Shooting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Fire();

    }

  
    private IVRInputDevice GetInput(VRInputDeviceHand hand)
    {
        var device = VRDevice.Device;

        //Debug.Log("Checking hands..." + hand);
        return hand == VRInputDeviceHand.Left ? device.SecondaryInputDevice : device.PrimaryInputDevice;

    }

    private void Fire()
    {
        var rightHandInput = GetInput(VRInputDeviceHand.Right);
        var leftHandInput = GetInput(VRInputDeviceHand.Left);

        if (rightHandInput.GetButtonDown(VRButton.One))
        {
            //Debug.Log("Lets start shooting");
            RightHandShoot();

        }
        if (leftHandInput.GetButtonDown(VRButton.One))
        {
            //Debug.Log("Lets start shooting");
            LeftHandShoot();

        }

    }
    private void RightHandShoot()
    {
        Debug.Log($"<b> Right hand shooting </b>");
    }

    private void LeftHandShoot()
    {
        Debug.Log($"<b>  Left hand shooting </b>");
    }
}
