using UnityEngine;

public class SpawnParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem PSEFinal;
    [SerializeField] private ParticleSystem PSEPurple;
    [SerializeField] private ParticleSystem PSEYellow;
    [SerializeField] private ParticleSystem DespawnParticle;

    void OnEnable()
    {
        LaserGunRayCast.OnLaserHit += AcitvateParticleEffect;
        Spawner.TDA += ActivateDespawnParticleEffect;
    }
    void OnDisable()
    {
        LaserGunRayCast.OnLaserHit -= AcitvateParticleEffect;
        Spawner.TDA -= ActivateDespawnParticleEffect;
    }

    private void AcitvateParticleEffect(RaycastHit h)
    {
        switch (h.collider.gameObject.GetComponent<TargetType>().GetTargetType()) 
        {
            case TargetType.TargetMaterial.Primary:
                Instantiate(PSEYellow, h.point, Quaternion.identity);
                break;
            case TargetType.TargetMaterial.Secondary:
                Instantiate(PSEPurple, h.point, Quaternion.identity);
                break;
            case TargetType.TargetMaterial.Final:
                Instantiate(PSEFinal, h.collider.transform.position, Quaternion.identity);
                break;
        }
    }
    private void ActivateDespawnParticleEffect(Vector3 position) 
    {
        Instantiate(DespawnParticle, position, Quaternion.identity);
    }
}
