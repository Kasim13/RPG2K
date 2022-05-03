using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelWindow : MonoBehaviour
{
    public Text levelText;
    public Image experienceBarImage;
    private LevelSystem levelSystem;

    private void Awake()
    {

    }
    public void TestXpButton()
    {
        levelSystem.AddExperience(25);
    }
    void SetExperienceBarSize(float experienceNormalized)
    {
        experienceBarImage.fillAmount = experienceNormalized;
    }
    void SetLevelNumber(int levelNumber)
    {
        levelText.text = "LEVEL\n" + (levelNumber + 1);
    }
    public void SetLevelSystem(LevelSystem levelSystem)
    {
        //levelsystem objesini ayarl�yor.
        this.levelSystem = levelSystem;

        //Ba�lang�� de�erlerini ayarl�yor
        SetLevelNumber(levelSystem.GetLevelNumber());
        SetExperienceBarSize(levelSystem.GetExperienceNormalized());

        //De�i�tirelen eventleri g�ncellemesi i�in fonksiyonlar� �a��r�yor
        levelSystem.OnExperienceChanged += LevelSystem_OnExperienceChanged;
        levelSystem.OnLevelChanged += LevelSystem_OnLevelChanged;
    }

    private void LevelSystem_OnLevelChanged(object sender, EventArgs e)
    {
        //Level artt���nda text'i g�ncellemesi i�in
        SetLevelNumber(levelSystem.GetLevelNumber());
    }

    private void LevelSystem_OnExperienceChanged(object sender, EventArgs e)
    {
        //Xp artt���nda bar'� g�ncellemesi i�in
        SetExperienceBarSize(levelSystem.GetExperienceNormalized());
    }
}
