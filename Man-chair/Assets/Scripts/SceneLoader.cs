using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private int lastLvl;

    private void Start()
    {
        lastLvl = PlayerPrefs.GetInt("Last_lvl");
        if (lastLvl == 0)
        {
            lastLvl = 2;
        }
        LoadLevel();
    }

    private void LoadLevel()
    {
        SceneManager.LoadScene(lastLvl);
    }
}