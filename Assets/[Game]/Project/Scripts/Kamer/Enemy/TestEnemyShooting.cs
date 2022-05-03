using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyShooting : EnemyAttack
{

    public GameObject projectile;
    public float minDamage, maxDamage, projectileForce, cooldown;

    public override void Start()
    {
        base.Start();
        StartCoroutine(ShootPlayer());

    }

    IEnumerator ShootPlayer()
    {
        yield return new WaitForSeconds(cooldown);

        if (player != null)
        {
            GameObject spell = Instantiate(projectile, transform.position, Quaternion.identity);
            Vector2 myPos = transform.position;
            Vector2 targetPosition = player.transform.position;
            Vector2 direction = (targetPosition - myPos).normalized;
            spell.GetComponent<Rigidbody2D>().velocity = direction * projectileForce;
            spell.GetComponent<TestEnemyProjectile>().damage = Random.Range(minDamage, maxDamage);
            StartCoroutine(ShootPlayer());

            Destroy(spell, 2.5f);
        }
    }
}
