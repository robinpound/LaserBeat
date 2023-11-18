using UnityEngine;

public class LaserGunGizmos : MonoBehaviour
{
    private Transform primaryOrigin;
    private Transform secondaryOrigin;
    private void Start()
    {
        primaryOrigin = GetComponent<PlayerInput>().GetOriginPosPrimary(); 
        secondaryOrigin = GetComponent<PlayerInput>().GetOriginPosSecondary(); 
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        float distance = 100f;
        if (primaryOrigin != null) DrawLine(primaryOrigin, distance);
        if (secondaryOrigin !=null) DrawLine(secondaryOrigin, distance);
    }
    private void DrawLine(Transform startPos, float distance)
    {
        Gizmos.DrawLine(startPos.position, startPos.forward * distance);
    }
}
