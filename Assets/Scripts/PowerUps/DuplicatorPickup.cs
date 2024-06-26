using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuplicatorPickup : PowerUp
{
    //public Duplicator duplicator;

    public override void EnablePowerUp(PlayerController player)
    {
        player.GetComponent<Duplicator>().isActive = true;
        base.EnablePowerUp(player);
        StartCoroutine(DestroyPowerUp(player));
    }

    IEnumerator DestroyPowerUp(PlayerController player)
    {
        yield return new WaitForSeconds(cooldown);
        if(player.GetComponent<Duplicator>() != null)
        {
            player.GetComponent<Duplicator>().isActive = false;
            Destroy(gameObject);
        }
        
    }
}
