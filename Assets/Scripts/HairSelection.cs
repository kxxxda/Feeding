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
    public GameManager gameManager;
    public GameData gameData;


    public void OnClickStylekButton() 
    {
        //누른 버튼의 정보 가져오기 
        string type = EventSystem.current.currentSelectedGameObject.name;
        gameManager.SelectStage(FindStage(type)); //게임 매니저가 아니라 데이터 매니저로 옮기고 관리해야함

        //(나중에) 세이브 파일이 있다면 로드하기 


        //처음이라면 성별 고르기 팝업 창 띄우기
        if (type=="MargeButton") // 마지버튼은 성별 고르기 팝업 없이 진행
            Debug.Log("성별 고르기 팝업 없이");
        else { //나머지 버튼들은 성별 고르기 팝업 띄우기
            genderPanel.SetActive(!genderPanel.activeSelf);
            coverPanel.SetActive(!coverPanel.activeSelf);        
        }


        //고른 성별로 시작하기

    }

    public int FindStage(string type)
    {
        switch (type)
        {
            case "StraightButton":
                return 1;
            case "CurlyButton":
                return 2;
            case "BraidedButton":
                return 3;
            case "MohicanButton":
                return 4;
            case "TaeyangButton":
                return 5;
            case "SandaraButton":
                return 6;
            case "MustacheButton":
                return 7;
            case "MargeButton":
                return 8;
        }
        return 0;
    }
    

    public void OnClickBackButton() 
    {
        SceneManager.LoadScene("Start Scene");
    }

    public void OnClickCloseButton()
    {
        genderPanel.SetActive(!genderPanel.activeSelf);
        coverPanel.SetActive(!coverPanel.activeSelf);
    }

}
