using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GameData 
{
    //스테이지 개수
    [SerializeField] public int stageCount;


    //스테이지 활성화 여부
    //스테이지 활설화 :1, 비활성화 : 0
    [SerializeField] public int[] stageActivate;


    //성별 정보
    //0:아직 정보 없음 1:여자 2:남자 3:특수 캐릭터
    [SerializeField] public int[] gender;



    //만약 0이라면 한번도 시작하지 않은 스테이지로 판정 --> gender패널 열리게 하기
    //해당 스테이지의 클릭  --> 클릭수에 따라서 머리 나타나게 하면 됨
    [SerializeField] public int[] clickCount;

    public GameData()
    {
        stageCount = 8;

        stageActivate = new int[stageCount];
        gender = new int[stageCount];
        clickCount = new int[stageCount];
    }
    public GameData(GameData data)
    {
        stageCount = data.stageCount;

        stageActivate = data.stageActivate;
        gender = data.gender;
        clickCount = data.clickCount;
    }
}
