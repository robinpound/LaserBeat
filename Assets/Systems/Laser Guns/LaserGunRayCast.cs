using UnityEngine;

public class LaserGunRayCast : MonoBehaviour
{
    public delegate void HitAction();
    public static event HitAction OnLaserHit;

    //[SerializeField] private Spawner spawner;

    public void Fire(Transform rayOrigin, int gunIndex)
    {
        RaycastHit hit;
        Ray laserGunRay = new Ray(rayOrigin.position, rayOrigin.forward);
        DebugRayCast(rayOrigin);

        if(Physics.Raycast(laserGunRay, out hit))
        {
            if (hit.collider != null && OnLaserHit != null && 
                hit.collider.gameObject.GetComponent<TargetType>())
            {
                TargetType.TargetMaterial targetType = 
                    hit.collider.gameObject.GetComponent<TargetType>().GetTargetType();

                Debug.Log("shot " + hit.collider.gameObject.name + ". Is of type " +  targetType);

                if (gunIndex == 1 && targetType == TargetType.TargetMaterial.Primary) PrimaryTargetHit(hit);

                if (gunIndex == 2 && targetType == TargetType.TargetMaterial.Secondary) SecondaryTargetHit(hit);
            }
        }
    }
    private void PrimaryTargetHit(RaycastHit hit)
    {
        //spawner.despawnTarget(hit.collider.gameObject);
        //OnLaserHit();
        Debug.Log("We destroyed a primary colour target");
    }
    private void SecondaryTargetHit(RaycastHit hit)
    {
        Debug.Log("We destroyed a secondary colour target");
    }

    private void DebugRayCast(Transform rayOrigin)
    {
        float rayDuration = 3f;
        float rayDistance = 50f;
        Debug.DrawRay(rayOrigin.position, rayOrigin.forward * rayDistance, Color.red, rayDuration);
    }

    
}
