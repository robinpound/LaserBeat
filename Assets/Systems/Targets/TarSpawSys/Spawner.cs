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
        [Header("General Settings")]
        public GameObject Target;
        [Range(0, 100)] public int poolSize;
        [Range(0, 5)] public float SpawnDelayMultiplier;

        [Header("Spawnrates")]
        public Vector2[] SpawnBehaviour;
        [SerializeField] public AnimationCurve spawnCurve;
    }

    public SpawnData spawnData;
    private float currentSpawnDelay; 
    private Queue<GameObject> pool;
    private bool isSpawning = false;

    public void Start() 
    {
        populateCurve();
        populatePool();
        StartSpawning();
    }
    private void populateCurve() 
    {
        spawnData.spawnCurve = new AnimationCurve();
        for (int i = 0; i < spawnData.SpawnBehaviour.Length; i++)
        {
            spawnData.spawnCurve.AddKey(spawnData.SpawnBehaviour[i].x, spawnData.SpawnBehaviour[i].y);
        }
        spawnData.spawnCurve.SmoothTangents(3,1);
    }
    private void populatePool() 
    {
        pool = new Queue<GameObject>();
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
            yield return new WaitForSeconds(currentSpawnDelay);
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