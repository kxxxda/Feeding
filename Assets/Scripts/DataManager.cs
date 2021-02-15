using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataManager : MonoBehaviour
{
    public static DataManager dataManager;

    public GameData data;//세이브 파일 데이터

    public int currentStage; //현재 진행할 스테이지 정보 
    public int currentClickCount;//현재 진행하는 스테이지 클릭수
    public int currentGender;//0이면 아직 게임진행 x
    public int currentClickStage;

    void Awake()
    {
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
        ManageSaveFile();
    }

    public static void SaveFile(GameData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath+ "/GameData.nobody";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData savingData = new GameData(data);

        formatter.Serialize(stream, savingData);
        stream.Close();
    }
    public static GameData LoadFile()
    {
        string path = Application.persistentDataPath + "/GameData.nobody";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData loadData = formatter.Deserialize(stream) as GameData;
            stream.Close();

            return loadData;
        }
        else
        {
            return null;
        }

    }

    void ManageSaveFile()
    {
        data = LoadFile();
        if (data == null)
        {
            FirstSaveFile();
            Save();
        }
        else
        {
            Debug.Log("세이브 파일 로드");
        }
    }
    public void Save()
    {
        SaveFile(data);
    }
    void FirstSaveFile()
    {
        Debug.Log("처음 세이브 파일 생성");

        //GameData 생성 및 데이터 초기화
        data = new GameData();
        for (int i = 0; i < data.stageCount; i++)
        {
            InitiateStage(i);
        }
        data.stageActivate[0] = 1; //default값인 1번째 스테이지 오픈
        data.gender[7] = 3;

        //DataManager의 current변수들 초기화
        currentStage = 0;
        currentGender = 0;
        currentClickStage = 1;//스프라이트 시작 스테이지는 1에서 시작해야 곱셈이 가능
        currentClickCount = 0;
   }

    public void InitiateStage(int stageNum)//스테이지 초기화
    {
        data.stageActivate[stageNum] = 0;
        data.gender[stageNum] = 0;
        data.clickCount[stageNum] = 0;
        data.clickStage[stageNum] = 1;
    }

    public void InitiateClickCount(int stageNum)//스테이지 초기화
    {
        data.clickCount[stageNum] = 0;
        data.clickStage[stageNum] = 1;
    }

    public void SetCurrentInfo()
    {
        currentGender = data.gender[currentStage];
        currentClickCount = data.clickCount[currentStage];
        currentClickStage = data.clickStage[currentStage];
    }
}
