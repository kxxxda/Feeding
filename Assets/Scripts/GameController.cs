using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    //배경 관련
    public GameObject[] backGroundPrefab;
    public GameObject backGroundParent;     //배경의 부모
    private int boundary;                   //화면 바운더리 (이 수치를 지나면 배경 생성)
    private int blockCount;                 //몇개째 배경이 붙었는지 카운트

    //머리 관련
    public Image hairImage;
    public GameObject hair47;
    public GameObject longHair;
    private RectTransform longHairRectTrans;
    private int hairIndex;

    //위자 관련
    public GameObject chair;
    public GameObject chairLeg;
    private int chairIndex;

    //사람 관련
    public Image manImage;
    public GameObject man;


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
        longHairRectTrans = longHair.GetComponent<RectTransform>();


        maxClick = 10;//스테이지간 클릭 간격 조절

        boundary = 2284;//머리가 디폴트 배경화면을 나가는 시점 //longHair의 position.y
        blockCount = 0;

        hairIndex = 0;
        chairIndex = 0;
    }

    private void Start()
    {
        InstantiateObject();
    }
    
    void ManRecorsdsLoad()
    {
        string path = "man/";

        int manIndex = dataManager.data.clickStage;
        if (manIndex < 26)
            path += "Mansad";
        else if (manIndex >= 26 && manIndex < 33)
            path += "Mansoso";
        else
            path += "Mansmile";

        manImage.sprite = Resources.Load(path, typeof(Sprite)) as Sprite;
    }
    void HairRecorsdsLoad()  //머리 이전 기록 가져오기
    {
        while(hairIndex < dataManager.data.clickStage)
        {
            if (hairIndex < 47)
            {
                string path = "hair/ManDown/DawnHair" + hairIndex;
                hairImage.sprite = Resources.Load(path, typeof(Sprite)) as Sprite;
            }
            else if (hairIndex == 47)
            {
                hairImage.gameObject.SetActive(false);
                hair47.gameObject.SetActive(true);
            }
            else if (hairIndex > 47 && hairIndex <= 92)
            {
                Obj20Stretch(hair47);
            }
            else if (hairIndex == 93)
            {
                hair47.gameObject.SetActive(false);
                longHair.gameObject.SetActive(true);
            }
            else
            {
                Obj20Stretch(longHair);
                Put20Up();
            }
            hairIndex++;
        }

        return;
    }
    

    void Obj20Stretch(GameObject obj)
    {
        RectTransform rectTrans = obj.GetComponent<RectTransform>();
        rectTrans.sizeDelta = new Vector2(rectTrans.sizeDelta.x, rectTrans.sizeDelta.y + 20);
        
        return;
    }

    void ChairRecordsLoad()
    {
        while(chairIndex< dataManager.data.clickStage-93)
        {
            Obj20Stretch(chairLeg);                                                                                                                                 //의자 목 20늘리기
            chair.transform.localPosition = (new Vector3(chair.transform.localPosition.x, chair.transform.localPosition.y + 20, chair.transform.localPosition.z));  //의자 20올리기
            chairIndex++;
        }
    }

    void BackGroungRecordsLoad()
    {
        while ((int)longHairRectTrans.sizeDelta.y > boundary)
        {
            boundary += 384;
            BackGroundObjectSetting();
        }
    }
    void BackGroundObjectSetting()
    {
        int num = BackGroundType();
        GameObject backGround = Instantiate(backGroundPrefab[num]);
        backGround.transform.SetParent(backGroundParent.transform);
        backGround.transform.localScale = new Vector3(1, 1, 1);
        backGround.transform.localPosition = new Vector3(0, 1151.9f + 384 * blockCount, 0);
        cameraMove.maxBoundary += 2;
        blockCount++;
    }
    int BackGroundType()//각 배경마다 몇개씩 할건지 정해야함
    {
        if (blockCount == 0)
            return 0;
        else if (blockCount > 0 && blockCount < 16)
            return 1;
        else if (blockCount == 16)
            return 2;
        else if (blockCount > 16 && blockCount < 32)
            return 3;
        else if (blockCount == 32)
            return 4;
        else if (blockCount > 32 && blockCount < 48)
            return 5;
        else if (blockCount == 48)
            return 6;
        else if (blockCount > 48 && blockCount < 63)
            return 7;
        else if (blockCount == 63)
            return 8;
        else
            return 9;

    }
    private void Update()
    {
        // # 터치 이벤트 
        PCTouchEvent();
        //MobileTouchEvent();

        // # 클릭수에 따른 스프라이트 관리 
        SpriteControl();
        //Debug.Log(longHairRectTrans.sizeDelta.y);
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
            //Debug.Log("생성");
            dataManager.data.clickStage++;
            dataManager.Save();
            InstantiateObject();
        }
    }

    void InstantiateObject()
    {
        //Debug.Log("머리 생성합니다.");
        ManRecorsdsLoad();
        HairRecorsdsLoad();
        ChairRecordsLoad();
        BackGroungRecordsLoad();
    }
    
    void Put20Up()
    {
        
        man.transform.localPosition = (new Vector3(man.transform.localPosition.x, man.transform.localPosition.y + 20, man.transform.localPosition.z));
    }
}