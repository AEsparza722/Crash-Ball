using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float speedIncrease;
    float currentMaxSpeed = 0;
    [SerializeField] ParticleSystem dustParticle;
    [SerializeField] float splashHeight;

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

        if(transform.position.y < -50f) Destroy(gameObject);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.velocity = new Vector3(rb.velocity.x * speedIncrease, 0, rb.velocity.z * speedIncrease);
        }
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            dustParticle.Play();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Water"))            
        {
            GameObject tempSplash = SplashPool.instance.GetPooledSplash();
            tempSplash.transform.position = new Vector3(transform.position.x, splashHeight, transform.position.z);
            tempSplash.SetActive(true);
            StartCoroutine(DeactivateSplash(tempSplash, 1.5f));

            ParticleSystem.MainModule main = dustParticle.main;
            main.startColor = Color.white;
        }
    }

    IEnumerator DeactivateSplash(GameObject splash, float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        splash.SetActive(false);
    }

}