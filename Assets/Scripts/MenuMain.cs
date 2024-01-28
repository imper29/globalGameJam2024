using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMain : MonoBehaviour
{
    public void OnPlay()
    {
        SceneManager.LoadScene("GameplayIntroScene");
    }
    public void OnQuit()
    {
        Application.Quit();
    }
}
