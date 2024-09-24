using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    public string id;
    public float cooldown;
    public GameObject particleVfx;

    public virtual void EnablePowerUp(MonoBehaviour player)
    {
        transform.GetChild(1).gameObject.SetActive(false);
        GetComponentInChildren<MeshRenderer>().enabled = false;
        GetComponentInChildren<SphereCollider>().enabled = false;
        transform.position = new Vector3(0, -200f, 0);
    }
}