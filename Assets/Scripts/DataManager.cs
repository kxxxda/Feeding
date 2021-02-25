using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataManager : MonoBehaviour
{
    public static DataManager dataManager;

    public GameData data;//세이브 파일 데이터

    void Awake()
    {
        if(dataManager != null)
        {
            Destroy(this.gameObject);
            return;
        }
        dataManager = this;
        DontDestroyOnLoad(this);

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
   }

    public void InitiateStage()//스테이지 초기화
    {
        data.clickCount = 0;
        data.clickStage = 1;
    }
}
