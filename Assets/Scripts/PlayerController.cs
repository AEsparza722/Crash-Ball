using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] Players player;
    float moveX;
    [SerializeField] float playerSpeed;
    public bool hasPowerUp; 
    Rigidbody rb;
    [SerializeField]public int lives;
    [SerializeField] GameObject barrier;


    private void Awake()
    {        
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        lives = 10;
    }

    private void Update()
    {
        Move();
        Sprint();

        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            KickBall();
        }

    }
    void Move()
    {
        float moveDirection = Input.GetAxis("Horizontal");
        Vector3 movement = transform.right * moveDirection * playerSpeed;
        rb.velocity = movement;
    }

    public void TakeGoal()
    {
        lives--;

        if(lives == 0)
        {
            barrier.SetActive(true);
            Destroy(gameObject);
        }
    }

    void KickBall()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 2.5f);

        if (colliders.Length != 0)
        {
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Ball"))
                {
                    Vector3 forceDir = collider.transform.position - transform.position;
                    collider.GetComponent<Rigidbody>().AddForce(forceDir.normalized * 15f, ForceMode.Impulse);
                }
            }
        }
    }

    void Sprint()
    {        
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            playerSpeed *= 2f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            playerSpeed /= 2f;
        }
    }

    void GrabBall()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 2f);

        if (colliders.Length != 0)
        {
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Ball"))
                {
                   FixedJoint ballJoint = collider.AddComponent<FixedJoint>(); //Vamos a usar joints para sostener las pelotas
                }
            }
        }
    }

}
