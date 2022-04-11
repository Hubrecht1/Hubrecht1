using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    Rigidbody2D Rb2D;
    [SerializeField] ParticleSystem PS;
    [SerializeField] BoxCollider2D BC2D;
    [SerializeField] SpriteRenderer SR;
    void Awake()
    {

        gameObject.SetActive(true);
        Rb2D = GetComponent<Rigidbody2D>();
        
        



    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.relativeVelocity.magnitude >= 12f )
        {
            StartCoroutine(DestroyThisObject());
            //Debug.Log(collision.relativeVelocity.magnitude);
        }
        

    }

    private IEnumerator DestroyThisObject()
    {
        PS.Play();
        SR.enabled = false;
        BC2D.enabled = false;
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
        
    }

}  
