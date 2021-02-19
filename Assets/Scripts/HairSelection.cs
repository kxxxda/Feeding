using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class HairSelection : MonoBehaviour
{
    public GameObject genderPanel;
    public GameObject coverPanel;
    public DataManager dataManager;
    public Button[] stageButton;
    
    

    private void Awake()
    {
        dataManager = GameObject.Find("Data Manager").GetComponent<DataManager>();
        
    }
    private void Update()
    {
        ActivateStage();
    }

    public void ActivateStage()
    {
        for (int i = 0; i < dataManager.data.stageCount; i++)
            ActivateStage(i);
    }

    public void ActivateStage(int stageNum)
    {
        GameObject hairText = stageButton[stageNum].transform.GetChild(0).gameObject;
        GameObject coverText = stageButton[stageNum].transform.GetChild(1).gameObject;


        if (dataManager.data.stageActivate[stageNum] == 0) { 
            stageButton[stageNum].interactable = false; // 0이면 버튼 클릭을 비활성
            hairText.SetActive(false);
            coverText.SetActive(true);
        }
            
        else {
            stageButton[stageNum].interactable = true;
            hairText.SetActive(true);
            coverText.SetActive(false);
        }
    }

   
    public void OnClickStylekButton() 
    {
        //누른 버튼의 정보 가져오기 
        string type = EventSystem.current.currentSelectedGameObject.name;
        dataManager.currentStage = FindStage(type); //데이터매니저에 고른 스테이지 정보 저장

        
        if(dataManager.data.clickCount[dataManager.currentStage]==0) //세이브파일에서 해당 스테이지 시작했는지 확인//해당 스테이지가 처음이라면
        {
            //패널 열기
            genderPanel.SetActive(!genderPanel.activeSelf);
            coverPanel.SetActive(!coverPanel.activeSelf);
        }
        else //(나중에) 세이브 파일이 있다면 로드하기
        {
            //Debug.Log("이거지 이거야");
            dataManager.currentGender = dataManager.data.gender[dataManager.currentStage];
            dataManager.currentClickCount = dataManager.data.clickCount[dataManager.currentStage];
            dataManager.currentClickStage = dataManager.data.clickStage[dataManager.currentStage];
            OnClickStartButton();
        }

        //고른 성별로 시작하기

    }

    public int FindStage(string type)
    {
        switch (type)
        {
            case "StraightButton":
                return 0;
            case "CurlyButton":
                return 1;
            case "BraidedButton":
                return 2;
            case "MohicanButton":
                return 3;
            case "TaeyangButton":
                return 4;
            case "SandaraButton":
                return 5;
            case "MustacheButton":
                return 6;
            case "MargeButton":
                return 7;
        }
        return 0;
    }
    
    public void OnClickWomanButton()
    {
        dataManager.currentGender = 1;
    }
    public void OnClickManButton()
    {
        dataManager.currentGender = 2;
    }

    public void OnClickCloseButton()//성별 고르기 패널 열고 닫기
    {
        genderPanel.SetActive(!genderPanel.activeSelf);
        coverPanel.SetActive(!coverPanel.activeSelf);
    }

    public void OnClickStartButton()
    {
        dataManager.data.gender[dataManager.currentStage] = dataManager.currentGender;//data에 현재 젠더 저장하면서 넘어가기
        SceneManager.LoadScene("Game Scene test");
    }
    public void OnClickBackButton() 
    {
        SceneManager.LoadScene("Start Scene");
    }
}
