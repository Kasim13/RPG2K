using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Final : MonoBehaviour
{
    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        Time.timeScale = 0;
        EventManager.OnLevelSuccess.Invoke();
    }

}
