using System.Collections;
using UnityEngine;

public class LaserMove : MonoBehaviour
{
    [Header("Settings")]
    [Range(0.1f, 2f)][SerializeField] float destroyTimer = .5f;
    [Range(0.1f, 100f)][SerializeField] float speed = 5f;
    private void Start()
    {
        StartCoroutine(Destroy());
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
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
