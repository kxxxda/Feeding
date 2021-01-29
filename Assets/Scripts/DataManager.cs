using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public GameData data;
    void Awake()
    {
        Debug.Log("1");
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        Debug.Log("2");
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
        Debug.Log(File.Exists(Application.dataPath + "/GameData.json"));
        return File.Exists(Application.dataPath + "/GameData.json");
    }
    void MakeSaveFile()
    {
        Debug.Log("세이브 파일 생성중");

        data = new GameData();
        data.stage1 = 1; //스테이지 활성화 : 1, 비활성화 :0
        data.stage2 = 0;
        data.stage3 = 0;
        data.stage4 = 0;
        data.stage5 = 0;
        data.stage6 = 0;
        data.stage7 = 0;
        data.stage8 = 0;
        data.gender1 = 0;
        data.gender2 = 0;
        data.gender3 = 0;
        data.gender4 = 0;
        data.gender5 = 0;
        data.gender6 = 0;
        data.gender7 = 0;
        data.gender8 = 3;//마지 샘숭은 특수 캐릭터
        data.clickCount1 = 0;
        data.clickCount2 = 0;
        data.clickCount3 = 0;
        data.clickCount4 = 0;
        data.clickCount5 = 0;
        data.clickCount6 = 0;
        data.clickCount7 = 0;
        data.clickCount8 = 0;

        //안드로이드는 이걸로 바꿔야 됨
        //Application.persistentDataPath
        File.WriteAllText(Application.dataPath + "/GameData.json", JsonUtility.ToJson(data));
    }
    void LoadSaveFile()
    {
        string path = File.ReadAllText(Application.dataPath + "/GameData.json");
        data = JsonUtility.FromJson<GameData>(path);
    }
}
