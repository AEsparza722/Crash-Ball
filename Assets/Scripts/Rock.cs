using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    Rigidbody rb;
    bool isCollision = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!isCollision)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y * 1.03f, rb.velocity.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isCollision = true;
        }
        
    }

}
