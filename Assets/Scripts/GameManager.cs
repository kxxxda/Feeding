using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int stage;
    bool isWoman;
    bool isMan;

    private void Awake()
    {
        //Debug.Log("GameManager Awake");
        isWoman = false;
        isMan = false;
    }

    public void SelectStage(int sta)
    {
        stage = sta;
    }
    public void SelectMan()
    {
        isWoman = false;
        isMan = true;
    }
    public void SelectWoman()
    {
        isWoman = true;
        isMan = false;
    }
}
