using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseBotton : MonoBehaviour
{
    [SerializeField] GameObject PausePanel;
    public void Pause()
    {
        Time.timeScale = 0.0f;
        PausePanel.SetActive(true);
    }
    public void Continue()
    {
        Time.timeScale = 1.0f;
        PausePanel.SetActive(false);
    }
    public void Restart()
    {
        SceneManager.LoadScene("TitleScene", LoadSceneMode.Single);
    }
}
