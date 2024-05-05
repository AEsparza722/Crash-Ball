using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    Transform shootingDirection;
    [SerializeField] GameObject ball;
    public bool hasPowerUp;
    public PowerUp powerUp;
    

    private void Awake()
    {
       shootingDirection = transform.Find("Barrel");
    }

    private void Update()
    {

    }

    public void ShootBall()
    {
        Vector3 shootDirForce = shootingDirection.forward * 5f;
        GameObject tempBall = Instantiate(ball, shootingDirection.position, Quaternion.identity);
        tempBall.GetComponent<Rigidbody>().AddForce(shootDirForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {        
        if (collision.gameObject.CompareTag("Player"))
        {           
            if(powerUp != null)
            {
                powerUp.EnablePowerUp(collision.gameObject.GetComponent<PlayerController>());

                Destroy(powerUp.gameObject);
            }
        }
    }

}
