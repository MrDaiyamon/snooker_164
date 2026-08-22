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
        SceneManager.LoadScene("Loading");
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
