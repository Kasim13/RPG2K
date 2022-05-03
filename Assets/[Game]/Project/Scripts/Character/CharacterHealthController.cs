using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealthController : MonoBehaviour, IDamageable, IHealable
{
    public float DefHealth;
    public float MaxHealth;
    public float MinHealth;
    public float CurrentHealth
    {
        get
        {
            var playerData = SaveLoadManager.LoadPDP<PlayerData>(SavedFileNameHolder.PlayerData, new PlayerData());
            return playerData.CurrentHealth;
        }
        set
        {
            var playerData = SaveLoadManager.LoadPDP<PlayerData>(SavedFileNameHolder.PlayerData, new PlayerData());
            playerData.CurrentHealth = value;
            SaveLoadManager.SavePDP(playerData, SavedFileNameHolder.PlayerData);
        }
    }

    Character character;
    Character Character { get { return (character == null) ? character = GetComponent<Character>() : character; } }

    private void OnEnable()
    {
        if (Managers.Instance == null)
            return;

        Character.OnCharacterRevive.AddListener(ResetHealth);
        EventManager.OnGameStart.AddListener(ResetHealth);

    }

    private void OnDisable()
    {
        if (Managers.Instance == null)
            return;

        Character.OnCharacterRevive.RemoveListener(ResetHealth);
        EventManager.OnGameStart.RemoveListener(ResetHealth);
    }

    public void Start()
    {
        ResetHealth();
    }

    private void ResetHealth()
    {
        CurrentHealth = MinHealth;
    }
    public void Damage()
    {
        if (CurrentHealth >= MinHealth)
        { CurrentHealth -= 0.4f;
        }

        if (CurrentHealth < MinHealth)
        { CurrentHealth = MinHealth; }

        Character.OnCharacterHit.Invoke();
    }
    public void Heal()
    {
        if (CurrentHealth <= MaxHealth)
        { CurrentHealth += 0.4f; }

        if (CurrentHealth >= MaxHealth)
        { CurrentHealth = MaxHealth; }

        Character.OnCharacterHeal.Invoke();      
    }
}
