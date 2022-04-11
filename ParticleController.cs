using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] Bird B;
    [SerializeField] ParticleSystem PS2;
    private void Awake()
    {

        //B = GetComponent<Bird>();
        PS2 = GetComponent<ParticleSystem>();
    }   
    public void PlayFeather()
    {
        transform.position = B.transform.position;
        PS2.Play();

    }
}
