using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    public string id;
    public float cooldown;

    public virtual void EnablePowerUp(PlayerController player)
    {
        GetComponentInChildren<MeshRenderer>().enabled = false;
        GetComponentInChildren<SphereCollider>().enabled = false;
        

    }
    


}