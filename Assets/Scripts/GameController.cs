using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject testingSpritePrefab;

    private Camera mainCamera;
    private DataManager dataManager;
    private ObjectManager objectManager;
    private Touch tempTouchs;
    private Vector2 touchPos;
    private bool touchOn;
    private ScrollRect scrollRect;
    public Text text;

    private int countLimit;

    private void Awake()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        dataManager = GameObject.Find("Data Manager").GetComponent<DataManager>();
        objectManager = GameObject.Find("Object Manager").GetComponent<ObjectManager>();
        scrollRect = GameObject.Find("Scroll View").GetComponent<ScrollRect>();

        countLimit = 10; //초기값은 10
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
            Vector2 mousePosition = Input.mousePosition;
            mousePosition = mainCamera.ScreenToWorldPoint(mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mousePosition, mainCamera.transform.forward);
            if (hit.collider!=null)
            {
                text.text = hit.collider.name;
                Debug.Log(hit.collider.name);
                if (hit.collider.name == "Content")
                {
                    dataManager.data.clickCount[dataManager.currentStage] += 1;
                    dataManager.Save();

                    SprayControl(mousePosition);

                    if (dataManager.data.clickCount[dataManager.currentStage] == countLimit)
                    { //현재 스테이지의 클릭 수가 50을 넘으면
                        SpriteControl(mousePosition);
                        dataManager.data.clickCount[dataManager.currentStage] = 0; //현재 스테이지의 클릭 수 0으로 초기화
                        countLimit += 0; //기준 클릭수 두배로 증가
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
    void SpriteControl(Vector2 pos)
    {
        Debug.Log("스프라이트 생성");
        //GameObject testingSprite = Instantiate(testingSpritePrefab, scrollRect.content.position, scrollRect.content.rotation);
        GameObject testingSprite = Instantiate(Resources.Load<GameObject>("Prefabs/TestingSprite"), scrollRect.content.transform);

    }

}
