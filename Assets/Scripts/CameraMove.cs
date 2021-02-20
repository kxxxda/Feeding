using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private float posZ;
    private float posX;
    private Vector3 MouseStart;

    void Start()
    {
        posZ = transform.position.z;  
        posX = transform.position.x;  
    }

    void Update()
    {
        Moving();
    }

    void Moving()
    {
        if (Input.GetMouseButtonDown(1))
        {
            MouseStart = new Vector3(posX, Input.mousePosition.y, posZ);
            MouseStart = Camera.main.ScreenToWorldPoint(MouseStart);
            MouseStart.z = transform.position.z;

        }
        else if (Input.GetMouseButton(1))
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
        if (Input.GetMouseButtonDown(1))
        {
            MouseStart = new Vector3(posX, Input.mousePosition.y, posZ);
            MouseStart = Camera.main.ScreenToWorldPoint(MouseStart);
            MouseStart.z = transform.position.z;

        }
        else if (Input.GetMouseButton(1))
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
}
