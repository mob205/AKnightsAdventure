using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMage : Enemy
{
    public int projectileSpeed;
    public float projectileLife;

    [SerializeField] Projectile projectilePrefab;
    [SerializeField] Transform projOrigin;

    bool canFire = true;

    void Fire()
    {
        if (canFire)
        {
            var projectile = Instantiate(projectilePrefab, projOrigin.position, projOrigin.rotation);
            projectile.parent = this;
            projectile.rb.velocity = movementDir * projectileSpeed;
            Debug.Log("X: " + movementDir.x + " Y: " + movementDir.y);
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
