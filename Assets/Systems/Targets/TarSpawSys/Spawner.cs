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
        [Range(0, 100)] public int poolSize;
        [Tooltip("Ignore the number - just use it as a slider")]
        [Range(4, 5)] public float spawnRate;
    }

    public SpawnData spawnData;
    private Queue<GameObject> pool;
    
    private bool isSpawning;

    public void Start() 
    {
        pool = new Queue<GameObject>();
        isSpawning = false;
        populatePool();
        StartSpawning();
    }
    private void populatePool() 
    {
        for (int i = 0; i < spawnData.poolSize; i++)
        {
            GameObject tar = Instantiate(spawnData.Target, this.transform);
            tar.SetActive(false); 
            pool.Enqueue(tar);
        }
    }
    private void StartSpawning()
    {
        isSpawning = true;
        StartCoroutine(SpawnOnLoop());
    }
    private IEnumerator SpawnOnLoop()
    {
        while (isSpawning)
        {
            float secondsWait = ((spawnData.spawnRate - 2.5f) * -1) + 2.6f; // flip the numbers so 5 is 0 and 0 is 5,
                                                                            //                     4 is 1 and 1 is 4, etc
            Debug.Log(secondsWait);
            yield return new WaitForSeconds(secondsWait);
            spawnRandomTarget();
        }
    }
    private void spawnRandomTarget()
    {
        poolUnderflow(); // if pool is empty, give it more
        GameObject newTarget = pool.Dequeue();
        newTarget.SetActive(true);
    }
    private void poolUnderflow() 
    {
        if (pool.Count == 0)
        {
            spawnData.poolSize += 1;
            GameObject tar = Instantiate(spawnData.Target, this.transform);
            pool.Enqueue(tar);
        }
    }
    // Public variables
    public void despawnTarget(GameObject destroyedTarget) 
    {
        pool.Enqueue(destroyedTarget);
        destroyedTarget.SetActive(false);
    }
    public void StopSpawning() => isSpawning = false;

    public Vector3 getRandomLocationInArea()
    {
        float randomDistance = Random.Range(SpawnerArea.Instance.Dmin, SpawnerArea.Instance.Dmax);
        float randomXAngle = Random.Range(SpawnerArea.Instance.Xmin, SpawnerArea.Instance.Xmax);
        float randomYAngle = Random.Range(SpawnerArea.Instance.Ymin, SpawnerArea.Instance.Ymax);
        Quaternion roation = Quaternion.Euler(-randomYAngle, randomXAngle, 0);
        return transform.position + roation * Vector3.forward * randomDistance;
    }
}