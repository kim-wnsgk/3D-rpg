using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveStage : MonoBehaviour
{
    public Player player;

    void Awake()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        // players 배열이 비어있지 않은 경우, 첫 번째 플레이어를 선택
        if (players.Length > 0)
        {
            player = players[0].GetComponent<Player>();
        }
        else
        {
            // players 배열이 비어있는 경우, 원하는 처리를 수행하거나 예외 처리를 추가할 수 있습니다.
            Debug.LogError("Player not found in the scene.");
        }
    }
    public void school()
    {
        player.health = player.maxHealth;
        LoadingManager.LoadScene("school");
    }

    public void Move1()
    {
        LoadingManager.LoadScene("Stage1");
    }

    public void Move2()
    {
        LoadingManager.LoadScene("Stage2");
    }
    public void Move3()
    {
        LoadingManager.LoadScene("Stage3");
    }
    public void Move4()
    {
        LoadingManager.LoadScene("Stage4");
    }
    public void Move5()
    {
        LoadingManager.LoadScene("Stage5");
    }
    public void Move6()
    {
        LoadingManager.LoadScene("Stage6");
    }
    public void Move7()
    {
        LoadingManager.LoadScene("Stage7");
    }
    public void Move8()
    {
        LoadingManager.LoadScene("Stage8");
    }
    public void Move9()
    {
        LoadingManager.LoadScene("Stage9");
    }
    public void Move10()
    {
        LoadingManager.LoadScene("Stage10");
    }
}
