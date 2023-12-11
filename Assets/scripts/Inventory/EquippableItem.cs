using UnityEngine;
using Kryz.CharacterStats;

public enum EquipmentType
{
    Helmet,
    Chest,
    Gloves,
    Boots,
    Weapon1,
    Weapon2,
    Accessory1,
    Accessory2,
}

[CreateAssetMenu(menuName = "Items/Equippable Item")]
public class EquippableItem : Item
{
    public int StrengthBonus;
    public int DexterityBonus;
    public int HPandMPBonus;
    public int DamageBonus;
    [Space]
    public float StrengthPercentBonus;
    public float DexterityPercentBonus;
    public float HPandMPPercentBonus;
    public float DamagePercentBonus;
    [Space]
    public EquipmentType EquipmentType;

    public override Item GetCopy()
    {
        return Instantiate(this);
    }

    public override void Destroy()
    {
        Destroy(this);
    }

    public void Equip(Character c)
    {
        if (StrengthBonus != 0)
            c.Strength.AddModifier(new StatModifier(StrengthBonus, StatModType.Flat, this));
        if (DexterityBonus != 0)
            c.Dexterity.AddModifier(new StatModifier(DexterityBonus, StatModType.Flat, this));
        if (HPandMPBonus != 0)
            c.HPandMP.AddModifier(new StatModifier(HPandMPBonus, StatModType.Flat, this));
        if (DamageBonus != 0)
            c.Damage.AddModifier(new StatModifier(DamageBonus, StatModType.Flat, this));

        if (StrengthPercentBonus != 0)
            c.Strength.AddModifier(new StatModifier(StrengthPercentBonus, StatModType.PercentMult, this));
        if (DexterityPercentBonus != 0)
            c.Dexterity.AddModifier(new StatModifier(DexterityPercentBonus, StatModType.PercentMult, this));
        if (HPandMPPercentBonus != 0)
            c.HPandMP.AddModifier(new StatModifier(HPandMPPercentBonus, StatModType.PercentMult, this));
        if (DamagePercentBonus != 0)
            c.Damage.AddModifier(new StatModifier(DamagePercentBonus, StatModType.PercentMult, this));
    }

    public void Unequip(Character c)
    {
        c.Strength.RemoveAllModifiersFromSource(this);
        c.Dexterity.RemoveAllModifiersFromSource(this);
        c.HPandMP.RemoveAllModifiersFromSource(this);
        c.Damage.RemoveAllModifiersFromSource(this);
    }

    public override string GetItemType()
    {
        return EquipmentType.ToString();
    }

    public override string GetDescription()
    {
        sb.Length = 0;
        AddStat(StrengthBonus, "Strength");
        AddStat(DexterityBonus, "Dexterity");
        AddStat(HPandMPBonus, "Intelligence");
        AddStat(DamageBonus, "Vitality");

        AddStat(StrengthPercentBonus, "Strength", isPercent: true);
        AddStat(DexterityPercentBonus, "Dexterity", isPercent: true);
        AddStat(HPandMPPercentBonus, "Intelligence", isPercent: true);
        AddStat(DamagePercentBonus, "Vitality", isPercent: true);

        return sb.ToString();
    }

    private void AddStat(float value, string statName, bool isPercent = false)
    {
        if (value != 0)
        {
            if (sb.Length > 0)
                sb.AppendLine();

            if (value > 0)
                sb.Append("+");

            if (isPercent)
            {
                sb.Append(value * 100);
                sb.Append("% ");
            }
            else
            {
                sb.Append(value);
                sb.Append(" ");
            }
            sb.Append(statName);
        }
    }
}
