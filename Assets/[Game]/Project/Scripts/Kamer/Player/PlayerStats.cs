using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats playerStats;
    [SerializeField] LevelWindow levelWindow;
    public GameObject player;
    public Text healthText;
    public Slider healthSlider;

    public float health;
    public float maxHealth;

    public int coins, gems;

    public Text coinsValue;

    public float attackRange = 0.25f;
    public float attackRate = 2f;

    public float moveSpeed;
    public float damage;

    public GameObject levelUpUI;
    private void Awake()
    {
        if (playerStats != null)
        {
            Destroy(playerStats);
        }
        else
        {
            playerStats = this;
        }

        DontDestroyOnLoad(this);
    }

    void Start()
    {
        health = maxHealth;
        SetHealthUI();
    }

    public void DealDamage(float damage)
    {
        health -= damage;
        CheckDeath();
        SetHealthUI();
    }
    public void HealCharacter(float heal)
    {
        health += heal;
        CheckOverheal();
        SetHealthUI();
    }
    private void SetHealthUI()
    {
        healthSlider.value = CalculateHealthPercentage();
        healthText.text = Mathf.Ceil(health).ToString() + " / " + Mathf.Ceil(maxHealth).ToString();
    }
    private void CheckOverheal()
    {
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }
    private void CheckDeath()
    {
        if (health <= 0)
        {
            health = 0;
            Destroy(player);
        }
    }
    private float CalculateHealthPercentage()
    {
        return (health / maxHealth);
    }
    public void AddCoins(int amount)
    {
        coins += amount;
        coinsValue.text = coins.ToString();
    }
    LevelSystem levelSystem = new LevelSystem();
    public void AddCurrency(CurrencyPickup currency)
    {
        if (currency.currentObject == CurrencyPickup.PickupObject.SILVER)
        {
            coins += currency.pickupQuantity;
            coinsValue.text = coins.ToString();
        }
        if (currency.currentObject == CurrencyPickup.PickupObject.XP)
        {
            
            levelSystem.AddExperience(currency.pickupQuantity);

            levelWindow.SetLevelSystem(levelSystem);
        }
    }
    public void LevelUp()
    {
        Time.timeScale = 0;
        levelUpUI.SetActive(true);
    }
    
    public void SelectSkill()
    {
        Time.timeScale = 1;
        levelUpUI.SetActive(false);
    }
}
