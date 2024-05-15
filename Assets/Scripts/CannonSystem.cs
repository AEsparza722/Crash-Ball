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

    private void Update()
    {
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
        Cannon currentCannon = cannons[Random.Range(0, cannons.Count)];
        currentCannon.ShootBall();
        yield return new WaitForSeconds(Random.Range(minCooldown, maxCooldown));
        canShoot=true;
    }

    IEnumerator SpawnRotateCannon()
    {
        canSpawnRotateCannon = false;

        
        yield return new WaitForSeconds(Random.Range(25f, 40f)); 

        rotateCannon.gameObject.SetActive(true);
        rotateCannon.canShot = true;
        yield return new WaitForSeconds(8f);
        rotateCannon.gameObject.SetActive(false);

        canSpawnRotateCannon = true;

    }
}
