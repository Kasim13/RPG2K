using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [Header("UI Buttons")]
    public Button pauseButton;
    public Button restartMenuButton;
    public Button restartLoseButton;
    public Button restartWinButoon;
    public Button resumeButton;
    public Button nextButton;
    [Header("Panels")]
    public CanvasGroup MainMenuPanel;
    public CanvasGroup InGameBeforePanel;
    public CanvasGroup winPanel;
    public CanvasGroup losePanel;
    public CanvasGroup Menu;
    public CanvasGroup gamePlay;
    public Text levelIndex;
    
    //
    public void Awake()
    {
        Instance = this;
        DefaultLayout();
        AddButonListeners();
    }
    public void OnEnable()
    {
        EventManager.OnGameStart.AddListener(GameStart);
        EventManager.OnLevelStart.AddListener(OnLevelStart);
        EventManager.OnLevelFailed.AddListener(LevelFailed);
        EventManager.OnLevelSuccess.AddListener(LevelSucces);
    }
    private void OnDisable()
    {
        EventManager.OnGameStart.RemoveListener(GameStart);
        EventManager.OnLevelStart.RemoveListener(OnLevelStart);
        EventManager.OnLevelFailed.RemoveListener(LevelFailed);
        EventManager.OnLevelSuccess.RemoveListener(LevelSucces);
    }

    private void AddButonListeners()
    {
        pauseButton.onClick.AddListener(PauseTheGame);
        resumeButton.onClick.AddListener(ResumeGame);
        restartMenuButton.onClick.AddListener(RestartLevelButton);
        restartLoseButton.onClick.AddListener(RestartLevel);
        restartWinButoon.onClick.AddListener(RestartLevelButton);
        nextButton.onClick.AddListener(NextLevel);
    }



    //
    private void GameStart()
    {
        CanvasProp.Hide(MainMenuPanel);
        CanvasProp.Show(InGameBeforePanel);
    }
    private void OnLevelStart()
    {
        levelIndex.text = "Level" + LevelManager.Instance.LevelIndex.ToString();
        CanvasProp.Show(gamePlay);
        CanvasProp.Hide(InGameBeforePanel);
    }

    private void LevelFailed()
    {
        CanvasProp.Show(losePanel);
    }
    private void LevelSucces()
    {
        CanvasProp.Show(winPanel);
    }



    //Pause-Resume
    public void PauseTheGame()
    {
        Time.timeScale = 0;
        CanvasProp.Show(Menu);
    }
    public void ResumeGame()
    {
        CanvasProp.Hide(Menu);
        Time.timeScale = 1;
    }
    //NextLevel-RestartLevel
    public void NextLevel()
    {
        DefaultLayout();
        NextScene();
    }
    public void RestartLevel()
    {
        DefaultLayout();
        ReloadScene();
    }
    public void RestartLevelButton()
    {
        DefaultLayout();
        GameManager.Instance.EndGame();
        ReloadScene();
    }
    // Methods
    private void DefaultLayout()
    {
        CanvasProp.Hide(losePanel);
        CanvasProp.Hide(winPanel);      
        CanvasProp.Hide(gamePlay);
        CanvasProp.Hide(Menu);
        CanvasProp.Show(InGameBeforePanel);
        Time.timeScale = 1;
        ResetValues();
    }
    public void ResetValues()
    {
    }

    public void NextScene()
    {
        LevelManager.Instance.NextLevel();
    }
    public void ReloadScene()
    { 
        LevelManager.Instance.Restart();
    }
    /* Alternatif panel kapatma veya açma
    public void ShowPanel(CanvasGroup c)
    {
        c.alpha = 1;
        c.interactable = true;
        c.blocksRaycasts = true;
    }

    public void HidePanel(CanvasGroup c)
    {
        c.alpha = 0;
        c.interactable = false;
        c.blocksRaycasts = false;
    }
    */
}

