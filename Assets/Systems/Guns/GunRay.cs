using Liminal.SDK.Core;
using Liminal.SDK.VR;
using Liminal.SDK.VR.Input;
using UnityEngine;

public class GunRay : MonoBehaviour
{
    [SerializeField] private Transform leftGunMuzzle;
    [SerializeField] private Transform rightGunMuzzle;

    [SerializeField] private GameObject leftGun;
    [SerializeField] private GameObject rightGun;

    [SerializeField] private GameObject leftController;
    [SerializeField] private GameObject rightController;

    [SerializeField] private GameObject laser;


    // TO DO - Add Shot delay to remove bug where some shots are not registering whilst shooting too fast.
    // [Range(0.1f, 1f)] [SerializeField] private float shotDelay;

    private Animation gunShotLeftAnim;
    private Animation gunShotRightAnim;

    private AudioSource gunShotSound;

    private Vector3 lastRayHitLeft;
    private Vector3 lastRayHitRight;

    private int targetsHit = 0;

    private void Start()
    {
        gunShotLeftAnim = leftGun.GetComponent<Animation>();
        gunShotRightAnim = rightGun.GetComponent<Animation>();
        gunShotSound = leftGun.GetComponent<AudioSource>();    
    }

    private void Update()
    {
        // TO DO: Move input and target hit to seperate scripts

        //Debug.Log("Number of targets hit " + targetsHit);

        var rightHandInput = GetInput(VRInputDeviceHand.Right);
        var leftHandInput = GetInput(VRInputDeviceHand.Left);

        //if (Input.GetMouseButtonDown(1))
        if (rightHandInput != null)
        {
            if (rightHandInput.GetButtonDown(VRButton.One))
            {
                Debug.Log("Right Gun Shot");
                CastRayRightGun();
                Instantiate(laser, rightGunMuzzle.position, Quaternion.identity);
                gunShotSound.Play();
            }
        }

        //if (Input.GetMouseButtonDown(0)
        if (leftHandInput != null)
        {
            if (leftHandInput.GetButtonDown(VRButton.One))
            {
                Debug.Log("Left Gun Shot");
                CastRayLeftGun();
                Instantiate(laser, leftGunMuzzle.position, Quaternion.identity);
                gunShotSound.Play();
            }
        }

        /* Alt input???

        //var avatar = VRAvatar.Active;
        //if (avatar == null) return;

        var rightInput = VRDevice.Device.SecondaryInputDevice;
        if (rightInput == null) {Debug.LogError("rightInput NULL"); return; } 

        var leftInput = VRDevice.Device.PrimaryInputDevice;
        if (leftInput == null) { Debug.LogError("leftInput NULL"); return; }
        */
    }

    private IVRInputDevice GetInput(VRInputDeviceHand hand)
    {
        var device = VRDevice.Device;
        return hand == VRInputDeviceHand.Left ? device.SecondaryInputDevice : device.PrimaryInputDevice;
    }

    private void CastRayLeftGun()
    {
        RaycastHit hit;
        Ray gunRay = new Ray(leftGunMuzzle.position, Vector3.forward);
        Debug.DrawRay(leftGunMuzzle.position, Vector3.forward * 100, Color.red);

        if (Physics.Raycast(gunRay, out hit))
        {
            if (hit.collider != null && 
                hit.collider.gameObject.GetComponent<Target1Tag>())
            {
                lastRayHitLeft = hit.point;
                Debug.Log("Left gun hit " + hit.collider.gameObject.name);
                Destroy(hit.collider.gameObject, .1f);
                targetsHit++;
            }
        }
    }

    private void CastRayRightGun()
    {
        RaycastHit hit;
        Ray gunRay = new Ray(rightGunMuzzle.position, Vector3.forward);
        Debug.DrawRay(rightGunMuzzle.position, Vector3.forward, Color.red);
        if (Physics.Raycast(gunRay, out hit))
        {

            if (hit.collider != null &&
                hit.collider.gameObject.GetComponent<Target2Tag>())
            {
                lastRayHitRight = hit.point;
                Debug.Log("Right gun hit " + hit.collider.gameObject.name);
                Destroy(hit.collider.gameObject, .1f);
                targetsHit ++;
            }
        }
    }

    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        float roRx = rightGun.transform.rotation.x;
        float roRy = rightGun.transform.rotation.y;
        float roRz = rightGun.transform.rotation.z;
        Debug.Log("ro L x = " + roRx + "\n ro L y = " + roRy);

        DrawLine(rightGunMuzzle.position, roRx, roRy, roRz, 100);
    }

    private void DrawLine(Vector3 startPos, float x, float y, float z, float distance)
    {
        Gizmos.DrawLine(startPos, 
            startPos + Quaternion.Euler(x, y, z) * Vector3.forward * distance);
    }
    
}
