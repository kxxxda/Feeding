using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePause : MonoBehaviour
{
    public GameObject menuSet;
    public GameObject confirmSet;
    public Button pauseButton;

    private DataManager dataManager;

    public Text hairLength;

    private bool bPaused = false;
    public bool paused = false;

    private void Awake()
    {
        dataManager = GameObject.Find("Data Manager").GetComponent<DataManager>();

        paused = false;
    }

    private void Update()
    {
        hairLength.text = dataManager.data.clickStage.ToString() + " M"; //근데 콘텐트의 길이가 머리 길이가 되어서는 안됨 *수정필요
    }

    private void OnApplicationPause(bool pause)//일시정지 기능
    {
        if (pause)
        {
            bPaused = true;
            if(!paused)
                OnClickToggleMenuSetButton();
        }
        else
        {
            if (bPaused)
            {
                bPaused = false;
                if (paused)
                {
                    OnClickToggleMenuSetButton();
                    TogglePause();
                }
            }
        }
    }

    public void TogglePause()
    {
        paused = !paused;
    }
    public void OnClickToggleMenuSetButton()
    {
        menuSet.SetActive(!menuSet.activeSelf);
    }

    public void ConfirmStageInitialize()
    {
        dataManager.InitiateStage();
        dataManager.Save();
        SceneManager.LoadScene("Game Scene test");

    }
    public void StageInitialize()
    {
        confirmSet.SetActive(!confirmSet.activeSelf);
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
