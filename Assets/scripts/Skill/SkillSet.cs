
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public enum Category
{
    SingleTargetSkill,
    AreaAttackSkill,
    MovingAttackSkill,
    BossAttackSkill,

}

[CreateAssetMenu(menuName = "SkillSet")]

public class SkillSet : ScriptableObject
{
    public string SkillName;
    [SerializeField] public Category Category;
    public string Explanation;
    [SerializeField] public int Damage;
    [SerializeField] public float CoolTime;

    [SerializeField, Range(1.0f, 2.0f)] float IncreaseDamage;
    [SerializeField] float DecreaseCool;
    [SerializeField] float LeastCoolTime;

    private float Damaging;
    private float CoolDownTime;
    
    private float roundedResult;
    private int SkillLevel;
    private string cat;

    // Update is called once per frame

    private float retRealCool()
    {
        return CoolTime;
    }
    public void Reseting()
    {
        Damaging = Damage;
        CoolDownTime = CoolTime;
    }
    public float retDmg()
    {
        return Damaging;
    }

    public float retCool()
    {
        return CoolDownTime;
    }

    public string retCategory()
    {
        
        if (Category == Category.SingleTargetSkill)
        {
            cat = "단일 공격형 스킬";
        }
        else if(Category == Category.AreaAttackSkill){
            cat = "광범위 공격형 스킬";
        }
        else if( Category == Category.MovingAttackSkill)
        {
            cat = "이동형 공격 스킬";
        }
        else if(Category == Category.BossAttackSkill)
        {
            cat = "보스 공격형 스킬";
        }
        return cat;
    }
    
    public void CalculateDamage()
    {
        if (SkillLevel == 1)
        {
            Damaging = (float)Damage* IncreaseDamage;
            roundedResult = Mathf.Round(Damaging * 20.0f) / 20.0f;
            Damaging = roundedResult;
        }
        else
        {
            Damaging = Damaging * IncreaseDamage;
            roundedResult = Mathf.Round(Damaging * 20.0f) / 20.0f;

            Damaging = roundedResult;
        }
        

    }

    public void CalculateCooltime()
    {
        if (SkillLevel == 1) { 
            if (CoolTime - DecreaseCool >= LeastCoolTime)
            {
                CoolDownTime = CoolTime - DecreaseCool;
            }
        }   
        else
        {
            if (CoolDownTime - DecreaseCool >= LeastCoolTime)
            {
                CoolDownTime = CoolDownTime - DecreaseCool;
            }

            else
            {
                CoolDownTime = LeastCoolTime;
            }
        }

       
    }
}
