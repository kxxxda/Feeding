using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    int stage; //현재 진행할 스테이지 정보 
    public DataManager dataManager;
    public Button[] stageButton;


    private void Awake()
    {
        Debug.Log("GameManager Awake");
        dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
       
    }
    private void Start()
    {
        ActivateStage();
    }

    public void ActivateStage()
    {
        //여기 리팩토링 보류 --> 우선 gameData.cs 정리하고 다시 건드리기
        if (dataManager.data.stage1 == 0)
        {
            stageButton[0].interactable = false; // 0이면 버튼 클릭을 비활성
        }
        if (dataManager.data.stage2 == 0)
        {
            stageButton[1].interactable = false; // 버튼 클릭을 비활성
        }
        if (dataManager.data.stage3 == 0)
        {
            stageButton[2].interactable = false; // 버튼 클릭을 비활성
        }
        if (dataManager.data.stage4 == 0)
        {
            stageButton[3].interactable = false; // 버튼 클릭을 비활성
        }
        if (dataManager.data.stage5 == 0)
        {
            stageButton[4].interactable = false; // 버튼 클릭을 비활성
        }
        if (dataManager.data.stage6 == 0)
        {
            stageButton[5].interactable = false; // 버튼 클릭을 비활성
        }
        if (dataManager.data.stage7 == 0)
        {
            stageButton[6].interactable = false; // 버튼 클릭을 비활성
        }
        if (dataManager.data.stage8 == 0)
        {
            stageButton[7].interactable = false; // 버튼 클릭을 비활성
        }
    }
    public void SelectStage(int sta)
    {
        stage = sta; //현재 클릭한 버튼의 스테이지 정보
                     //여기에서 현재 클릭한 스테이지 정보를 가지고  datamanager에서 load한 data구조체 정보와 비교작업
    }

    public void Selectgender()//여기 함수도 똑같이 헤어셀렉션에서 gender 클릭하면 받아오고 
    {
        //위 함수랑 똑같이 여기서 비교 작업
    }
}
