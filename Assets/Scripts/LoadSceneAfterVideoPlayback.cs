using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class LoadSceneAfterVideoPlayback : MonoBehaviour
{
    [SerializeField]
    private string m_SceneToLoad;

    private void Update()
    {
        if (!GetComponent<VideoPlayer>().isPlaying || Input.GetKeyDown(KeyCode.Space))
            SceneManager.LoadScene(m_SceneToLoad);
    }
}
