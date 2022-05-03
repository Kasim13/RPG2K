using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSystem
{
    public event EventHandler OnExperienceChanged;
    public event EventHandler OnLevelChanged;

    private int maxLevel = 20;
    private int level;
    private int experience;
    private int experienceToNextLevel;

    public LevelSystem()
    {
        level = 0;
        experience = 0;
        experienceToNextLevel = 100;
    }
    public void AddExperience(int amount)
    {
        if (level < maxLevel)
        {
            experience += amount;
            while (experience >= experienceToNextLevel)
            {
                //seviye atlamak için yeterli xp aldýðýnda..
                level++;
                experience -= experienceToNextLevel;
                ExperienceForNextLevel();
                PlayerStats.playerStats.LevelUp();
                if (OnLevelChanged != null) OnLevelChanged(this, EventArgs.Empty);

            }
        }
        if (OnExperienceChanged != null) OnExperienceChanged(this, EventArgs.Empty);
    }
    public void ExperienceForNextLevel()
    {
        experienceToNextLevel = experienceToNextLevel + (experienceToNextLevel * 27 / 100);
        Debug.Log(experienceToNextLevel);
    }
    public int GetLevelNumber()
    {
        return level;
    }
    public float GetExperienceNormalized()
    {
        return (float)experience / experienceToNextLevel;
    }
}
