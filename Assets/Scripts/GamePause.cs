using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePause : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject coverPanel;
    public GameObject newStagePanel;
    public RectTransform rectTrans;
    public Slider slider;
    public Text hairLength;

    private void Update()
    {
        hairLength.text = rectTrans.rect.height.ToString() + " M"; //근데 콘텐트의 길이가 머리 길이가 되어서는 안됨 *수정필요
        Debug.Log(rectTrans.rect.height);

        if (rectTrans.rect.height >= 2000) //content의 높이가 특정 숫자보다 커지면
        {
            slider.gameObject.SetActive(true);  //슬라이더바가 활성화 되고
            slider.value += Time.deltaTime*10;  //*수정필요 : 머리길이 즉, content길이 변화만큼 씩 값이 증가해야 함

            if (slider.value == 100)
                SetSlider();
        }

    }

    void SetSlider()
    {
        newStagePanel.SetActive(true); //스테이지 깼으니까 패널 띄우고
        //Invoke("CloseNewStagePanel", 2); //2초뒤에 띄운 패널 없애고
        slider.gameObject.SetActive(false); //슬라이더도 사라지기
    }
    void CloseNewStagePanel()
    {
        newStagePanel.SetActive(false);
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
