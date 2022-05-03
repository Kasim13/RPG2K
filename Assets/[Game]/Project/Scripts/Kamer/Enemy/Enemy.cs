using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    public EnemyData data;

    public GameObject healthBar;
    public Slider healthBarSlider;
    public Animator anim;
    public GameObject player;
    private Rigidbody2D rb;
    private Vector2 movement;

    public Transform attackPoint;
    public float attackRange = 0.1f;
    public LayerMask playerLayer;
    float nextAttackTime = 0.5f;

    public float health;


    void Start()
    {

        anim = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        gameObject.name = data.enemyName;
        health = data.health;
    }
    private void Update()
    {
        EnemyRotate();

        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();
        movement = direction;

        if (Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + 1f / data.attackSpeed;
        }
    }
    void EnemyRotate()
    {
        if (player != null)
        {
            if (gameObject.transform.position.x > player.transform.position.x)
            {
                gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else
            {
                gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
    }
    private void FixedUpdate()
    {
        MoveCharacter(movement);
    }
    void MoveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * data.moveSpeed * Time.deltaTime));
        anim.SetBool("Walk", true);
    }
    public void DealDamage(float damage)
    {
        healthBar.SetActive(true);
        health -= damage;
        anim.SetTrigger("Hurt");
        CheckDeath();
        healthBarSlider.value = CalculateHealthPercentage();
    }
    public void HealCharacter(float heal)
    {
        health += heal;
        CheckOverheal();
        healthBarSlider.value = CalculateHealthPercentage();
    }
    private void CheckOverheal()
    {
        if (health > data.health)
        {
            health = data.health;
        }
    }
    private void CheckDeath()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            Instantiate(data.xpDrop, transform.position, Quaternion.identity);
            Instantiate(data.silverDrop, transform.position, Quaternion.identity);
        }
    }
    private float CalculateHealthPercentage()
    {
        return (health / data.health);
    }
    void Attack()
    {
        Collider2D hitPlayer = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);
        
        if (hitPlayer != null)
        {
            if (hitPlayer.tag == "Player")
            {
                anim.SetTrigger("Attack");
                PlayerStats.playerStats.DealDamage(data.damage);
            }
        } 
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
