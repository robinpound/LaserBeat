using UnityEngine;
using Liminal.SDK.VR;
using Liminal.SDK.VR.Input;

public class PlayerInput : MonoBehaviour
{
    [Tooltip("Attach primary and secondary hand game objects located on the VR avatar, here")]
    [SerializeField] private GameObject primaryHand, secondaryHand;
    private LaserGunRayCast rayCast;
    private LaserShotSFX sfx;

    int primaryGunIndex = 1;
    int secondaryGunIndex = 2;

    private void Start()
    {
        rayCast = gameObject.GetComponent<LaserGunRayCast>();
        sfx = gameObject.GetComponent<LaserShotSFX>();
    }

    private void Update() 
    {
        var rightHandInput = GetInput(VRInputDeviceHand.Right); // Primary Hand
        var leftHandInput = GetInput(VRInputDeviceHand.Left); // Secondary Hand

        if (rightHandInput != null)
        {
            if (rightHandInput.GetButtonDown(VRButton.One))
            {
                Debug.Log("Call right hand input");
                rayCast.Fire(primaryHand.transform, primaryGunIndex);
                sfx.PlayShootSound();
            }
            
        }

        if (leftHandInput != null)
        {
            if (leftHandInput.GetButtonDown(VRButton.One))
            {
                //Debug.Log("Call left hand input");
                rayCast.Fire(secondaryHand.transform, secondaryGunIndex);
                sfx.PlayShootSound();
            }
            
        }
    }

    private IVRInputDevice GetInput(VRInputDeviceHand hand)
    {
        var device = VRDevice.Device;
        return hand == VRInputDeviceHand.Left ? device.SecondaryInputDevice : device.PrimaryInputDevice;
    }

    public Transform GetOriginPosPrimary()
    {
        return primaryHand.transform;
    }
    public Transform GetOriginPosSecondary()
    {
        return secondaryHand.transform;
    }

}
