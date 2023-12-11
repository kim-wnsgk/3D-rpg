using System.Collections;
using UnityEngine;
using Kryz.CharacterStats;

public class Statmod
{
    public StatModifier statmodifier1;
    public StatModifier statmodifier2;
    public StatModifier statmodifier3;
    public StatModifier statmodifier4;
}

public class BuffParameters
{
    public int DexterityBuff;
    public int StrengthBuff;
    public int HPandMPBuff;
    public int DamageBuff;

    public float StrengthPercentBuff;
    public float DexterityPercentBuff;
    public float HPandMPPercentBuff;
    public float DamagePercentBuff;

    public float Duration;
    public float DurationP;
}


[CreateAssetMenu(menuName = "Item Effects/Stat Buff")]
public class StatBuffItemEffect : UsableItemEffect
{


    public int DexterityBuff;
    public int StrengthBuff;
    public int HPandMPBuff;
    public int DamageBuff;
    [Space]
    public float StrengthPercentBuff;
    public float DexterityPercentBuff;
    public float HPandMPPercentBuff;
    public float DamagePercentBuff;
    [Space]

    public float Duration;
    public float DurationP;

    public override void ExecuteEffect(UsableItem parentItem, Character character)
    {

        Statmod buff1 = new Statmod();
        Statmod buff2 = new Statmod();

        var parameters = new BuffParameters
        {
            DexterityBuff = DexterityBuff,
            StrengthBuff = StrengthBuff,
            HPandMPBuff = HPandMPBuff,
            DamageBuff = DamageBuff,
            StrengthPercentBuff = StrengthPercentBuff,
            DexterityPercentBuff = DexterityPercentBuff,
            HPandMPPercentBuff = HPandMPPercentBuff,
            DamagePercentBuff = DamagePercentBuff,
            Duration = Duration,
            DurationP = DurationP,
        };



        StatModifier dexModifier = new StatModifier(DexterityBuff, StatModType.Flat, this);
        buff1.statmodifier1= dexModifier;
        character.Dexterity.AddModifier(dexModifier);
        
        
        StatModifier strModifier = new StatModifier(StrengthBuff, StatModType.Flat, this);
        buff1.statmodifier2 = strModifier;
        character.Strength.AddModifier(strModifier);
        
        StatModifier hpModifier = new StatModifier(HPandMPBuff, StatModType.Flat, this);
        buff1.statmodifier3 = hpModifier;   
        character.HPandMP.AddModifier(hpModifier);
        
        StatModifier damageModifier = new StatModifier(DamageBuff, StatModType.Flat, this);
        buff1.statmodifier4 = damageModifier;   
        character.Damage.AddModifier(damageModifier);

        
        
        StatModifier dex2Modifier = new StatModifier(DexterityPercentBuff, StatModType.PercentMult, this);
        buff2.statmodifier1 = dex2Modifier;
        character.Dexterity.AddModifier(dex2Modifier);
        
        StatModifier str2Modifier = new StatModifier(StrengthPercentBuff, StatModType.PercentMult, this);
        buff2.statmodifier2 = str2Modifier;
        character.Strength.AddModifier(str2Modifier);
        
        StatModifier hp2Modifier = new StatModifier(HPandMPPercentBuff, StatModType.PercentMult, this);
        buff2.statmodifier3 = hp2Modifier;
        character.HPandMP.AddModifier(hp2Modifier);
        
        StatModifier damage2Modifier = new StatModifier(DamagePercentBuff, StatModType.PercentMult, this);
        buff2.statmodifier2 = damage2Modifier;
        character.Damage.AddModifier(damage2Modifier);
        

        character.UpdateStatValues();
        character.StartCoroutine(RemoveBuff(character, Duration, buff1));
        character.StartCoroutine(RemovePercentBuff(character, DurationP, buff2));
    }

    public override string GetDescription()
    {
        string description = "";
        if (DexterityBuff != 0 || StrengthBuff != 0 || HPandMPBuff != 0 || DamageBuff != 0)
        {
            description += "스탯 상승\n\n";

            
            if (DexterityBuff != 0)
                description += "신속 +" + DexterityBuff + " 상승  유지 시간 (" + FormatDuration(Duration) + ")\n";
            if (StrengthBuff != 0)
                description += "힘 +" + StrengthBuff + " 상승  유지 시간 (" + FormatDuration(Duration) + ")\n";
            if (HPandMPBuff != 0)
                description += "체력 +" + HPandMPBuff + " 상승  유지 시간 (" + FormatDuration(Duration) + ")\n";
            if (DamageBuff != 0)
                description += "데미지 +" + DamageBuff + " 상승  유지 시간 (" + FormatDuration(Duration) + ")\n";
        }

        if (DexterityPercentBuff != 0 || StrengthPercentBuff != 0 || HPandMPPercentBuff != 0 || DamagePercentBuff != 0)
        {

            description += "스탯 퍼센트 상승\n\n";
            if (DexterityPercentBuff != 0)
                description += "신속 +" + DexterityPercentBuff*100 + "% 상승  유지 시간 (" + FormatDuration(DurationP) + ")\n";
            if (StrengthPercentBuff != 0)
                description += "힘 +" + StrengthPercentBuff * 100 + "% 상승  유지 시간 (" + FormatDuration(DurationP) + ")\n";
            if (HPandMPPercentBuff != 0)
                description += "체력 +" + HPandMPPercentBuff*100 + "% 상승  유지 시간 (" + FormatDuration(DurationP) + ")\n";
            if (DamagePercentBuff != 0)
                description += "데미지 +" + DamagePercentBuff * 100 + "% 상승  유지 시간 (" + FormatDuration(DurationP) + ")\n";
        }

        
        return description;
    }

    private static IEnumerator RemoveBuff(Character character,float duration, Statmod buff1)
    {
        yield return new WaitForSeconds(duration);

        
        
        
        character.Dexterity.RemoveModifier(buff1.statmodifier1);
        
        character.Strength.RemoveModifier(buff1.statmodifier2); 
        
        character.HPandMP.RemoveModifier(buff1.statmodifier3);
        character.Damage.RemoveModifier(buff1.statmodifier4);
        character.UpdateStatValues();
            


        


    }

    private IEnumerator RemovePercentBuff(Character character, float durationP, Statmod buff2)
    {
        yield return new WaitForSeconds(durationP);

        
        
        character.Dexterity.RemoveModifier(buff2.statmodifier1);
            
        
        character.Strength.RemoveModifier(buff2.statmodifier2);
            
       
        character.HPandMP.RemoveModifier(buff2.statmodifier3);
            
        
        character.Damage.RemoveModifier(buff2.statmodifier4);
           
        character.UpdateStatValues();
        

    }

    private string FormatDuration(float timeInSeconds)
    {
        if (timeInSeconds >= 3600f)
        {
            int hours = Mathf.FloorToInt(timeInSeconds / 3600f);
            int minutes = Mathf.FloorToInt((timeInSeconds % 3600f) / 60f);
            int seconds = Mathf.FloorToInt(timeInSeconds % 60f);
            return hours + "시간 " + minutes + "분 " + seconds + "초";
        }
        else if (timeInSeconds >= 60f)
        {
            int minutes = Mathf.FloorToInt(timeInSeconds / 60f);
            int seconds = Mathf.FloorToInt(timeInSeconds % 60f);
            return minutes + "분 " + seconds + "초";
        }
        else
        {
            return timeInSeconds + "초";
        }
    }
}
