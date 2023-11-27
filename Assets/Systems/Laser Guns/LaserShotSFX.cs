using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShotSFX : MonoBehaviour
{
    [SerializeField] private AudioSource laser;
    public void PlayShootSound()
    {
        laser.Play();
    }
}
