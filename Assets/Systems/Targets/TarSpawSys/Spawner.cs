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
    
    private bool spawning;

    public void Start() 
    {
        pool = new Queue<GameObject>();
        spawning = false;
        populatePool();
        // StartSpawning(); // Delete
    }
    private void populatePool() 
    {
        for (int i = 0; i < spawnData.size; i++)
        {
            GameObject tar = Instantiate(spawnData.Target, this.transform);
            tar.SetActive(false); 
            pool.Enqueue(tar);
        }
    }
    public void StartSpawning()
    {
        spawning = true;
        StartCoroutine(SpawnOnLoop());
    }
    public void StopSpawning() => spawning = false;
    private IEnumerator SpawnOnLoop()
    {
        while (spawning)
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
}
