using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class Cannon : MonoBehaviour
{
    Transform shootingDirection;
    [SerializeField] GameObject ball;
    public PowerUp powerUp;
    
    
    public event EventHandler OnPowerUpPick;

    
    private void Awake()
    {
       shootingDirection = transform.Find("Barrel");
    }

    private void Update()
    {

    }

    public void ShootBall()
    {
        Quaternion initialRotation = shootingDirection.rotation;
        shootingDirection.Rotate(0, UnityEngine.Random.Range(-20f, 20f), 0); //= Quaternion.Euler(0, UnityEngine.Random.Range(-30f,30f), 0);
        Vector3 shootDirForce = shootingDirection.forward * 5f;
        GameObject tempBall = Instantiate(ball, shootingDirection.position, Quaternion.identity);
        tempBall.GetComponent<Rigidbody>().AddForce(shootDirForce, ForceMode.Impulse);
        shootingDirection.rotation = initialRotation;
    }

    private void OnCollisionEnter(Collision collision)
    {        
        if (collision.gameObject.CompareTag("Player"))
        {           
            if(powerUp != null)
            {
                powerUp.EnablePowerUp(collision.gameObject.GetComponent<PlayerController>());
                OnPowerUpPick?.Invoke(this, EventArgs.Empty);
            }
        }
    }

}
