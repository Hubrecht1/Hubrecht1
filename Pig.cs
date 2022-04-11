using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Pig : MonoBehaviour
{
    [SerializeField] Sprite deadSprite;
    [SerializeField] ParticleSystem PS;
    bool hasDied = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ShouldDieGromCollision(collision) && hasDied == false)
        {
            StartCoroutine(Die());
        }
        
    }
    bool ShouldDieGromCollision(Collision2D collision)
    {
        Bird bird = collision.gameObject.GetComponent<Bird>();
        DeadBird deadbird = collision.gameObject.GetComponent<DeadBird>();
        if (bird != null || deadbird != null)
        {
            return true;
        }
        if (collision.contacts[0].normal.y < -0.5)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    IEnumerator Die()
    {
        GetComponent<SpriteRenderer>().sprite = deadSprite;
        PS.Play();
        hasDied = true;
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false); 

        
    }
}
