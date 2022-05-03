using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Character : MonoBehaviour
{
    private Rigidbody rigidbody;
    public Rigidbody Rigidbody { get { return (rigidbody == null) ? rigidbody = GetComponent<Rigidbody>() : rigidbody; } }

    private Collider collider;
    public Collider Collider { get { return (collider == null) ? collider = GetComponent<Collider>() : collider; } }

    #region Events
    [HideInInspector]
    public UnityEvent OnCharacterHit = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnCharacterHeal = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnCharacterDie = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnCharacterRevive = new UnityEvent();
    #endregion

    private bool isDead;

    public bool IsDead { get { return (isDead); } set { isDead = value; } }

    private bool isControlable;

    public bool IsControlable { get { return isControlable; } set { isControlable = value; } }

    public float CurrentHealth
    {
        get
        {
            var playerData = SaveLoadManager.LoadPDP<PlayerData>(SavedFileNameHolder.PlayerData, new PlayerData());
            return playerData.CurrentHealth;
        }
    }

    private void OnEnable()
    {
        if (Managers.Instance == null)
            return;

        CharacterManager.Instance.AddCharacter(this);
        EventManager.OnLevelContine.AddListener(ReviveCharacter);
        EventManager.OnLevelStart.AddListener(ReviveCharacter);
        EventManager.OnLevelFinish.AddListener(() =>
        {
        });

        OnCharacterHit.AddListener(Hit);
        OnCharacterHeal.AddListener(Heal);
    }

    private void OnDisable()
    {
        if (Managers.Instance == null)
            return;

        CharacterManager.Instance.RemoveCharacter(this);
        EventManager.OnLevelContine.RemoveListener(ReviveCharacter);
        EventManager.OnLevelStart.RemoveListener(ReviveCharacter);
        EventManager.OnLevelFinish.RemoveListener(() =>
        {
        });

        OnCharacterHit.RemoveListener(Hit);
        OnCharacterHeal.RemoveListener(Heal);
    }

    public void Hit()
    {
        CharacterManager.Instance.Player.transform.localScale = new Vector3(CurrentHealth, CurrentHealth, CurrentHealth);
    }
    public void Heal()
    {
        CharacterManager.Instance.Player.transform.localScale = new Vector3(CurrentHealth, CurrentHealth, CurrentHealth);
    }

    public void KillCharacter()
    {
        if (IsDead)
            return;

        IsDead = true;
        IsControlable = false;
        OnCharacterDie.Invoke();

        EventManager.OnLevelFailed.Invoke();
    }

    public void ReviveCharacter()
    {
        if (!IsDead)
            return;

        IsDead = false;
        IsControlable = true;
        OnCharacterRevive.Invoke();
    }


    private void OnTriggerEnter(Collider other)
    {
        ICollectable icollectable = other.GetComponent<ICollectable>();

        if (icollectable != null)
            icollectable.Collect();
    }
}
