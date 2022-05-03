using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatToPlayer : MonoBehaviour
{
    private GameObject player;
    private bool playerCircleCollider = false;
    public float speed;

    private void Start()
    {
        player = GameObject.Find("Player");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerCircleCollider")
        {
            playerCircleCollider = true;
        }
    }
    private void Update()
    {
        if (playerCircleCollider == true)
        {
            if (player != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            }
        }
    }
}
