using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{   
        [Header("Step 1: Distance")]

    [Tooltip("Minimum distance from center.")]
    [Range(0, 300)][SerializeField] public float dmin;

    [Tooltip("Maximum distance from center.")]
    [Range(0, 300)][SerializeField] public float dmax;

        [Header("Step 2: X-Angle")]

    [Tooltip("Minimum X angle.")]
    [Range(-180, 0)][SerializeField] public float xmin;

    [Tooltip("Maximum X angle.")]
    [Range(0, 180)][SerializeField] public float xmax;

        [Header("Step 3: Y-Angle")]

    [Tooltip("Minimum Y angle.")]
    [Range(-90, 0)][SerializeField] public float ymin;

    [Tooltip("Maximum Y angle.")]
    [Range(0, 90)][SerializeField] public float ymax;

        [Header("Sphere Colours")]

    [SerializeField] Color dminColor;
    [SerializeField] Color dmaxColor;

        [Header("Target Settings")]

    [Range(0, 100)] public int poolSize;
    public GameObject Target;
    [SerializeField] public AnimationCurve spawnCurve;
    public GameObject FinalTarget;
    [SerializeField] private float spawnFinalTargetTime;
    private bool HasSpawnedFinal = false;

        // Private 

    private Queue<GameObject> pool;
    private bool isSpawning = false;
    private int targetsInPlayCounter;

    [SerializeField] ParticleSystem PurTarEffect;
    [SerializeField] ParticleSystem YelTarEffect;
    [SerializeField] ParticleSystem FinTarEffect;

    public void Start() 
    {
        populatePool();
        StartSpawning();
    }

    public void Update() 
    {
        if (Time.time > spawnFinalTargetTime && !HasSpawnedFinal)
        {
            HasSpawnedFinal = true;
            FinalTarget.GetComponent<TargetFinal>().Spawn();
        }
    }

    private void populatePool() 
    {
        pool = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject tar = Instantiate(Target, this.transform);
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
            if (targetsInPlayCounter < spawnCurve.Evaluate(Time.time)) 
            {
                spawnRandomTarget();
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void spawnRandomTarget()
    {
        if (pool.Count != 0) // hard limit
        {
            GameObject newTarget = pool.Dequeue();
            newTarget.transform.position = getRandomLocationInArea();
            newTarget.SetActive(true);
            newTarget.GetComponent<TargetMovement>().SetTargetMoving();
            targetsInPlayCounter++;
        }
    }

    // Public methods
    public void despawnTarget(GameObject destroyedTarget) 
    {
        Instantiate(PurTarEffect, destroyedTarget.transform);
        pool.Enqueue(destroyedTarget);
        destroyedTarget.SetActive(false);
        targetsInPlayCounter--;
    }
    public void StopSpawning() => isSpawning = false;
    public Vector3 getRandomLocationInArea()
    {
        float randomDistance = Random.Range(dmin, dmax);
        float randomXAngle = Random.Range(xmin, xmax);
        float randomYAngle = Random.Range(ymin, ymax);
        Quaternion roation = Quaternion.Euler(-randomYAngle, randomXAngle, 0);
        return transform.position + roation * Vector3.forward * randomDistance;
    }

    //GIZMOS
    private void DrawLine(Vector3 centre, float z11, float z12, float z21, float z22, float d1, float d2)
    {
        Gizmos.DrawLine( // TOP RIGHT
            centre + Quaternion.Euler(z11, z12, 0) * Vector3.forward * d1,
            centre + Quaternion.Euler(z21, z22, 0) * Vector3.forward * d2
        );
    }
    private void OnDrawGizmos()
    {
        // SETTINGS
        Vector3 centre = transform.position;
        const float LINELENGTH = 1;
        const float LINESPACE = 3;

        // Distance Representiation
        Gizmos.color = dminColor;
        Gizmos.DrawSphere(transform.position, dmin);
        Gizmos.color = dmaxColor;
        Gizmos.DrawSphere(transform.position, dmax);

        // 4 Lines
        Gizmos.color = Color.cyan;
        DrawLine(centre, -ymax, xmax, -ymax, xmax, dmin, dmax);
        DrawLine(centre, -ymin, xmax, -ymin, xmax, dmin, dmax);
        DrawLine(centre, -ymin, xmin, -ymin, xmin, dmin, dmax);
        DrawLine(centre, -ymax, xmin, -ymax, xmin, dmin, dmax);

        // DOTTED LINES X-AXIS
        Gizmos.color = Color.cyan;
        for (float i = xmin; i < xmax; i += LINESPACE)
        {
            DrawLine(centre, -ymax, i, -ymax, i + LINELENGTH, dmax, dmax);
            DrawLine(centre, -ymin, i, -ymin, i + LINELENGTH, dmax, dmax);
            DrawLine(centre, -ymax, i, -ymax, i + LINELENGTH, dmin, dmin);
            DrawLine(centre, -ymin, i, -ymin, i + LINELENGTH, dmin, dmin);
        }
        // DOTTED LINES Y-AXIS
        Gizmos.color = Color.cyan;
        for (float i = -ymax; i < -ymin; i += LINESPACE)
        {
            DrawLine(centre, i, xmax, i + LINELENGTH, xmax, dmax, dmax);
            DrawLine(centre, i, xmin, i + LINELENGTH, xmin, dmax, dmax);
            DrawLine(centre, i, xmax, i + LINELENGTH, xmax, dmin, dmin);
            DrawLine(centre, i, xmin, i + LINELENGTH, xmin, dmin, dmin);
        }
    }
}