using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private float posZ;
    private float posX;
    public float maxBoundary;
    private Vector3 MouseStart, MouseMove;
    private Touch tempTouchs;
    public Rigidbody2D rigid;
    GamePause gamePause;

    void Awake()
    {
        gamePause = GameObject.Find("GamePause").GetComponent<GamePause>();
        maxBoundary = 0;
    }
    void Start()
    {
        posZ = transform.position.z;
        posX = transform.position.x;
    }

    void Update()
    {
        // # 방향벡터 계산
        if (!gamePause.paused)
            Drag();
            //MobileDrag();

        // # 화면 밖으로 못나가게 지정
        Boundary();
    }

    public void Drag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseStart = new Vector3(posX, Input.mousePosition.y, posZ);
            MouseStart = Camera.main.ScreenToWorldPoint(MouseStart);
            MouseStart.z = transform.position.z;
        }
        else if (Input.GetMouseButton(0))
        {
            MouseMove = new Vector3(posX, Input.mousePosition.y, posZ);
            MouseMove = Camera.main.ScreenToWorldPoint(MouseMove);
            MouseMove.z = transform.position.z;
            Move();
        }
    }

    void MobileDrag()
    {
        if (Input.touchCount == 1)
        {
            tempTouchs = Input.GetTouch(0);
            if (tempTouchs.phase == TouchPhase.Began)
            {
                MouseStart = new Vector3(posX, tempTouchs.position.y, posZ);
                MouseStart = Camera.main.ScreenToWorldPoint(MouseStart);
                MouseStart.z = transform.position.z;

            }
            else if (tempTouchs.phase == TouchPhase.Moved)
            {
                MouseMove = new Vector3(posX, tempTouchs.position.y, posZ);
                MouseMove = Camera.main.ScreenToWorldPoint(MouseMove);
                MouseMove.z = transform.position.z;
                Move();
            }
        }
    }

    void Move()
    {
        transform.position = transform.position - (MouseMove - MouseStart);
        rigid.AddForce((MouseStart - MouseMove).normalized * 0.25f, ForceMode2D.Impulse);
        //transform.position += temp * 0.5f* Time.deltaTime;
    }
    void Boundary()
    {
        if (transform.position.y > maxBoundary)
            transform.position = new Vector3(transform.position.x, maxBoundary, transform.position.z);
        if (transform.position.y < 0)
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }

}