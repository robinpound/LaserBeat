using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("SpawnRate is the number of targets spawning per second (Targets/s)")]
    [Range(0, 5)][SerializeField] float SpawnRate;
    [Tooltip("A moving target lerps between two locations. 0% means none, 100% means all.")]
    [Range(0, 100)] [SerializeField] float PercentageMoving; // To-DO

    [Header("References")]
    [SerializeField] GameObject Target1;
    [SerializeField] GameObject Target2;
    [SerializeField] TargetArea Area;

    void Start()
    {
        StartCoroutine(StartSpawning());
    }
    private IEnumerator StartSpawning() 
    {
        while (true)
        {
            yield return new WaitForSeconds(SpawnRate);
            spawnRandomTarget();
        }
    }
    private void spawnRandomTarget()
    {
        GameObject newtarget;
        // Colour
        Boolean randomColour = Random.Range(0, 2) == 0;
        if (randomColour) newtarget = Instantiate(Target1, getRandomLocationInArea(), Quaternion.identity);
        else newtarget = Instantiate(Target2, getRandomLocationInArea(), Quaternion.identity);
        // Moving
        Boolean randomMove = Random.Range(0, 100) > PercentageMoving;
        if (randomMove) newtarget.GetComponent<TargetMovement>().SetTargetMoving();
    }
    public Vector3 getRandomLocationInArea() {
        float randomDistance = Random.Range(Area.Dmin, Area.Dmax);
        float randomXAngle = Random.Range(Area.Xmin, Area.Xmax);
        float randomYAngle = Random.Range(Area.Ymin, Area.Ymax);
        Quaternion roation = Quaternion.Euler(-randomYAngle, randomXAngle, 0);
        return transform.position + roation * Vector3.forward * randomDistance;
    }
}