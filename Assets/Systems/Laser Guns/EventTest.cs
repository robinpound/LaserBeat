using UnityEngine;

public class EventTest : MonoBehaviour
{
    void OnEnable() => LaserGunRayCast.OnLaserHit += Test;
    void OnDisable() => LaserGunRayCast.OnLaserHit -= Test;
        
    private void Test(RaycastHit h)
    {
        Debug.Log("We subscribed to the on hit event");
    }
}
