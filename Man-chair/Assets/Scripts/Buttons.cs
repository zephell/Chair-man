using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void ToMain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
