using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject testingSpritePrefab;
    public GameObject testingBackgroundSpritePrefab;
    public Transform parent1Transform;
    public Transform parent2Transform;
    public Text text;

    private Camera mainCamera;
    private DataManager dataManager;
    private ObjectManager objectManager;
    private ScrollRect scrollRect;
    private Touch tempTouchs;
    private Vector2 touchPos;
    private bool touchOn;
    private int maxClick;

    private void Awake()
    {
        //Debug.Log("Game Controller Awake");
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        dataManager = GameObject.Find("Data Manager").GetComponent<DataManager>();
        objectManager = GameObject.Find("Object Manager").GetComponent<ObjectManager>();
        scrollRect = GameObject.Find("Scroll View").GetComponent<ScrollRect>();

        maxClick = 10;//스테이지간 클릭 간격 조절

        FirstSpriteControl();
    }

    private void Update()
    {
        // # 스프레이 뿌리기 애니 + 클릭수 증가
        TouchEvent();

        // # 클릭수에 따른 스프라이트 관리 
        SpriteControl();

        // # 스테이지 클리어 관리
        StageControl();
    }

    void TouchEvent()
    {
        //터치 시에
        //touchOn = false;
        //if (Input.touchCount > 0)
        //{
        //    for (int i = 0; i < Input.touchCount; i++)
        //    {

        //        tempTouchs = Input.GetTouch(i);
        //        if (tempTouchs.phase == TouchPhase.Began)
        //        {
        //            touchPos = mainCamera.ScreenToWorldPoint(tempTouchs.position);
        //            touchOn = true;

        //            RaycastHit2D hit = Physics2D.Raycast(touchPos, mainCamera.transform.forward);
        //            text.text = hit.collider.name + dataManager.data.clickCount[dataManager.currentStage];


        //            if (hit.collider.name == "Content")
        //            {
        //                dataManager.data.clickCount[dataManager.currentStage] += 1;
        //                dataManager.Save();

        //                SprayControl(touchPos);
        //            }
        //        }

        //    }
        //}


        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("클릭 수 : " + dataManager.data.clickCount[dataManager.currentStage]);

            Vector2 mousePosition = Input.mousePosition;
            mousePosition = mainCamera.ScreenToWorldPoint(mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mousePosition, mainCamera.transform.forward);
            if (hit.collider != null)
            {
                text.text = hit.collider.name;
                //Debug.Log(hit.collider.name);
                if (hit.collider.name == "Content 1" || hit.collider.name == "Content 2")
                {
                    dataManager.data.clickCount[dataManager.currentStage] += 1;
                    dataManager.Save();

                    SprayControl(mousePosition);
                }

            }

        }
    }

    void SprayControl(Vector2 pos)
    {
        GameObject hairSpray = objectManager.MakeObj("HairSpray");
        hairSpray.transform.position = pos;
        hairSpray.SetActive(true);
        HairSpray h = hairSpray.GetComponent<HairSpray>();
        h.StartSpray();
    }

    void FirstSpriteControl()
    {
        for (int i = 1; i < dataManager.data.clickStage[dataManager.currentStage]; i++)
            InstantiateObject();
    }
    void SpriteControl()
    {
        int clickCount = dataManager.data.clickCount[dataManager.currentStage];
        int clickStage = dataManager.data.clickStage[dataManager.currentStage];
        int maxCount = maxClick* clickStage;

        //Debug.Log("스테이지 : " +dataManager.currentClickStage);
        //Debug.Log("현재 클릭수 : " +clickCount);
        //Debug.Log("한계 : " + maxClick);

        if (clickCount < maxCount) //아무 일도 일어나지 않음
            return;
        else if (clickCount >= maxCount) //이때 생성될 프리팹 이미지 변경?
        {
            Debug.Log("생성");
            InstantiateObject();
            dataManager.data.clickStage[dataManager.currentStage]++;
        }
    }

    void InstantiateObject()
    {
        if (dataManager.data.clickCount[dataManager.currentStage] < 10) //아무 일도 일어나지 않음
            return;
        //else if (dataManager.data.clickCount[dataManager.currentStage] < 20) //이때 생성될 프리팹 이미지 변경?




        GameObject testingBackground1Sprite = Instantiate(testingBackgroundSpritePrefab);
        testingBackground1Sprite.transform.SetParent(parent1Transform.transform);

        GameObject testingSprite1 = Instantiate(testingSpritePrefab);
        testingSprite1.transform.SetParent(parent1Transform.transform);


        GameObject testingBackground2Sprite = Instantiate(testingBackgroundSpritePrefab);
        testingBackground2Sprite.transform.SetParent(parent2Transform.transform);

        GameObject testingSprite2 = Instantiate(testingSpritePrefab);
        testingSprite2.transform.SetParent(parent2Transform.transform);

    }

    void StageControl()
    {
        //해금 이벤트 처리 
        if (dataManager.data.clickStage[dataManager.currentStage] == 50) ;

    }
}
