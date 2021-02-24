using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    public GameObject hairParent;
    public GameObject[] straightHairPrefabs;
    public int straightHairIndex;

    public Text text;

    private Camera mainCamera;
    private DataManager dataManager;
    private ObjectManager objectManager;
    private Touch tempTouchs;
    private Vector2 touchPos;
    private int maxClick;
    private GameObject[] straightHairSprites;


    public bool dragOn;
    Rigidbody2D rigid;

    GameObject[] hairSprites;

    private void Awake()
    {
        //Debug.Log("Game Controller Awake");
        straightHairSprites = new GameObject[55];


        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        dataManager = GameObject.Find("Data Manager").GetComponent<DataManager>();
        objectManager = GameObject.Find("Object Manager").GetComponent<ObjectManager>();
        //scrollRect = GameObject.Find("Scroll View").GetComponent<ScrollRect>();

        maxClick = 10;//스테이지간 클릭 간격 조절

        Generate();
        //FirstSpriteControl();

        rigid = mainCamera.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        dragOn = false;

        // # 터치 이벤트 
        PCTouchEvent();
        //MobileTouchEvent();

        // # 클릭수에 따른 스프라이트 관리 
        SpriteControl();

    }

    void Generate() 
     {
         for (int index = 0; index < straightHairPrefabs.Length; index++)  //우선만들어놓기
         { 
             straightHairSprites[index] = Instantiate(straightHairPrefabs[index]);
             straightHairSprites[index].SetActive(false);
         }
     }

    /*void Generate()
    {
        InstantiateObject(straightHairSprites, straightHairPrefabs);
    }

    void InstantiateObject(GameObject[] hairSprites, GameObject[] hairPrefabs)
    {
        for (int index = 0; index < hairSprites.Length; index++)
        {
            hairSprites[index] = Instantiate(hairPrefabs[index]);
            hairSprites[index].SetActive(false);
        }
    }*/


    void MobileTouchEvent()
    {
        //터치 시에
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                if (!EventSystem.current.IsPointerOverGameObject(i))
                {
                    tempTouchs = Input.GetTouch(i);
                    if (tempTouchs.phase == TouchPhase.Began)
                    {
                        text.text = rigid.mass + "," + rigid.drag;
                        touchPos = mainCamera.ScreenToWorldPoint(tempTouchs.position);


                        if (!dragOn)
                        {
                            DataControl();
                            SprayControl(touchPos);
                        }
                    }

                }
            }
        }
    }

    void PCTouchEvent()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                //Debug.Log("클릭 수 : " + dataManager.data.clickCount[dataManager.currentStage]);

                Vector2 mousePosition = Input.mousePosition;
                mousePosition = mainCamera.ScreenToWorldPoint(mousePosition);
                text.text = rigid.mass + "," + rigid.drag;


                DataControl();
                SprayControl(mousePosition);
            }
        }
    }
    void DataControl()
    {
        dataManager.data.clickCount[dataManager.currentStage] += 1;
        dataManager.Save();
    }
    void SprayControl(Vector2 pos)
    {
        GameObject hairSpray = objectManager.MakeObj("HairSpray");
        hairSpray.transform.position = pos;
        hairSpray.SetActive(true);
        HairSpray h = hairSpray.GetComponent<HairSpray>();
        h.StartSpray();
    }

    
    void SpriteControl()
    {
        int clickCount = dataManager.data.clickCount[dataManager.currentStage];
        int clickStage = dataManager.data.clickStage[dataManager.currentStage];
        int maxCount = maxClick * clickStage;

        //Debug.Log("스테이지 : " +dataManager.currentClickStage);
        //Debug.Log("현재 클릭수 : " +clickCount);
        //Debug.Log("한계 : " + maxClick);

        //Debug.Log("clickCount : " + clickCount + " maxCount : " + maxCount);
        if (clickCount < maxCount) //아무 일도 일어나지 않음
            return;
        else if (clickCount >= maxCount) //이때 생성될 프리팹 이미지 변경?
        {
            Debug.Log("생성");
            InstantiateObject();
            dataManager.data.clickStage[dataManager.currentStage]++;
            dataManager.Save();
            //progress바 올리기
        }
    }

    void InstantiateObject()
    {
        Debug.Log("머리 생성합니다.");
        straightHairSprites[straightHairIndex].transform.SetParent(hairParent.transform); //hierarchy 창에서 위치 설정


        RectTransform rc = straightHairSprites[straightHairIndex].GetComponent<RectTransform>();
        rc.localScale = new Vector3(1, 1, 1); //scale 1로 설정

        if (straightHairIndex != 0)
        {
            straightHairSprites[straightHairIndex - 1].SetActive(false); //이전 스프라이트는 비활성화
            //Debug.Log("인덱스 " + straightHairIndex + "-1 비활성화했음");
        }


        straightHairSprites[straightHairIndex].SetActive(true); //새로운 스프라이트 활성화
        //Debug.Log("인덱스 " + straightHairIndex + " 활성화했음");

        straightHairIndex++;
    }

    /*void ObjectActivate(GameObject[] hairSprites, int index)
    {
        Debug.Log("머리 생성합니다.");
        hairSprites[index].transform.SetParent(hairParent.transform); //hierarchy 창에서 위치 설정


        RectTransform rc = hairSprites[index].GetComponent<RectTransform>();
        rc.localScale = new Vector3(1, 1, 1); //scale 1로 설정

        if (index != 0) 
            hairSprites[index - 1].SetActive(false); //이전 스프라이트는 비활성화

        hairSprites[index].SetActive(true); //새로운 스프라이트 활성화

        index++;
    }*/


    public void MassUp()
    {
        rigid.mass += 0.02f;
    }
    public void MassDown()
    {
        rigid.mass -= 0.02f;
    }
    public void LinearUp()
    {
        rigid.drag += 0.1f;
    }
    public void LinearDown()
    {
        rigid.drag -= 0.1f;
    }
}