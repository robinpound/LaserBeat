using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("SpawnRate is the number of targets spawning per second (Targets/s)")]

    [Range(0, 5)][SerializeField] float SpawnRate;
    [Header("References")]
    [SerializeField] GameObject Target;

    public void StartSpawning()
    {
        StartCoroutine(SpawnOnLoop());
    }
    private IEnumerator SpawnOnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(SpawnRate);
            spawnRandomTarget();
        }
    }
    private void spawnRandomTarget()
    {
    }
}
