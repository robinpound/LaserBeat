using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDespawner : MonoBehaviour
{
    [SerializeField] Spawner TargetSpawningSystem;

    IEnumerator Start()
    {
        
        yield return new WaitForSeconds(5);
        TargetSpawningSystem.despawnTarget(gameObject);
    }
}
