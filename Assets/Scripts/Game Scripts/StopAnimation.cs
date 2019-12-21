using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAnimation : MonoBehaviour
{
    void StopParentAnimation()
    {
        SendMessageUpwards("StopAnimation");
    }

    void ShootProjectile()
    {
        Debug.Log("Tell firemage to shoot projectiles");
        //SendMessageUpwards("ShootProjectile");
    }
}
