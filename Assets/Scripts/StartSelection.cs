using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartSelection : MonoBehaviour
{
    public void GameStart()
    {
        SceneManager.LoadScene("Menu Scene");
    }
    public void GameExit()
    {
        Application.Quit();
    }

}
