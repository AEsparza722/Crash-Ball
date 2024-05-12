using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSystem : MonoBehaviour
{
    [SerializeField] PowerUpFactory powerUpFactory;
    [SerializeField] CannonSystem cannons;
    public bool canGeneratePowerUp = true;
    public bool isPowerUpActive = false;


    private void Awake()
    {
        foreach (Cannon cannon in cannons.cannons)
        {
            cannon.OnPowerUpPick += PowerUpPicked;
        }
    }
    private void Update()
    {
        if (canGeneratePowerUp && !CannonHasPowerUp() && !isPowerUpActive)
        {
            StartCoroutine(GeneratePowerUp());
        }
    }

    IEnumerator GeneratePowerUp()
    {
        canGeneratePowerUp = false;
        Cannon randomCannon = cannons.cannons[UnityEngine.Random.Range(0, cannons.cannons.Count)];
        if (randomCannon.powerUp == null)
        {
            randomCannon.powerUp = powerUpFactory.CreatePowerUp(powerUpFactory.GetRandomPowerUpID(), randomCannon.transform);
        }
        yield return new WaitForSeconds(UnityEngine.Random.Range(20f, 30f)); //Espera para aparecer mas powerups
        canGeneratePowerUp = true;
    }

    bool CannonHasPowerUp()
    {
        foreach(Cannon cannon in cannons.cannons)
        {
            if(cannon.powerUp != null)
            {
                return true;
            }
        }
        return false;
    }

    IEnumerator WaitForPowerUp()
    {
        isPowerUpActive = true;
        yield return new WaitForSeconds(10f); //Duracion del powerup
        isPowerUpActive = false;
    }

    void PowerUpPicked(object sender, EventArgs eventArgs)
    {
        StartCoroutine(WaitForPowerUp());
    }
}
