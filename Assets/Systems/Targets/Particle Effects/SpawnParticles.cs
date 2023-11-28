using UnityEngine;

public class SpawnParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem PSEFinal;
    [SerializeField] private ParticleSystem PSEPurple;
    [SerializeField] private ParticleSystem PSEYellow;

    void OnEnable() => LaserGunRayCast.OnLaserHit += AcitvateParticleEffect;
    void OnDisable() => LaserGunRayCast.OnLaserHit -= AcitvateParticleEffect;

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
                Instantiate(PSEPurple, h.point, Quaternion.identity);
                break;
        }
    }
}
