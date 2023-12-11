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
        // 스킬 창 업데이트
        skillPointText.text = "Skill Points: " + skillPoints;

        for (int i = 0; i < skillSlots.Length; i++)
        {
            skillSlots[i].UpdateSlot();
        }
    }

    public void LevelUpSkill(int skillIndex)
    {
        // 스킬 레벨 업 처리
        if (skillPoints > 0)
        {
            skillPoints--;
            skillSlots[skillIndex].LevelUp();
            UpdateSkillWindow();
        }
    }

    public void CloseWindow()
    {
        // 스킬 창 닫기
        gameObject.SetActive(false);
    }
}