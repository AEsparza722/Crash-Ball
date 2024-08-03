using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetPickUp : PowerUp
{
    //public Magnet magnet;
    public override void EnablePowerUp(MonoBehaviour player)
    {
        player.GetComponent<Magnet>().enabled = true;
        base.EnablePowerUp(player);
        StartCoroutine(DestroyPowerUp(player));
    }

    IEnumerator DestroyPowerUp(MonoBehaviour player)
    {
        yield return new WaitForSeconds(cooldown);
        player.GetComponent<Magnet>().enabled = false;
        Destroy(gameObject);
    }

}
