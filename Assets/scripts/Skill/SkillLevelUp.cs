using UnityEngine;
using UnityEngine.UI;

public class SkillLevelUp : MonoBehaviour
{

    [SerializeField]
    private Skill[] talents = default;

    public LevelConfig levelConfig;

    [SerializeField]
    private Skill[] unlockedByDefault = default;


    private int points;

    public void CalculatePoint(int level)
    {
        
        if (level == 1)
        {
            points = 5;
            
        }
        else
        {
            points += levelConfig.GetSP(level);
            
        }


        UpdateTalentPointText();
        
    }
    public Skill[] MyTalents
    {
        get => talents;
    }

    public int MyPoints
    {
        get => points;
        set
        {
            points = value;


            UpdateTalentPointText();
        }
    }

    void Start()
    {
        ResetTalents();
    }




    [SerializeField]
    private Text talentPointText = default;


    public void TryUseTalent(Skill talent)
    {
        
       
        if (points > 0 && talent.Click())
        {
            Debug.Log("clicked");
            points--;

            UpdateTalentPointText();
        }


        if (points == 0)
        {

            foreach (Skill t in talents)
            {

                if (t.MyCurrentCount == 0)
                {

                    t.Lock();
                }
            }

        }

    }


    private void ResetTalents()
    {

        UpdateTalentPointText();


        foreach (Skill talent in talents)
        {

            talent.Lock();
        }


        foreach (Skill talent in unlockedByDefault)
        {

            talent.Unlock();
        }
    }


    private void UpdateTalentPointText()
    {
        
        talentPointText.text = "SP : " + MyPoints.ToString();
    }
}
