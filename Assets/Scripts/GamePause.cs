using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePause : MonoBehaviour
{
    public GameObject menuSet;
    public GameObject display;
    public GameObject content;
    public GameObject hairLengthPanel;
    public GameObject pausePanel;
    public GameObject coverPanel;
    public GameObject newStagePanel;
    public Image progressBar;
    public Image progressBar_Fill;
    public Button pauseButton;

    private RectTransform displayRectTrans;
    private RectTransform contentRectTrans;
    private RectTransform progressBarRectTrans;
    private RectTransform pauseButtonRectTrans;
    private RectTransform hairLengthPanelRectTrans;
    private RectTransform pausePanelRectTrans;
    private RectTransform coverPanelRectTrans;
    private RectTransform newStagePanelRectTrans;

    private BoxCollider2D contentCollider;
    private BoxCollider2D sliderCollider;
    private BoxCollider2D pauseButtonCollider;
    private BoxCollider2D hairLengthPanelCollider;
    private BoxCollider2D pausePanelCollider;
    private BoxCollider2D coverPanelCollider;
    private BoxCollider2D newStagePanelCollider;

    private DataManager dataManager;

    public Text hairLength;

    private bool bPaused = false;

    private bool isPanelOpened = false;

    private void Awake()
    {
        dataManager = GameObject.Find("Data Manager").GetComponent<DataManager>();

        displayRectTrans = display.GetComponent<RectTransform>();
        contentRectTrans = content.GetComponent<RectTransform>();
        progressBarRectTrans = progressBar.GetComponent<RectTransform>();
        pauseButtonRectTrans = pauseButton.GetComponent<RectTransform>();
        hairLengthPanelRectTrans = hairLengthPanel.GetComponent<RectTransform>();
        pausePanelRectTrans = pausePanel.GetComponent<RectTransform>();
        coverPanelRectTrans = coverPanel.GetComponent<RectTransform>();
        newStagePanelRectTrans = newStagePanel.GetComponent<RectTransform>();

        contentCollider = content.GetComponent<BoxCollider2D>();
        sliderCollider = progressBar.GetComponent<BoxCollider2D>();
        pauseButtonCollider = pauseButton.GetComponent<BoxCollider2D>();
        hairLengthPanelCollider = hairLengthPanel.GetComponent<BoxCollider2D>();
        pausePanelCollider = pausePanel.GetComponent<BoxCollider2D>();
        coverPanelCollider = coverPanel.GetComponent<BoxCollider2D>();
        newStagePanelCollider = newStagePanel.GetComponent<BoxCollider2D>();

    }


    private void Update()
    {
        hairLength.text = dataManager.data.clickStage[dataManager.currentStage].ToString() + " M"; //근데 콘텐트의 길이가 머리 길이가 되어서는 안됨 *수정필요
                                                                                                   //Debug.Log(contentRectTrans.rect.height);


        progressBar_Fill.fillAmount = dataManager.data.clickStage[dataManager.currentStage] / 100f;  //클릭수 받아오기
        //Debug.Log("현재 프로그레스바 값은 : " + progressBar_Fill.fillAmount + " 받아온값은 : " + dataManager.data.clickStage[dataManager.currentStage] / 100f);

        if (progressBar_Fill.fillAmount == 1 && !isPanelOpened)
        {
            SliderFull();
            isPanelOpened = true;
        }

        SetCollider();
    }

    void SetCollider()
    {
        /*콜라이더 사이즈 전부 설정*/

        /*content의 길이가 더 길때 이걸로 하기*/
        if (contentRectTrans.rect.height > displayRectTrans.rect.height)
            contentCollider.size = new Vector2(contentRectTrans.rect.width, contentRectTrans.rect.height);
        else
            contentCollider.size = new Vector2(displayRectTrans.rect.width, displayRectTrans.rect.height);

        sliderCollider.size = new Vector2(progressBarRectTrans.rect.width, progressBarRectTrans.rect.height);
        sliderCollider.offset = new Vector2((-1) * progressBarRectTrans.rect.width / 2, 0);

        pauseButtonCollider.size = new Vector2(pauseButtonRectTrans.rect.width, pauseButtonRectTrans.rect.height);
        pauseButtonCollider.offset = new Vector2(pauseButtonRectTrans.rect.width / 2, (-1) * (pauseButtonRectTrans.rect.height / 2));

        hairLengthPanelCollider.size = new Vector2(hairLengthPanelRectTrans.rect.width, hairLengthPanelRectTrans.rect.height);
        hairLengthPanelCollider.offset = new Vector2((-1) * (hairLengthPanelRectTrans.rect.width / 2), (-1) * (hairLengthPanelRectTrans.rect.height / 2));

        pausePanelCollider.size = new Vector2(pausePanelRectTrans.rect.width, pausePanelRectTrans.rect.height);
        coverPanelCollider.size = new Vector2(coverPanelRectTrans.rect.width, coverPanelRectTrans.rect.height);
        newStagePanelCollider.size = new Vector2(newStagePanelRectTrans.rect.width, newStagePanelRectTrans.rect.height);

    }

    void SliderFull()
    {
        Debug.Log("새로운 스테이지 열림");
        newStagePanel.SetActive(true); //스테이지 깼으니까 패널 띄우고
        Invoke("CloseNewStagePanel", 2); //2초뒤에 띄운 패널 없애고
        progressBar.gameObject.SetActive(false);
    }

    private void OnApplicationPause(bool pause)//일시정지 기능
    {
        if (pause)
        {
            bPaused = true;
            OnClickToggleMenuSetButton();
        }
        else
        {
            if (bPaused)
            {
                bPaused = false;
                OnClickToggleMenuSetButton();
            }
        }
    }
    void CloseNewStagePanel()
    {
        newStagePanel.SetActive(false);
    }

    public void OnClickToggleMenuSetButton()
    {

        menuSet.SetActive(!menuSet.activeSelf);

        //pausePanel.SetActive(!pausePanel.activeSelf);
        //coverPanel.SetActive(!coverPanel.activeSelf);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu Scene");
    }

    public void StageInitialize()
    {
        dataManager.InitiateClickCount(dataManager.currentStage);
        dataManager.Save();
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
