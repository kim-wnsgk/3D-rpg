using Kryz.CharacterStats;
using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class StatLevelUp : MonoBehaviour
{

    public LevelConfig levelConfig;

    [SerializeField] Text Level;
    [SerializeField] Text StrLevel;
    [SerializeField] Text DexLevel;
    [SerializeField] Text HPLevel;
    [SerializeField] Text SP;
    [SerializeField] StatPanel statPanel;

    [SerializeField] Image StrUpicon;
    [SerializeField] Image StrDownicon;
    [SerializeField] Image DexUpicon;
    [SerializeField] Image DexDownicon;
    [SerializeField] Image HpUpicon;
    [SerializeField] Image HpDownicon;

    Character c1;

    private int points;
    private int syslevel;
    
    private int Strpt;
    private int Dexpt;
    private int HPpt;

    
    public void CalculatePoint(int level, Character c)
    {
        
        if (level == 1)
        {
            points = 5;

        }
        else
        {
            points += levelConfig.GetSP(level);

        }

        c1 = c;
        syslevel = level; 



        UpdateUI();
        UpdateTalentPointText();

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
        unLock();
        DownLock();
        Strpt = 0;
        Dexpt = 0;
        HPpt = 0;
    }

   

    

    public void Lock()
    {

        StrUpicon.color = Color.gray;
        DexUpicon.color = Color.gray;
        HpUpicon.color = Color.gray;

    }

    public void unLock()
    {
        StrUpicon.color = Color.white;
        DexUpicon.color = Color.white;
        HpUpicon.color = Color.white;
    }

    public void DownLock()
    {
        StrDownicon.color = Color.gray;
        DexDownicon.color = Color.gray;
        HpDownicon.color = Color.gray;
    }

    public void IncreaseStrength()
    {
        if (points > 0)
        {
            Strpt++;
            points--;
            StrDownicon.color = Color.white;
        }

        UpdateUI();
    }

    public void DecreaseStrength()
    {
        if (Strpt > 0)
        {
            Strpt--;
            points++;
            
        }
        else
        { StrDownicon.color = Color.gray; }

        UpdateUI();
    }

    public void IncreaseDexterity()
    {
        if (points > 0 )
        {
            Dexpt++;
            points--;
            DexDownicon.color = Color.white;
        }

        UpdateUI();
    }

    public void DecreaseDexterity()
    {
        if (Dexpt > 0)
        {
            Dexpt--;
            points++;
            
        }
        else
        {
            DexDownicon.color = Color.gray;
        }
        UpdateUI();
    }

    public void IncreaseHealthANDMana()
    {
        if (points > 0 )
        {
            HPpt++;
            
            points--;
            HpDownicon.color = Color.white;
        }
        UpdateUI();
    }

    public void DecreaseHealthANDMana()
    {
        if (HPpt >= 0)
        {
            HPpt--;
            points++;
           
        }
        else
        {
            HpDownicon.color = Color.gray;
        }
        UpdateUI();
    }

    public void Use(Character c1)
    {
        c1.Strength.AddModifier(new StatModifier(Strpt, StatModType.Flat, this));
        c1.Dexterity.AddModifier(new StatModifier(Dexpt, StatModType.Flat, this));
        c1.HPandMP.AddModifier(new StatModifier(HPpt, StatModType.Flat, this));

        DownLock();

        StartCoroutine(TickTextUp(StrLevel, Strpt));
        StartCoroutine(TickTextUp(DexLevel, Dexpt));
        StartCoroutine(TickTextUp(HPLevel, HPpt));

        Strpt = 0;
        Dexpt = 0;
        HPpt = 0;



        statPanel.UpdateStatValues();
        
    }

    public void Reseting()
    {
        points += Strpt + Dexpt + HPpt;
        Strpt = 0;
        Dexpt = 0;
        HPpt = 0;

        UpdateUI();
    }
    
    private void UpdateTalentPointText()
    {
        if (MyPoints == 0)
        {
            Lock();
        }
        else
        {
            unLock();
        }
        SP.text = MyPoints.ToString();
    }

    void UpdateUI()
    {
        Level.text = syslevel.ToString();
        StrLevel.text = Strpt.ToString();
        DexLevel.text = Dexpt.ToString();
        HPLevel.text = HPpt.ToString();
        SP.text = MyPoints.ToString();
    }

    public IEnumerator TickTextUp(Text t, int difference)
    {
       
        yield return new WaitForSeconds(1f);
        while (difference > 0)
        {
            difference--;
            
            t.text = "+" + difference.ToString();
            

            yield return new WaitForSeconds(0.4f);
        }

        t.text = "";
    }
}
