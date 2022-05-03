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
        //levelsystem objesini ayarlýyor.
        this.levelSystem = levelSystem;

        //Baþlangýç deðerlerini ayarlýyor
        SetLevelNumber(levelSystem.GetLevelNumber());
        SetExperienceBarSize(levelSystem.GetExperienceNormalized());

        //Deðiþtirelen eventleri güncellemesi için fonksiyonlarý çaðýrýyor
        levelSystem.OnExperienceChanged += LevelSystem_OnExperienceChanged;
        levelSystem.OnLevelChanged += LevelSystem_OnLevelChanged;
    }

    private void LevelSystem_OnLevelChanged(object sender, EventArgs e)
    {
        //Level arttýðýnda text'i güncellemesi için
        SetLevelNumber(levelSystem.GetLevelNumber());
    }

    private void LevelSystem_OnExperienceChanged(object sender, EventArgs e)
    {
        //Xp arttýðýnda bar'ý güncellemesi için
        SetExperienceBarSize(levelSystem.GetExperienceNormalized());
    }
}
