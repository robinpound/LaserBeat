using UnityEngine;

public class FinalTargetDestroySound : MonoBehaviour
{
    [SerializeField] private AudioSource destroy;
    void OnEnable() => LaserGunRayCast.OnFinalTargetHit += PlayDestroySound;
    void OnDisable() => LaserGunRayCast.OnFinalTargetHit -= PlayDestroySound;

    private void PlayDestroySound()
    {
        AudioSource.PlayClipAtPoint(destroy.clip, transform.position);
    }
}
