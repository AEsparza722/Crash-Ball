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

    [SerializeField] ParticleSystem shootCannonExplosion;
    [SerializeField] Material explosionMaterial;

    float startValueExplosion = 0f;
    float endValueExplosion = 1f;
    float durationExplosion = 1f;
    float currentValueExplosionDark;
    float currentValueExplosionClear;

    IEnumerator dissolveClear;
    IEnumerator dissolveDark;

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
        shootCannonExplosion.Play();
        if (dissolveClear != null && dissolveDark != null)
        {
            StopCoroutine(dissolveClear);
            StopCoroutine(dissolveDark);
        }
        dissolveClear = DissolveExplosionClear();
        StartCoroutine(dissolveClear);
        Quaternion initialRotation = shootingDirection.rotation;
        shootingDirection.Rotate(0, UnityEngine.Random.Range(-20f, 20f), 0); //= Quaternion.Euler(0, UnityEngine.Random.Range(-30f,30f), 0);
        Vector3 shootDirForce = shootingDirection.forward * 20f;
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

    IEnumerator DissolveExplosionClear()
    {
        currentValueExplosionClear = 0;
        currentValueExplosionDark = 0;
        currentValueExplosionClear = endValueExplosion;
        explosionMaterial.SetFloat("_DissolveClear", currentValueExplosionClear);
        explosionMaterial.SetFloat("_DissolveDark", currentValueExplosionDark);
        float elapsedTime = 0;

        while (elapsedTime < durationExplosion)
        {
            explosionMaterial.SetFloat("_DissolveClear", currentValueExplosionClear);
            elapsedTime += Time.deltaTime;
            float T = elapsedTime / durationExplosion; //factor de interpolacion
            currentValueExplosionClear = Mathf.Lerp(startValueExplosion, endValueExplosion, T);
            if (elapsedTime >= durationExplosion / 2 && currentValueExplosionDark == startValueExplosion)
            {
                dissolveDark = DissolveExplosionDark();
                StartCoroutine(dissolveDark);
            }
            yield return null;
        }
        

    }

    IEnumerator DissolveExplosionDark()
    {
        float elapsedTime = 0;
        currentValueExplosionDark = endValueExplosion;

        while (elapsedTime < durationExplosion)
        {
            explosionMaterial.SetFloat("_DissolveDark", currentValueExplosionDark);
            elapsedTime += Time.deltaTime;
            float T = elapsedTime / durationExplosion; //factor de interpolacion
            currentValueExplosionDark = Mathf.Lerp(startValueExplosion, endValueExplosion, T);

            yield return null;
        }
        
    }

}
