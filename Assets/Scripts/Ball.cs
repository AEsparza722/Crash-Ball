using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float speedIncrease;
    float currentMaxSpeed = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (rb.velocity.magnitude > currentMaxSpeed)
        {
            currentMaxSpeed = rb.velocity.magnitude;            
        }
        rb.velocity = rb.velocity.normalized * currentMaxSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.velocity = new Vector3(rb.velocity.x * speedIncrease, 0, rb.velocity.z * speedIncrease);
    }
}