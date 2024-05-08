using System.Collections;
using System.Collections.Generic;
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
    }
    void Move()
    {
        //moveX = Input.GetAxis("Horizontal");
        //rb.velocity = new Vector3(moveX * playerSpeed, 0, 0);
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
     
}
