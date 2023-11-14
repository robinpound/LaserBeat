using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public class SpawnData 
    {
        public GameObject Target;
        [Range(0, 100)] public int size;
        [Range(0, 5)] public float SpawnRate;
    }

    public SpawnData spawnData;
    public Queue<GameObject> pool;
    
    private bool isSpawning;

    public void Start() 
    {
        pool = new Queue<GameObject>();
        isSpawning = false;
        populatePool();
        // StartSpawning(); // Delete
    }

    // Spawning Targets
    private void populatePool() 
    {
        for (int i = 0; i < spawnData.size; i++)
        {
            GameObject tar = Instantiate(spawnData.Target, this.transform);
            tar.SetActive(false); 
            pool.Enqueue(tar);
        }
    }

    // Activating Targets
    public void StartSpawning()
    {
        isSpawning = true;
        StartCoroutine(SpawnOnLoop());
    }
    private IEnumerator SpawnOnLoop()
    {
        while (isSpawning)
        {
            yield return new WaitForSeconds(spawnData.SpawnRate);
            spawnRandomTarget();
        }
    }
    private void spawnRandomTarget()
    {
        GameObject newTarget = pool.Dequeue();
        newTarget.SetActive(true);
    }

    // Stop Activating Targets
    public void StopSpawning() => isSpawning = false;
}
