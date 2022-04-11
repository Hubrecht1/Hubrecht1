using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;





public class Bird : MonoBehaviour
{
    Vector2 _startPosition;
    [SerializeField] float force;
    [SerializeField] float maxdragDistance = 2;
    Rigidbody2D Rb2D;
    [SerializeField] Sprite angrySprite;
    [SerializeField] Sprite normalSprite;
    [SerializeField] Sprite closedEyeSprite;
    [SerializeField] ParticleSystem PS;
    [SerializeField] CinemachineVirtualCamera CM;
    [SerializeField] ParticleController PC;
    [SerializeField] Crate CR;

    bool reset = false;
    bool hasPlayed = false;
    public bool IsDragging = false;


    // Start is called before the first frame update
    void Start()
    {
        _startPosition = Rb2D.position;
        Rb2D.isKinematic = true;

 

    }
    private void Awake()
    {
        var deadbird = GameObject.Find("dead bird");
        Rb2D = GetComponent<Rigidbody2D>();
        PS = GetComponent<ParticleSystem>();
   
        //CM = GetComponent<CinemachineVirtualCamera>();
        //PC = GetComponent<ParticleController>();
    }

    private void OnMouseDown()
    {
        IsDragging = true;
    }

    private void OnMouseUp()
    {
        IsDragging = false;
        if (Rb2D.velocity.x == 0 && Rb2D.velocity.y == 0)
        {
            //CM.m_Lens.OrthographicSize = 2f;
            var currentPosition = Rb2D.position;
            float distance = Vector2.Distance(currentPosition, _startPosition);
            Vector2 direction = _startPosition - currentPosition;
            direction.Normalize();
            Rb2D.isKinematic = false;
            Rb2D.AddForce(direction * force * distance);
            GetComponent<SpriteRenderer>().sprite = angrySprite;

            if (hasPlayed == false)
            {
                InvokeRepeating("_PlayFeather", 0.2f, 0.5f);
                PS.Play();

                hasPlayed = true;
            }
        }
       


    }
    

    void _PlayFeather()
    {
        PC.PlayFeather();
    }   
    
    private void OnMouseDrag()
    {
      
        if(Rb2D.velocity.x == 0 && Rb2D.velocity.y == 0)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 desiredPosition = mousePosition;


            float distance = Vector2.Distance(desiredPosition, _startPosition);
            if (distance > maxdragDistance)
            {
                Vector2 direction = desiredPosition - _startPosition;
                direction.Normalize();
                desiredPosition = _startPosition + (direction * maxdragDistance);
            }


            Rb2D.position = desiredPosition;

        }

    }




    // Update is called once per frame
    void Update()
    {
 
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        CancelInvoke("startParicleSystem");
        CancelInvoke("_PlayFeather");
        if (reset == false)
        {
           
            StartCoroutine(ResetAfterDelay());
        }
        


    }
    IEnumerator ResetAfterDelay()
    {
        hasPlayed = false;
        reset = true;
        if (GetComponent<SpriteRenderer>().sprite != closedEyeSprite)
        {
            GetComponent<SpriteRenderer>().sprite = normalSprite;
        }
        yield return new WaitForSeconds(1);
        GetComponent<SpriteRenderer>().sprite = closedEyeSprite;

        yield return new WaitForSeconds(1.5f);
        var deadbird = GameObject.Find("dead bird");
        Vector3 deadPosition = transform.position;
        Quaternion deadRotation = transform.rotation;
        Rb2D.isKinematic = true;
        Rb2D.position = _startPosition; 
        Rb2D.velocity = new Vector2(0, 0);
        Rb2D.angularVelocity = 0f;
        transform.eulerAngles = new Vector3(0, 0, 0);
        Instantiate(deadbird, deadPosition, deadRotation);
        yield return new WaitForSeconds(0.5f);
        GetComponent<SpriteRenderer>().sprite = normalSprite;
        reset = false;
    }

}
