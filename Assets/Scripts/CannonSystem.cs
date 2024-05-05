using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonSystem : MonoBehaviour
{
    public List<Cannon> cannons;
    [SerializeField] float minCooldown;
    [SerializeField] float maxCooldown;
    bool canShoot = true;

    private void Update()
    {
        if (canShoot)
        {
            StartCoroutine(launchBall());
        }               
    }


    IEnumerator launchBall()
    {
        maxCooldown -= 0.1f;
        maxCooldown = Mathf.Clamp(maxCooldown, minCooldown, maxCooldown);
        canShoot = false;
        Cannon currentCannon = cannons[Random.Range(0, cannons.Count)];
        currentCannon.ShootBall();
        yield return new WaitForSeconds(Random.Range(minCooldown, maxCooldown));
        canShoot=true;
    }

}
