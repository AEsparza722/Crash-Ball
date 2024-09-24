using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuplicatorPickup : PowerUp
{
    public override void EnablePowerUp(MonoBehaviour player)
    {
        player.GetComponent<Duplicator>().isActive = true;
        player.GetComponent<Duplicator>().duplicatorVFX.SetActive(true);
        base.EnablePowerUp(player);
        StartCoroutine(DestroyPowerUp(player));
    }

    IEnumerator DestroyPowerUp(MonoBehaviour player)
    {
        yield return new WaitForSeconds(cooldown);
        if(player.GetComponent<Duplicator>() != null)
        {
            player.GetComponent<Duplicator>().isActive = false;
            player.GetComponent<Duplicator>().duplicatorVFX.SetActive(false);
            Destroy(gameObject);
        }
        
    }
}
