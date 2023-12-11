using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SkillSlot : MonoBehaviour
{
    public Text skillNameText;
    public Text skillLevelText;
    public Text skillDescriptionText;
    public Text skillDamageText;
    public Button levelUpButton;

    public string skillName;
    public int skillLevel = 0;
    public float damageIncreasePercentage = 10f; // ������ ������ ���� ����

    private SkillWindow skillWindow;

    private void Start()
    {
        skillWindow = GetComponentInParent<SkillWindow>();
    }

    public void UpdateSlot()
    {
        skillNameText.text = skillName;
        skillLevelText.text = "Level " + skillLevel;
        skillDescriptionText.text = "Description: This skill deals damage.";
        skillDamageText.text = "Damage: " + CalculateDamage();
        levelUpButton.interactable = skillWindow.skillPoints > 0;
    }

    public void LevelUp()
    {
        if (skillLevel < 5) // �ִ� ���� ���� (��: �ִ� ���� 5)
        {
            skillLevel++;
            UpdateSlot();
        }
    }

    private float CalculateDamage()
    {
        // ������ ���
        return 100f + skillLevel * damageIncreasePercentage;
    }
}
