using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Spawner;
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

    // FILL POOL
    public void Start() 
    {
        spawnData = new SpawnData();
        pool = new Queue<GameObject>();
        
        for (int i = 0; i < pool.Count; i++) 
        {
            GameObject tar = Instantiate(spawnData.Target);
            tar.SetActive(true); //set to false
            pool.Enqueue(tar);
        }
    }
    // SPAWNING 
    public void StartSpawningSequence() => StartCoroutine(SpawnOnLoop());
    private IEnumerator SpawnOnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnData.SpawnRate);
            spawnRandomTarget();
        }
    }
    private void spawnRandomTarget()
    {
        
    }
}
