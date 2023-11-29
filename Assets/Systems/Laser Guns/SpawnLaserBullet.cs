using UnityEngine;

public class SpawnLaserBullet : MonoBehaviour
{
    [Header("Laser Bullet Settings")]
    [Tooltip("Add the prefab secondary laser here, located in Asset > Systems > Laser Guns Folder")]
    [SerializeField] private GameObject primaryLaser;
    [Tooltip("Add the prefab secondary laser here, located in Asset > Systems > Laser Guns Folder")]
    [SerializeField] private GameObject secondaryLaser;
    [Tooltip("Add the game object, primary laser spawn point, to this field. Located in the scifi gun game object under primary hand.")]
    [SerializeField] private Transform primaryGunSpwanPos;
    [Tooltip("Add the game object, secondary laser spawn point, to this field. Located in the scifi gun game object under secondary hand.")]
    [SerializeField] private Transform secondaryGunSpwanPos;

    private int primaryGunIndex = 1;
    private int secondaryGunIndex = 2;

    public void InstantiateLaser(int gunIndex)
    {
        if (gunIndex == primaryGunIndex) Instantiate(primaryLaser, primaryGunSpwanPos);
        else if (gunIndex == secondaryGunIndex) Instantiate(secondaryLaser, secondaryGunSpwanPos);
    }

}
