using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillWindow : MonoBehaviour
{
    public Text skillPointText;
    public SkillSlot[] skillSlots;

    public int skillPoints = 0;

    private void Start()
    {
        UpdateSkillWindow();
    }

    public void UpdateSkillWindow()
    {
        // ��ų â ������Ʈ
        skillPointText.text = "Skill Points: " + skillPoints;

        for (int i = 0; i < skillSlots.Length; i++)
        {
            skillSlots[i].UpdateSlot();
        }
    }

    public void LevelUpSkill(int skillIndex)
    {
        // ��ų ���� �� ó��
        if (skillPoints > 0)
        {
            skillPoints--;
            skillSlots[skillIndex].LevelUp();
            UpdateSkillWindow();
        }
    }

    public void CloseWindow()
    {
        // ��ų â �ݱ�
        gameObject.SetActive(false);
    }
}