using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    public GameObject testingSpritePrefab;
    public GameObject testingBackgroundSpritePrefab;
    public Transform parent1Transform;
    //public Transform parent2Transform;
    public Text text;

    private Camera mainCamera;
    private DataManager dataManager;
    private ObjectManager objectManager;
    private ScrollRect scrollRect;
    private Touch tempTouchs;
    private Vector2 touchPos;
    private bool touchOn;
    private int maxClick;
    private bool isViewport;

    private void Awake()
    {
        //Debug.Log("Game Controller Awake");
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        dataManager = GameObject.Find("Data Manager").GetComponent<DataManager>();
        objectManager = GameObject.Find("Object Manager").GetComponent<ObjectManager>();
        //scrollRect = GameObject.Find("Scroll View").GetComponent<ScrollRect>();

        maxClick = 10;//스테이지간 클릭 간격 조절

        FirstSpriteControl();
    }

    private void Update()
    {
        isViewport = false;
        //콜라이더가 아닌 다른 방법으로 해결함
        TouchEvent();

        MobileTouch();

        // # 클릭수에 따른 스프라이트 관리 
        SpriteControl();

    }

    void MobileTouch()
    {
        //터치 시에.
        touchOn = false;
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                tempTouchs = Input.GetTouch(i);
                if (tempTouchs.phase == TouchPhase.Began)
                {
                    touchPos = mainCamera.ScreenToWorldPoint(tempTouchs.position);
                    touchOn = true;

                    RaycastHit2D hit = Physics2D.Raycast(touchPos, mainCamera.transform.forward);
                    text.text = dataManager.data.clickCount[dataManager.currentStage].ToString();

                    dataManager.data.clickCount[dataManager.currentStage] += 1;
                    dataManager.Save();
                    SprayControl(touchPos);

                }

            }
        }
    }

    // # 스프레이 뿌리기 애니 + 클릭수 증가
    public void ppTouchEvent()//트리거로 돌아가는 함수
    {
        isViewport = true;

        Debug.Log("바꾸면 다시 하지 ");
        Vector2 mousePosition = Input.mousePosition;
        mousePosition = mainCamera.ScreenToWorldPoint(mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider!=null)
            Debug.Log(hit.collider.name);
            //if (hit.collider.name == "Content 1" || hit.collider.name == "Content 2")

            dataManager.data.clickCount[dataManager.currentStage] += 1;
                dataManager.Save();

                SprayControl(mousePosition);
        
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


        //            if (hit.collider.name == "GameSprite")
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
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                //Debug.Log("클릭 수 : " + dataManager.data.clickCount[dataManager.currentStage]);

                Vector2 mousePosition = Input.mousePosition;
                mousePosition = mainCamera.ScreenToWorldPoint(mousePosition);

                RaycastHit2D hit = Physics2D.Raycast(mousePosition, mainCamera.transform.forward);
                if (hit.collider != null)
                {
                    /*디버깅용*/
                    text.text = hit.collider.name;
                    Debug.Log(hit.collider.name);
                    /*디버깅용*/


                    if (hit.collider.name == "GameSprite")
                    {
                        dataManager.data.clickCount[dataManager.currentStage] += 1;
                        dataManager.Save();

                        SprayControl(mousePosition);
                    }

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
        int maxCount = maxClick * clickStage;

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
            dataManager.Save();
            //progress바 올리기
        }
    }

    void InstantiateObject()
    {
        GameObject testingBackground1Sprite = Instantiate(testingBackgroundSpritePrefab);
        testingBackground1Sprite.transform.SetParent(parent1Transform.transform);

        GameObject testingSprite1 = Instantiate(testingSpritePrefab);
        testingSprite1.transform.SetParent(parent1Transform.transform);


        //GameObject testingBackground2Sprite = Instantiate(testingBackgroundSpritePrefab);
        //testingBackground2Sprite.transform.SetParent(parent2Transform.transform);

        //GameObject testingSprite2 = Instantiate(testingSpritePrefab);
        //testingSprite2.transform.SetParent(parent2Transform.transform);

    }

    
}
