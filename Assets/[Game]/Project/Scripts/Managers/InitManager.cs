using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class InitManager : MonoBehaviour
{
    private IEnumerator Start()
    {
        PlayerPrefs.DeleteAll();
        //Init Game Here   
        yield return SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
        yield return SceneManager.LoadSceneAsync(PlayerPrefs.GetString("LastLevel", "Level01"), LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(PlayerPrefs.GetString("LastLevel", "Level01")));
        //GameManager.Instance.StartGame();
        Destroy(gameObject);
    }
}