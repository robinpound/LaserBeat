﻿using UnityEngine;

public class LaserGunRayCast : MonoBehaviour
{
    // Event for normal targets
    public delegate void HitAction(RaycastHit hit);
    public static event HitAction OnLaserHit;
    // Event for final target
    public delegate void FinalHitAction();
    public static event FinalHitAction OnFinalTargetHit;

    [SerializeField] private Spawner spawner;
    [SerializeField] private TargetFinal FinalTarget;

    public void Fire(Transform rayOrigin, int gunIndex)
    {
        RaycastHit hit;
        Ray laserGunRay = new Ray(rayOrigin.position, rayOrigin.forward);
        DebugRayCast(rayOrigin);

        if(Physics.Raycast(laserGunRay, out hit))
        {
            if (hit.collider != null && 
                hit.collider.gameObject.GetComponent<TargetType>() != null)
            {
                TargetType.TargetMaterial targetType = 
                    hit.collider.gameObject.GetComponent<TargetType>().GetTargetType();

                //Debug.Log("shot " + hit.collider.gameObject.name + ". Is of type " +  targetType);

                if (gunIndex == 1 && targetType == TargetType.TargetMaterial.Primary) PrimaryTargetHit(hit);

                if (gunIndex == 2 && targetType == TargetType.TargetMaterial.Secondary) SecondaryTargetHit(hit);

                if (targetType == TargetType.TargetMaterial.Final) FinalTargetHit(hit);
            }
        }
    }
    private void PrimaryTargetHit(RaycastHit hit)
    {
        spawner.despawnTarget(hit.collider.gameObject);
        OnLaserHit(hit);
        //Debug.Log("We destroyed a primary colour target");
    }
    private void SecondaryTargetHit(RaycastHit hit)
    {
        spawner.despawnTarget(hit.collider.gameObject);
        OnLaserHit(hit);
        //Debug.Log("We destroyed a secondary colour target");
    }
    private void FinalTargetHit(RaycastHit hit)
    {
        //Debug.Log("We hit the final colour target");
        FinalTarget.HitTarget();
        OnLaserHit(hit);
        OnFinalTargetHit();
    }

    private void DebugRayCast(Transform rayOrigin)
    {
        float rayDuration = 3f;
        float rayDistance = 50f;
        Debug.DrawRay(rayOrigin.position, rayOrigin.forward * rayDistance, Color.red, rayDuration);
    }

    
}
