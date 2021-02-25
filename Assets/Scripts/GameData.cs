using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GameData 
{
    //만약 0이라면 한번도 시작하지 않은 스테이지로 판정 --> gender패널 열리게 하기
    //해당 스테이지의 클릭  --> 클릭수에 따라서 머리 나타나게 하면 됨
    [SerializeField] public int clickCount;


    //클릭에 따른 스프라이트 열린 개수
    //1에서 시작 
    //스프라이트 열린 개수 == 머리 하나 스테이지 안에서 나뉘는 세부 스테이지
    [SerializeField] public int clickStage;

    public GameData()
    {
        clickCount = new int();
        clickStage = new int();
    }
    public GameData(GameData data)
    {
        clickCount = data.clickCount;
        clickStage = data.clickStage;
    }
}
