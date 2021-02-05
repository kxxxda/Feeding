using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePause : MonoBehaviour
{
    public GameObject content;
    public Slider slider;
    public Button pauseButton;
    public GameObject hairLengthPanel;
    public GameObject pausePanel;
    public GameObject coverPanel;
    public GameObject newStagePanel;

    RectTransform contentRectTrans;
    RectTransform sliderRectTrans;
    RectTransform pauseButtonRectTrans;
    RectTransform hairLengthPanelRectTrans;
    RectTransform pausePanelRectTrans;
    RectTransform coverPanelRectTrans;
    RectTransform newStagePanelRectTrans;

    BoxCollider2D contentCollider;
    BoxCollider2D sliderCollider;
    BoxCollider2D pauseButtonCollider;
    BoxCollider2D hairLengthPanelCollider;
    BoxCollider2D pausePanelCollider;
    BoxCollider2D coverPanelCollider;
    BoxCollider2D newStagePanelCollider;


    public Text hairLength;

    private void Awake()
    {
        contentRectTrans = content.GetComponent<RectTransform>();
        sliderRectTrans = slider.GetComponent<RectTransform>();
        pauseButtonRectTrans = pauseButton.GetComponent<RectTransform>();
        hairLengthPanelRectTrans = hairLengthPanel.GetComponent<RectTransform>();
        pausePanelRectTrans = pausePanel.GetComponent<RectTransform>();
        coverPanelRectTrans = coverPanel.GetComponent<RectTransform>();
        newStagePanelRectTrans = newStagePanel.GetComponent<RectTransform>();

        contentCollider = content.GetComponent<BoxCollider2D>();
        sliderCollider = slider.GetComponent<BoxCollider2D>();
        pauseButtonCollider = pauseButton.GetComponent<BoxCollider2D>();
        hairLengthPanelCollider = hairLengthPanel.GetComponent<BoxCollider2D>();
        pausePanelCollider = pausePanel.GetComponent<BoxCollider2D>();
        coverPanelCollider = coverPanel.GetComponent<BoxCollider2D>();
        newStagePanelCollider = newStagePanel.GetComponent<BoxCollider2D>();
    }


    private void Update()
    {
        hairLength.text = contentRectTrans.rect.height.ToString() + " M"; //근데 콘텐트의 길이가 머리 길이가 되어서는 안됨 *수정필요
        //Debug.Log(contentRectTrans.rect.height);

        if (contentRectTrans.rect.height >= 2000) //content의 높이가 특정 숫자보다 커지면
        {
            slider.gameObject.SetActive(true);  //슬라이더바가 활성화 되고
            slider.value += Time.deltaTime*10;  //*수정필요 : 머리길이 즉, content길이 변화만큼 씩 값이 증가해야 함

            if (slider.value == 100)
                SetSlider();
        }

        /*콜라이더 사이즈 전부 설정*/
        contentCollider.size = new Vector2(contentRectTrans.rect.width, contentRectTrans.rect.height);
        sliderCollider.size = new Vector2(sliderRectTrans.rect.width, sliderRectTrans.rect.height);
        
        /*현재 수정중*/
        pauseButtonCollider.offset = new Vector2(pauseButtonRectTrans.position.x, pauseButtonRectTrans.position.y);
        pauseButtonCollider.size = new Vector2(pauseButtonRectTrans.rect.width, pauseButtonRectTrans.rect.height);
        
        hairLengthPanelCollider.size = new Vector2(hairLengthPanelRectTrans.rect.width, hairLengthPanelRectTrans.rect.height);
        pausePanelCollider.size = new Vector2(pausePanelRectTrans.rect.width, pausePanelRectTrans.rect.height);
        coverPanelCollider.size = new Vector2(coverPanelRectTrans.rect.width, coverPanelRectTrans.rect.height);
        newStagePanelCollider.size = new Vector2(newStagePanelRectTrans.rect.width, newStagePanelRectTrans.rect.height);
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
