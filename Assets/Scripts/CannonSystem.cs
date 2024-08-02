using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class CannonSystem : MonoBehaviour
{
    public List<Cannon> cannons;
    [SerializeField] float minCooldown;
    [SerializeField] float maxCooldown;
    [SerializeField] RotateCannon rotateCannon;
    bool canShoot = true;
    bool canSpawnRotateCannon = true;
    bool isWinGame = false;
    [SerializeField] ParticleSystem rotatingCannonExplosion;

    [SerializeField] Material explosionMaterial;

    float startValueExplosion = 0f;
    float endValueExplosion = 1f;
    float durationExplosion = 1f;
    float currentValueExplosionDark;
    float currentValueExplosionClear;

    private void Awake()
    {
        GameManager.OnWinGame += StopAll;
    }

    private void Update()
    {
        if (isWinGame) return;
        if (canShoot)
        {
            StartCoroutine(launchBall());
        }
        if (canSpawnRotateCannon)
        {
            StartCoroutine(SpawnRotateCannon());
        }       
    }

    IEnumerator launchBall()
    {
        maxCooldown -= 0.1f;
        maxCooldown = Mathf.Clamp(maxCooldown, minCooldown, maxCooldown);
        canShoot = false;
        Cannon currentCannon = cannons[UnityEngine.Random.Range(0, cannons.Count)];
        currentCannon.ShootBall();
        yield return new WaitForSeconds(UnityEngine.Random.Range(minCooldown, maxCooldown));
        canShoot=true;
    }

    IEnumerator SpawnRotateCannon()
    {
        canSpawnRotateCannon = false;

        
        yield return new WaitForSeconds(UnityEngine.Random.Range(25f, 40f));

        rotateCannon.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        rotateCannon.canShot = true;

        yield return new WaitForSeconds(8f);

        rotatingCannonExplosion.Play();
        StartCoroutine(DissolveExplosionClear());
        rotateCannon.gameObject.SetActive(false);

        canSpawnRotateCannon = true;

    }

    IEnumerator DissolveExplosionClear()
    {
        currentValueExplosionClear = 0;
        currentValueExplosionDark = 0;
        explosionMaterial.SetFloat("_DissolveClear", currentValueExplosionClear);
        explosionMaterial.SetFloat("_DissolveDark", currentValueExplosionDark);
        float elapsedTime = 0;

        while(elapsedTime < durationExplosion)
        {
            explosionMaterial.SetFloat("_DissolveClear", currentValueExplosionClear);
            elapsedTime += Time.deltaTime;
            float T = elapsedTime / durationExplosion; //factor de interpolacion
            currentValueExplosionClear = Mathf.Lerp(startValueExplosion, endValueExplosion, T);
            if(elapsedTime >= durationExplosion / 2 && currentValueExplosionDark == startValueExplosion)
            {
                StartCoroutine(DissolveExplosionDark());
            }
            yield return null;
        }
        currentValueExplosionClear = endValueExplosion;
        
    }

    IEnumerator DissolveExplosionDark()
    {
        float elapsedTime = 0;

        while (elapsedTime < durationExplosion)
        {
            explosionMaterial.SetFloat("_DissolveDark", currentValueExplosionDark);
            elapsedTime += Time.deltaTime;
            float T = elapsedTime / durationExplosion; //factor de interpolacion
            currentValueExplosionDark = Mathf.Lerp(startValueExplosion, endValueExplosion, T);
            
            yield return null;
        }
        currentValueExplosionDark = endValueExplosion;
    }

    void StopAll(object sender, EventArgs e)
    {
        StopAllCoroutines();
        rotateCannon.gameObject.SetActive(false);
    }
}
