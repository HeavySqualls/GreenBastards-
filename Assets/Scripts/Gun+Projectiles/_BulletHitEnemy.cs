using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _BulletHitEnemy : EnemyInteractable
{
    public override void OnHit(Collision2D hit, EnemyController enemy)
    {
        if (hit.gameObject.GetComponent<ProjectileController>())
        {
            enemy.TakeDamage(hit.gameObject.GetComponent<ProjectileController>().damageOutput);
            Destroy(hit.gameObject);
        }
    }
}
