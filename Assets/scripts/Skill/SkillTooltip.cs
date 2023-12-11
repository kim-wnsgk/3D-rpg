using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Kryz.CharacterStats;


public class SKillTooltip : MonoBehaviour
{
    [SerializeField] Text SkillNameText;
    [SerializeField] Text CategoryText;
    [SerializeField] Text ExplanationText;
    [SerializeField] Text DamageText;
    [SerializeField] Text CoolTipText;


    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void ShowTooltip(SkillSet skillset)
    {
        SkillNameText.text = skillset.SkillName;
        CategoryText.text = skillset.retCategory();
        ExplanationText.text = skillset.Explanation;
        DamageText.text = skillset.retDmg().ToString("F2") + " % Damage";
        CoolTipText.text = skillset.retCool().ToString("F1") + " Cooldown";
        gameObject.SetActive(true);
    }

    public void RefreshTooltip(SkillSet skillset)
    {

        skillset.CalculateDamage();
        skillset.CalculateCooltime();
        
    }

    public void Reset(SkillSet skillset)
    {
        skillset.Reseting();
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }


    
}

