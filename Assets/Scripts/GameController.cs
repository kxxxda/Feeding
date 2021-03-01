using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    public GameObject backGroundPrefab;     //배경 프리팹 1개 선언하고 배경 바꿔주기 --> 전부 프리팹화 하면 너무 많아짐
    public GameObject backGroundParent;     //배경의 부모
    public Image manImage;
    public Image hairImage;
    public Image hair47;
    public Image longHair;

    public Text text;

    private Camera mainCamera;
    private DataManager dataManager;
    private ObjectManager objectManager;
    private CameraMove cameraMove;
    private Touch tempTouchs;
    private Vector2 touchPos;
    private int maxClick;


    private void Awake()
    {
        //Debug.Log("Game Controller Awake");

        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        dataManager = GameObject.Find("Data Manager").GetComponent<DataManager>();
        objectManager = GameObject.Find("Object Manager").GetComponent<ObjectManager>();
        cameraMove = mainCamera.GetComponent<CameraMove>();

        maxClick = 10;//스테이지간 클릭 간격 조절
    }

    private void Start()
    {
        HairRecordsLoad();  //머리 이전 기록 가져오기
        BackGroungRecordsLoad();
    }
    void HairRecordsLoad()
    {
        int hairIndex = dataManager.data.clickStage;
        if (hairIndex < 47)
        {
            string path = "hair/ManDown/DawnHair" + hairIndex;
            Debug.Log(path);
            hairImage.sprite = Resources.Load(path, typeof(Sprite)) as Sprite;
        }
        else if (hairIndex >= 47 && hairIndex <= 92)
        {
            hairImage.gameObject.SetActive(false);
            Hair20Stretch(hair47);
        }
        else
        {
            hair47.gameObject.SetActive(false);
            Hair20Stretch(longHair);
        }

        return;
    }

    void Hair20Stretch(Image hair)
    {
        hair.gameObject.SetActive(true);

        RectTransform rectTrans = hair.rectTransform;
        rectTrans.sizeDelta = new Vector2(rectTrans.sizeDelta.x, rectTrans.sizeDelta.y + 20);

        return;
    }

    void BackGroungRecordsLoad()
    {
        int index = dataManager.data.clickStage;

        for (int i = 1; i < index; i++)
        {
            BackGroundObjectSetting(i);
        }
    }

    void BackGroundObjectSetting(int i)
    {
        GameObject backGround = Instantiate(backGroundPrefab);
        backGround.transform.SetParent(backGroundParent.transform);
        backGround.transform.localScale = new Vector3(1, 1, 1);
        backGround.transform.localPosition = new Vector3(0, 1151.9f + (384 * (float)i), 0);
        cameraMove.maxBoundary += 2;
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
            dataManager.data.clickStage++;
            dataManager.Save();
            InstantiateObject();
            //progress바 올리기
        }
    }

    void InstantiateObject()
    {
        Debug.Log("머리 생성합니다.");
        HairRecordsLoad();
        BackGroundObjectSetting(dataManager.data.clickStage - 1);
    }
}