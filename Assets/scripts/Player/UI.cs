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

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        HandleHpBar();
        HandleMpBar();
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
}
