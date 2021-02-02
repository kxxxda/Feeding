using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GameData 
{
    public int stageCount; //스테이지 개수
    public int[] stageActivate;//스테이지 활성화 여부
    public int[] gender;
    public int[] clickCount;//만약 0이라면 한번도 시작하지 않은 스테이지로 판정 --> gender패널 열리게 하기

    //public int stage1;//스테이지 활설화 :1, 비활성화 : 0
    //public int stage2;
    //public int stage3;
    //public int stage4;
    //public int stage5;
    //public int stage6;
    //public int stage7;
    //public int stage8;
    //public int gender1;//성별 정보 (0:아직 정보 없음 1:여자 2:남자 3:특수 캐릭터) 
    //public int gender2;
    //public int gender3;
    //public int gender4;
    //public int gender5;
    //public int gender6;
    //public int gender7;
    //public int gender8;
    //public int clickCount1;//해당 스테이지의 클릭  --> 클릭수에 따라서 머리 나타나게 하면 됨
    //public int clickCount2;
    //public int clickCount3;
    //public int clickCount4;
    //public int clickCount5;
    //public int clickCount6;
    //public int clickCount7;
    //public int clickCount8;
}
