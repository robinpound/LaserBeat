using UnityEngine;

public class LaserGunRayCast : MonoBehaviour
{
    public delegate void HitAction();
    public static event HitAction OnLaserHit;

    public void Fire(Transform rayOrigin)
    {
        RaycastHit hit;
        Ray laserGunRay = new Ray(rayOrigin.position, rayOrigin.forward);
        float rayDuration = 1f;
        Debug.DrawRay(rayOrigin.position, rayOrigin.forward, Color.red, rayDuration);

        if(Physics.Raycast(laserGunRay, out hit))
        {
            if (hit.collider != null && OnLaserHit != null)
            {
                Debug.Log("Laser from " + rayOrigin.gameObject.name + 
                    " hit " + hit.collider.gameObject.name +   ", call subscibed events!");
                OnLaserHit();
            }
        }
    }
}
