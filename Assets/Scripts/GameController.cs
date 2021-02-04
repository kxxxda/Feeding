using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private Camera mainCamera;
    private DataManager dataManager;
    private ObjectManager objectManager;
    private Touch tempTouchs;
    private Vector2 touchPos;
    private bool touchOn;

    private void Awake()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        dataManager = GameObject.Find("Data Manager").GetComponent<DataManager>();
        objectManager = GameObject.Find("Object Manager").GetComponent<ObjectManager>();
    }

    private void Update()
    {
        //터치 시에
        //touchOn = false;
        //if (Input.touchCount > 0)
        //{
        //    for(int i = 0; i < Input.touchCount; i++)
        //    {

        //        tempTouchs = Input.GetTouch(0);
        //        if (tempTouchs.phase == TouchPhase.Began)
        //        {
        //            touchPos = mainCamera.ScreenToWorldPoint(tempTouchs.position);
        //            touchOn = true;
        //            dataManager.data.clickCount[dataManager.currentStage] += 1;
        //            SprayControl(touchPos);
        //        }
        //    }
        //}


        if (Input.GetMouseButtonDown(0))
        {
            dataManager.data.clickCount[dataManager.currentStage] += 1;

            Vector2 mousePosition = Input.mousePosition;
            mousePosition = mainCamera.ScreenToWorldPoint(mousePosition);
            SprayControl(mousePosition);
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
    
}
