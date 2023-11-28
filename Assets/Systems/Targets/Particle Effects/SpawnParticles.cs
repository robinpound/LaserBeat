using UnityEngine;

public class SpawnParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystem;

    void OnEnable() => LaserGunRayCast.OnLaserHit += AcitvateParticleEffect;
    void OnDisable() => LaserGunRayCast.OnLaserHit -= AcitvateParticleEffect;

    private void AcitvateParticleEffect(RaycastHit h)
    {
        Instantiate(particleSystem, h.point, Quaternion.identity);  
    }
}
