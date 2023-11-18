using UnityEngine;

public class TestTargetSpawn : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private Transform spawn;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Instantiate(target, spawn);
        }
    }
}
