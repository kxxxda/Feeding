using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private float posZ;
    private float posX;
    private Vector3 MouseStart;
    private Touch tempTouchs;
    void Start()
    {
        posZ = transform.position.z;  
        posX = transform.position.x;  
    }

    void Update()
    {
        Moving();
        //MobileMoving();
    }

    public void Moving()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            MouseStart = new Vector3(posX, Input.mousePosition.y, posZ);
            MouseStart = Camera.main.ScreenToWorldPoint(MouseStart);
            MouseStart.z = transform.position.z;

        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 temp = new Vector3();
            var MouseMove = new Vector3(posX, Input.mousePosition.y, posZ);
            MouseMove = Camera.main.ScreenToWorldPoint(MouseMove);
            MouseMove.z = transform.position.z;


            temp = transform.position - (MouseMove - MouseStart);

            if (temp.y > 20)
                temp.y = 20;
            if (temp.y < 0)
                temp.y = 0;

            transform.position = temp;
        }
    }

    void MobileMoving()
    {
        if (Input.touchCount == 1)
        {
            tempTouchs = Input.GetTouch(0);
            if (tempTouchs.phase == TouchPhase.Began)
            {
                MouseStart = new Vector3(posX, Input.mousePosition.y, posZ);
                MouseStart = Camera.main.ScreenToWorldPoint(MouseStart);
                MouseStart.z = transform.position.z;

            }
            else if (tempTouchs.phase == TouchPhase.Moved)
            {
                Vector3 temp = new Vector3();
                var MouseMove = new Vector3(posX, Input.mousePosition.y, posZ);
                MouseMove = Camera.main.ScreenToWorldPoint(MouseMove);
                MouseMove.z = transform.position.z;


                temp = transform.position - (MouseMove - MouseStart);

                if (temp.y > 20)
                    temp.y = 20;
                if (temp.y < 0)
                    temp.y = 0;

                transform.position = temp;
                //temp *= Time.deltaTime ;
                //transform.Translate(temp);
            }
        }
    }
}
