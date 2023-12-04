using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpinTarget : MonoBehaviour
{
    public float rotationSpeed = 10f;

    void start() 
    {
        transform.Rotate(Vector3.forward, Random.Range(0, 360));
    }
    void Update()
    {
        transform.Rotate(new Vector3(0.5f,0.5f,1), rotationSpeed * Time.deltaTime);

    }
}
