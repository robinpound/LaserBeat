using Liminal.Core.Fader;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TargetFinal : MonoBehaviour
{
    [SerializeField] float sizestep = 1f;
    [SerializeField] int health = 5;
    [SerializeField] float sizeThreshold = 5f;
    //[SerializeField] ParticleSystem deathParticles;

    public void Spawn() => gameObject.SetActive(true);

    public void Update()
    {
        IncreaseSize();
        if (transform.localScale.x >= sizeThreshold)
        {
            FadeScreenToBlack();
            Despawn();
        }
    }

    public void HitTarget() 
    {
        health--;
        if (health == 0) DestroyTarget();
    }

    private void DestroyTarget()
    {
        //Instantiate(deathParticles, transform.position, Quaternion.identity);
        Despawn();
    }

    private void FadeScreenToBlack()
    {
        var fader = ScreenFader.Instance;
        fader.FadeTo(Color.black, 2f);
    }
    
    private void Despawn() => gameObject.SetActive(false);
    private void IncreaseSize() => transform.localScale += new Vector3(sizestep, sizestep, sizestep) * Time.deltaTime;

    
}
