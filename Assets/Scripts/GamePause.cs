using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePause : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject coverPanel;

    public void OnClickPauseButton()
    {
        pausePanel.SetActive(!pausePanel.activeSelf);
        coverPanel.SetActive(!coverPanel.activeSelf);

    }

    public void OnClickCloseButton()
    {
        pausePanel.SetActive(!pausePanel.activeSelf);
        coverPanel.SetActive(!coverPanel.activeSelf);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu Scene");
    }
}
