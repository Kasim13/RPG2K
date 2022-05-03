using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="New Enemy Type",menuName ="Enemy Type")]
public class EnemyData : ScriptableObject
{
    public float health;
    public float damage;
    public float moveSpeed;
    public float attackSpeed;
    //public GameObject healthBar;
    //public Slider healthBarSlider;
    public GameObject silverDrop;
    public GameObject xpDrop;
    public string enemyName;

}
