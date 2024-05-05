using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetPickUp : PowerUp
{
    public Magnet magnet;
    public override void EnablePowerUp(PlayerController player)
    {
        player.GetComponent<Magnet>().enabled = true;       
    }
}
