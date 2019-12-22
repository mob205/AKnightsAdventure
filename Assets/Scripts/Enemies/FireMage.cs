using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMage : Enemy
{
    [SerializeField]
    GameObject projectile;

    void Fire()
    {
        Debug.Log("Shoot projectile");
    }
}
