using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HairSelection : MonoBehaviour
{
    public GameObject genderPanel;
    public GameObject coverPanel;

    public void OnClickStylekButton() 
    {
        genderPanel.SetActive(!genderPanel.activeSelf);
        coverPanel.SetActive(!coverPanel.activeSelf);
    }

    public void OnClickBackButton() 
    {
        SceneManager.LoadScene("Start Scene");
    }

    public void OnClickCloseButton()
    {
        SceneManager.LoadScene("Game Scene");
    }

}
