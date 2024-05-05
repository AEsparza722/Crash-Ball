using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSystem : MonoBehaviour
{
    [SerializeField] PowerUpFactory powerUpFactory;
    [SerializeField] CannonSystem cannons;
    [SerializeField] List<PlayerController> players;
    float cooldown = 3f;
    public bool canGeneratePowerUp = true;
    public bool isPowerUpActive = false;
    

    private void Update()
    {
        if (canGeneratePowerUp && !isPowerUpActive)
        {
            StartCoroutine(GeneratePowerUp());
        }

        //foreach (PlayerController player in players) //
        //{
        //    if (player.hasPowerUp) //
        //    {
        //        isPowerUpActive = true;
        //    }
        //}
    }

    IEnumerator GeneratePowerUp()
    {

        canGeneratePowerUp = false;
        Cannon randomCannon = cannons.cannons[Random.Range(0, cannons.cannons.Count)];
        if (randomCannon.powerUp == null)
        {
            randomCannon.powerUp = powerUpFactory.CreatePowerUp(powerUpFactory.GetRandomPowerUpID(), randomCannon.transform);
        }
        yield return new WaitForSeconds(cooldown);
        canGeneratePowerUp = true;
               
    }
}
