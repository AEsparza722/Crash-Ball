using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class PowerUpFactory : MonoBehaviour
{
    [SerializeField] List<PowerUp> PowerUps;
    Dictionary<string, PowerUp> IDtoPowerUp;
    private void Awake()
    {
        IDtoPowerUp = new Dictionary<string, PowerUp>();
        foreach (var powerUp in PowerUps)
        {
            IDtoPowerUp.Add(powerUp.id, powerUp);
        }
    }

    public PowerUp CreatePowerUp(string powerUpID, Transform cannonPos)
    {
        PowerUp powerUp;
        if(!IDtoPowerUp.TryGetValue(powerUpID, out powerUp))
        {
            throw new Exception(powerUpID + " does not exist");               
        }
        return Instantiate(powerUp, cannonPos.position, Quaternion.identity);
    }

    public string GetRandomPowerUpID()
    {
        return PowerUps[UnityEngine.Random.Range(0, PowerUps.Count)].id;
    }
}
