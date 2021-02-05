using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]

public class DataManager : MonoBehaviour
{
    public static DataManager dataManager;

    public GameData data;//세이브 파일 데이터

    public int currentStage; //현재 진행할 스테이지 정보 
    public int currentClickCount;//현재 진행하는 스테이지 클릭수
    public int currentGender;//0이면 아직 게임진행 x
    void Awake()
    {
        //Debug.Log("1");
        if(dataManager != null)
        {
            Destroy(this.gameObject);
            return;
        }
        dataManager = this;
        DontDestroyOnLoad(this);

    }
    void Start()
    {
        //Debug.Log("2");
        ManageSaveFile();
    }
    
    void ManageSaveFile()
    {
        //세이브 파일 있는지 확인
        if (FindSaveFile())
             LoadSaveFile();//있으면 로드
        else
            MakeSaveFile();//없으면 초기 세이브 파일 생성
    }

    bool FindSaveFile()
    {
        //Debug.Log(File.Exists(Application.dataPath + "/GameData.json"));
        return File.Exists(Application.dataPath + "/GameData.json");
    }
    void MakeSaveFile()
    {
        Debug.Log("세이브 파일 생성중");

        data = new GameData();
        data.stageCount = 8; //스테이지 개수
        data.stageActivate = new int[data.stageCount];
        data.gender = new int[data.stageCount];
        data.clickCount = new int[data.stageCount];

        for(int i=0;i<data.stageCount;i++)
        {
            InitiateStage(i);
        }
        data.stageActivate[0] = 1; //default값인 1번째 스테이지 오픈
        data.gender[7] = 3;

        //안드로이드는 이걸로 바꿔야 됨
        //Application.persistentDataPath
        File.WriteAllText(Application.dataPath + "/GameData.json", JsonUtility.ToJson(data,true));
    }
    void LoadSaveFile()
    {
        string path = File.ReadAllText(Application.dataPath + "/GameData.json");
        data = JsonUtility.FromJson<GameData>(path);
    }

    void InitiateStage(int stageNum)//스테이지 초기화
    {
        data.stageActivate[stageNum] = 0;
        data.gender[stageNum] = 0;
        data.clickCount[stageNum] = 0;
    }

    public void SaveFile()
    {
        File.WriteAllText(Application.dataPath + "/GameData.json", JsonUtility.ToJson(data, true));
    }
    void SaveStage()//스테이지의 성별,클릭수 저장
    {
        data.clickCount[dataManager.currentStage] = dataManager.currentClickCount;
        data.gender[dataManager.currentStage] = dataManager.currentGender;
    }
    void SaveStageActivation()//스테이지 활성화하기
    {
        data.stageActivate[dataManager.currentStage] = 1;
    }
}
