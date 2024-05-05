using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    public string id;

    public abstract void EnablePowerUp(PlayerController player);
}