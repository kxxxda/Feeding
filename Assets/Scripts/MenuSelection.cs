using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuSelection : MonoBehaviour
{
    public GameManager gameManager;
    public void GameStart()
    {
        SceneManager.LoadScene("Menu Scene");
    }
    public void GameExit()
    {
        Application.Quit();
    }

}
