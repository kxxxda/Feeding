using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    //프리팹들
    public GameObject hairSprayPrefab;


    //오브젝트 배열
    GameObject[] hairSpray;
   


    //타겟 오브젝트 배열
    GameObject[] targetPool;

    private void Awake()
    {
        hairSpray = new GameObject[20];

        Generate();
    }

    void Generate()
    {
        GenerateLoop(hairSpray, hairSprayPrefab);
    }

    void GenerateLoop(GameObject[] my, GameObject myPrefab)
    {
        for (int i = 0; i < my.Length; i++)
        {
            my[i] = Instantiate(myPrefab);
            my[i].SetActive(false);
        }
    }

    public GameObject[] TypeOfObj(string type)
    {
        switch (type)
        {
            case "HairSpray":
                return hairSpray;
        }
        return null;
    }

    public GameObject MakeObj(string type)
    {
        targetPool = TypeOfObj(type);

        for (int i = 0; i < targetPool.Length; i++)
        {
            if (!targetPool[i].activeSelf)//비활성화 되어있다면
            {
                //targetPool[i].SetActive(true);
                return targetPool[i];
            }
        }
        return null;
    }

    public GameObject[] GetPool(string type)
    {
        targetPool = TypeOfObj(type);

        return targetPool;
    }
}
