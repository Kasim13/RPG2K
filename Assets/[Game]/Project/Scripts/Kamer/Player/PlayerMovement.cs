using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerStats playerStats;
    private Vector2 targetPos;
    public float dashRange;
    public Rigidbody2D rb;
    Animator anim;

    Vector2 direction;
    Vector2 movement;

    public Transform attackPoint;

    public LayerMask enemyLayers;

    float nextAttackTime = 0f;
    private enum Facing { UP, DOWN, LEFT, RIGHT };
    private Facing facingDir = Facing.DOWN;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        TakeInput();
        Move();

    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * playerStats.moveSpeed * Time.fixedDeltaTime);
    }
    private void Move()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //transform.Translate(direction * playerStats.moveSpeed * Time.deltaTime);
        if (movement.x == 0)
        {
            anim.SetBool("isRunning", false);
        }
        if (movement.x < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            anim.SetBool("isRunning", true);
            facingDir = Facing.LEFT;
        }
        else if (movement.x > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            anim.SetBool("isRunning", true);
            facingDir = Facing.LEFT;
        }
        if (movement.y < 0)
        {
            anim.SetBool("isRunning", true);
            facingDir = Facing.DOWN;
        }
        else if (movement.y > 0)
        {
            anim.SetBool("isRunning", true);
            facingDir = Facing.UP;
        }
    }
    private void TakeInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            targetPos = Vector2.zero;
            if (facingDir == Facing.UP)
            {
                targetPos.y = 1;
            }
            else if (facingDir == Facing.DOWN)
            {
                targetPos.y = -1;
            }
            else if (facingDir == Facing.RIGHT)
            {
                targetPos.x = 1;
            }
            else if (facingDir == Facing.LEFT)
            {
                targetPos.x = 1; //normalde -1 olmasý gerekiyor fakat biz karakteri "left" hareket ettirmiyoruz, 180 derece döndürüp tekrar "right" hareket ettiriyoruz.
            }
            transform.Translate(targetPos * dashRange);

        }

        if (Time.time >= nextAttackTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
                nextAttackTime = Time.time + 1f / playerStats.attackRate;
            }
        }

    }

    void Attack()
    {
        anim.SetTrigger("atk_1");

        Collider2D[] hitenemies = Physics2D.OverlapCircleAll(attackPoint.position, playerStats.attackRange, enemyLayers);

        foreach (Collider2D enemy in hitenemies)
        {
            enemy.GetComponent<Enemy>().DealDamage(playerStats.damage);
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, playerStats.attackRange);
    }
}
