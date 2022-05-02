using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private int lastLvl;

    private void Start()
    {
        lastLvl = PlayerPrefs.GetInt("Last lvl");
        if (lastLvl == 0)
        {
            lastLvl = 1;
        }
        LoadLevel();
    }

    private void LoadLevel()
    {
        SceneManager.LoadSceneAsync(lastLvl);
    }
}