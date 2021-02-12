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
    

    private void Awake()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        dataManager = GameObject.Find("Data Manager").GetComponent<DataManager>();
        objectManager = GameObject.Find("Object Manager").GetComponent<ObjectManager>();
        scrollRect = GameObject.Find("Scroll View").GetComponent<ScrollRect>();

    }

    private void Update()
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
                Debug.Log(hit.collider.name);
                if (hit.collider.name == "Content 1" || hit.collider.name == "Content 2")
                {
                    dataManager.data.clickCount[dataManager.currentStage] += 1;
                    dataManager.Save();

                    SprayControl(mousePosition);
                    SpriteControl();

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
    void SpriteControl()
    {
        if (dataManager.data.clickCount[dataManager.currentStage] < 10) //아무 일도 일어나지 않음
            return;
        //else if (dataManager.data.clickCount[dataManager.currentStage] < 20) //이때 생성될 프리팹 이미지 변경?


        GameObject testingSprite = Instantiate(testingSpritePrefab);
        testingSprite.transform.SetParent(parent1Transform.transform);

        GameObject testingBackground1Sprite = Instantiate(testingBackgroundSpritePrefab);
        testingBackground1Sprite.transform.SetParent(parent1Transform.transform);

        GameObject testingBackground2Sprite = Instantiate(testingBackgroundSpritePrefab);
        testingBackground2Sprite.transform.SetParent(parent2Transform.transform);
    }
    void InstantiateObject(int num)
    {
        for(int i = 0;i< num;i++)
        {
            GameObject testingSprite = Instantiate(testingSpritePrefab);
            testingSprite.transform.SetParent(parent1Transform.transform);
        }
    }

   
}
