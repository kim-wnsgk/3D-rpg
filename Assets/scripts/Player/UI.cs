using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField]
    public Player player;

    public Slider hpbar;
    public TextMeshProUGUI hp;
    public Slider mpbar;
    public TextMeshProUGUI mp;
    public Slider expbar;
    public TextMeshProUGUI exp;

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

    void Update()
    {
        HandleHpBar();
        HandleMpBar();
        HandleExpBar();
    }

    private void HandleHpBar()
    {
        hpbar.value = (float)player.health / (float)player.maxHealth;
        hp.text = player.health.ToString();
    }

    private void HandleMpBar()
    {
        mpbar.value = (float)player.mana / (float)player.maxMana;
        mp.text = player.mana.ToString();
    }

    private void HandleExpBar()
    {
        expbar.value = (float)player.exp / (float)(100f + player.level * 10);
        exp.text = player.level.ToString() + " Level";
    }
}
