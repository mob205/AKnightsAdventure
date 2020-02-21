using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMage : Enemy
{
    public int projectileSpeed;
    public float projectileLife;

    [SerializeField] Projectile projectilePrefab;
    [SerializeField] Transform projectileOrigin;

    bool canFire = true;

    void Fire()
    {
        if (canFire)
        {
            var projectile = Instantiate(projectilePrefab, projectileOrigin.position, projectileOrigin.rotation);
            projectile.parent = this;
            projectile.rb.velocity = movementDir * projectileSpeed;
            Destroy(projectile.gameObject, projectileLife);
            StartCoroutine(ToggleFire());
        }
    }
    IEnumerator ToggleFire()
    {
        canFire = false;
        yield return new WaitForSeconds(attackCD);
        canFire = true;
    }
}
