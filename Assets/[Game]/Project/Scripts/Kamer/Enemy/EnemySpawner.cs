using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : EnemyAttack
{
    public List<GameObject> Enemies = new List<GameObject>();
    public float spawnRate;

    private float x, y;
    private Vector3 spawnPos;
    public override void Start()
    {
        base.Start();
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        
        if (player != null)
        {
            x = Random.Range(-1f, 1f);
            y = Random.Range(-1f, 1f);
            spawnPos.x += x;
            spawnPos.y += y;
            Instantiate(Enemies[0], spawnPos, Quaternion.identity);
           
            yield return new WaitForSeconds(spawnRate);
            StartCoroutine(SpawnEnemy());
        }
        
    }
    private void Update()
    {
        
    }
}
