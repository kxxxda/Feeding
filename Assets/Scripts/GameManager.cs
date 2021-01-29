using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int stage; //현재 진행할 스테이지 정보 


    private void Awake()
    {
        //Debug.Log("GameManager Awake");
        
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
