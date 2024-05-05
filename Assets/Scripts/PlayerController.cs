using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float moveX;
    [SerializeField] float playerSpeed;
    public bool hasPowerUp; 
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Magnet();
        }
    }
    void Move()
    {
        moveX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector3(moveX * playerSpeed, 0, 0);
    }

    void Magnet()
    {

    }

}
