using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nextlvl : MonoBehaviour
{
    public void click()
    {
        LevelManager.Instance.NextLevel();
    }
}
