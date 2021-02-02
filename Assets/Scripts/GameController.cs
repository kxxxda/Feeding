using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject hairSpray;
    private Camera mainCamera;
    public DataManager dataManager;

    private void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            dataManager.data.clickCount1+=1;
            Vector2 mousePosition = Input.mousePosition;
            mousePosition = mainCamera.ScreenToWorldPoint(mousePosition);

            hairSpray.transform.position = mousePosition;
            hairSpray.SetActive(true);
            HairSpray h = hairSpray.GetComponent<HairSpray>();
            h.StartSpray();
        }
    }

}
