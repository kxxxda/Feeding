using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    bool isWoman;
    bool isMan;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    public void SelectStage(int stage)
    {
        player.stage = stage;
    }
    public void SelectMan()
    {
        player.isWoman = false;
        player.isMan = true;
    }
    public void SelectWoman()
    {
        player.isWoman = true;
        player.isMan = false;
    }
}
