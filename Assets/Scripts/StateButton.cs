using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateButton : MonoBehaviour
{
    public GameObject statePanel; //패널받아서

    public void OnClickButton()
    {
        if (statePanel != null) //?
        {
            bool isActive = statePanel.activeSelf; //상태 받아서 
            statePanel.SetActive(!isActive);
        }  

    }
}
