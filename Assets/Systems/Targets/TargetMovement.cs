using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TargetMovement : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Seconds until the target changes direction")]
    [Range(0, 10)][SerializeField] float SecDirectionChange;
    [Tooltip("Speed of target")]
    [Range(0, 5)][SerializeField] float Speed;

    [Header("Refereces")]
    [SerializeField] Spawner spawner;

    private Vector3 targetPosition;
    public void SetTargetMoving() => StartCoroutine(Move());
    private IEnumerator Move()
    {
        while (true)
        {
            yield return new WaitForSeconds(SecDirectionChange);
            targetPosition = spawner.getRandomLocationInArea();
        }
    }
    void Start()
    {
        targetPosition = transform.position;
    }
    void Update() 
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Speed * Time.deltaTime);
        //transform.position = targetPosition;
    }
}
