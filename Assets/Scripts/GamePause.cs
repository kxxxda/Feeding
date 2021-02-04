using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePause : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject coverPanel;
    public RectTransform rectTrans;
    public Slider slider;

    private void Update()
    {
        Debug.Log(rectTrans.rect.height);
        if (rectTrans.rect.height >= 300) //content의 높이가 특정 숫자보다 커지면
        {
            slider.gameObject.SetActive(true);  //슬라이더바가 활성화 되고
            slider.value += Time.deltaTime;  //흐르는 시간에 맞춰 바가 채워짐
        }

    }

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
