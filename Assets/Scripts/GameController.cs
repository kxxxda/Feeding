using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    public Image manImage;
    public Image hairImage;
    public Image longHair;

    public Text text;

    private Camera mainCamera;
    private DataManager dataManager;
    private ObjectManager objectManager;
    private Touch tempTouchs;
    private Vector2 touchPos;
    private int maxClick;


    private void Awake()
    {
        //Debug.Log("Game Controller Awake");

        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        dataManager = GameObject.Find("Data Manager").GetComponent<DataManager>();
        objectManager = GameObject.Find("Object Manager").GetComponent<ObjectManager>();

        maxClick = 10;//스테이지간 클릭 간격 조절
    }

    private void Start()
    {
        HairRecordsLoad();  //머리 이전 기록 가져오기
    }
    void HairRecordsLoad()
    {
        int hairIndex = dataManager.data.clickStage;

        if (hairIndex >= 52)
        {
            hairImage.gameObject.SetActive(false);
            longHair.gameObject.SetActive(true);
            RectTransform rectTrans = longHair.rectTransform;
            Debug.Log(rectTrans.position.y);
            Debug.Log(rectTrans.position.y + 20);

            rectTrans.sizeDelta = new Vector2(rectTrans.sizeDelta.x, rectTrans.sizeDelta.y + 20);
            rectTrans.position = new Vector3(rectTrans.position.x, rectTrans.position.y + 20, rectTrans.position.z);

           

            return;
        }
        string path = "hair/ManDown/DawnHair" + hairIndex;
        Debug.Log(path);
        hairImage.sprite = Resources.Load(path, typeof(Sprite)) as Sprite;
    }


    private void Update()
    {
        // # 터치 이벤트 
        PCTouchEvent();
        //MobileTouchEvent();

        // # 클릭수에 따른 스프라이트 관리 
        SpriteControl();

    }

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
                        touchPos = mainCamera.ScreenToWorldPoint(tempTouchs.position);
                        DataControl();
                        SprayControl(touchPos);
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
                Vector2 mousePosition = Input.mousePosition;
                mousePosition = mainCamera.ScreenToWorldPoint(mousePosition);

                DataControl();
                SprayControl(mousePosition);
            }
        }
    }
    void DataControl()
    {
        dataManager.data.clickCount += 1;
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
        int clickCount = dataManager.data.clickCount;
        int clickStage = dataManager.data.clickStage;
        int maxCount = maxClick * clickStage;

        if (clickCount < maxCount) //아무 일도 일어나지 않음
            return;
        else if (clickCount >= maxCount) //이때 생성될 프리팹 이미지 변경?
        {
            Debug.Log("생성");
            InstantiateObject();
            dataManager.data.clickStage++;
            dataManager.Save();
            //progress바 올리기
        }
    }

    void InstantiateObject()
    {
        Debug.Log("머리 생성합니다.");
        HairRecordsLoad();
    }
}