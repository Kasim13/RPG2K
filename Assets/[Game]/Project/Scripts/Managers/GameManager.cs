using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameData
{
    public float BoostSpeedTimer=1.5f;
    public float IsBoostSpeed;
}


public class GameManager : Singleton<GameManager>
{
    public GameData GameData = new GameData();

    private bool isGameStarted;
    public bool IsGameStarted { get { return isGameStarted; } private set { isGameStarted = value; } }

    private bool isLevelStarted;
    public bool IsLevelStarted { get { return isLevelStarted; } private set { isLevelStarted = value; } }

    private void Awake()
    {
        IsGameStarted = false;
        IsLevelStarted = false;
    }

    private void OnEnable()
    {
        EventManager.OnLevelFailed.AddListener(EndGame);
        EventManager.OnLevelSuccess.AddListener(EndGame);
    }

    private void OnDisable()
    {
        EventManager.OnLevelFailed.RemoveListener(EndGame);
        EventManager.OnLevelSuccess.RemoveListener(EndGame);
    }

    public void StartGame()
    {
        if (IsGameStarted)
            return;

        IsGameStarted = true;
        EventManager.OnGameStart.Invoke();
    }

    public void StartLevel()
    {
        if (IsLevelStarted)
            return;

        IsLevelStarted = true;
        EventManager.OnLevelStart.Invoke();
    }

    public void EndGame()
    {
        if (!IsGameStarted)
            return;

        IsLevelStarted = false;
        EventManager.OnGameEnd.Invoke();
    }
}
