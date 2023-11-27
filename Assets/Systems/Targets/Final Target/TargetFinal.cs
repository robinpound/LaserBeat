using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFinal : MonoBehaviour
{
    [SerializeField] float sizestep = 1f;
    [SerializeField] int health = 5;

    public void Spawn() => gameObject.SetActive(true);

    public void HitTarget() 
    {
        health--;
        if (health == 0) Despawn();
        IncreaseSize();
    }
    private void Despawn() => gameObject.SetActive(false);
    private void IncreaseSize() => transform.localScale += new Vector3(sizestep, sizestep, sizestep);
}
