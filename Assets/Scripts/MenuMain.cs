using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMain : MonoBehaviour
{
    public void OnPlay()
    {
        SceneManager.LoadScene("GameplayScene");
    }
    public void OnQuit()
    {
        Application.Quit();
    }
}
