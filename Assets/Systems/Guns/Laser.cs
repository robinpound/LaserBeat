using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [Range(0.1f, 2f)] [SerializeField] float destroyTimer = .5f;
    [Range(0.1f, 100f)] [SerializeField] float speed = 5f;

    private void Start()
    {
        StartCoroutine(Destroy());
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,
            transform.position + Vector3.forward, speed * Time.deltaTime);
    }

    private IEnumerator Destroy()
    {
        while (true)
        {
            yield return new WaitForSeconds(destroyTimer);
            Destroy(gameObject);
        }
    }
}
