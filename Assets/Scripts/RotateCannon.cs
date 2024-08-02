using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCannon : MonoBehaviour
{
    Transform shootingDirection;
    [SerializeField] GameObject ball;
    float rotationSpeed;
    public bool canShot = false;
    GameObject ballContainer;

    private void Awake()
    {
        shootingDirection = transform.Find("Barrel");
        ballContainer = GameObject.FindGameObjectWithTag("BallContainer");
    }

    private void Update()
    {
        if (canShot)
        {
            StartCoroutine(ShootBall());
        } 
            Rotate();

    }

    IEnumerator ShootBall()
    {
        canShot = false;
        Quaternion initialRotation = shootingDirection.rotation;
        shootingDirection.Rotate(0, UnityEngine.Random.Range(-20f, 20f), 0); //= Quaternion.Euler(0, UnityEngine.Random.Range(-30f,30f), 0);
        Vector3 shootDirForce = shootingDirection.forward * 10f;
        GameObject tempBall = Instantiate(ball, shootingDirection.position, Quaternion.identity, ballContainer.transform);
        tempBall.GetComponent<Rigidbody>().AddForce(shootDirForce, ForceMode.Impulse);
        shootingDirection.rotation = initialRotation;
        yield return new WaitForSeconds(1f);
        canShot = true;
    }

    void Rotate()
    {
        rotationSpeed = 5;
        transform.Rotate(new Vector3(0, rotationSpeed, 0));
    }

    
    
}
