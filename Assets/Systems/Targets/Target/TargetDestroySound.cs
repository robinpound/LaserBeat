using UnityEngine;
public class TargetDestroySound : MonoBehaviour
{
    [SerializeField] private AudioSource destroy;
    void OnEnable() => LaserGunRayCast.OnLaserHit += PlayDestroySound;
    void OnDisable() => LaserGunRayCast.OnLaserHit -= PlayDestroySound;

    private void PlayDestroySound(RaycastHit h)
    {
        AudioSource.PlayClipAtPoint(destroy.clip, h.point);
    }
}
