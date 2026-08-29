using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject adjustPanel;

    [SerializeField]
    private Slider volumeSlider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioManager.instance.PlayBGM(0);

        AudioManager.instance.LoadCurrentMasterVolumn();
    }

    public void StartGame()
    {
        Settings.fromSave = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Loading");
    }

    public void LoadSaveGame()
    {
        Settings.fromSave = true;
        SceneManager.LoadScene("Loading");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void ShowAdjustPanel(bool flag)
    {
        adjustPanel.SetActive(flag);
    }
}
