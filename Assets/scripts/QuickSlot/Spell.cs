using System;
using System.Diagnostics;
using UnityEngine;


public interface IMoveable
{
    
    Sprite MyIcon { get; }
}
public interface IUseable
{
    
    Sprite MyIcon { get; }

    
    void Use();
}


[Serializable]
public class Spell : IUseable, IMoveable, IDescribable
{
    
    [SerializeField]
    private string title = default;

    
    public string MyTitle { get => title; }

    
    [SerializeField]
    private float damage = default;

    
    public float MyDamage { get => Mathf.Ceil(damage); set => damage = value; }

    
    [SerializeField]
    private float range = default;

    
    public float MyRange { get => range; set => range = value; }

    
    [SerializeField]
    private string description = default;

    
    [SerializeField]
    private Sprite icon = default;

    
    public Sprite MyIcon { get => icon; }

    [SerializeField]
    private float speed = default;

    
    public float MySpeed { get => speed; }

   
    [SerializeField]
    private float castTime = default;

    
    public float MyCastTime { get => castTime; set => castTime = value; }

    
    [SerializeField]
    private GameObject prefab = default;

    
    public GameObject MyPrefab { get => prefab; }

    
    [SerializeField]
    private Color barColor = default;

    
    public Color MyBarColor { get => barColor; }

  

    
    public void Use()
    {
        //Player.MyInstance.CastSpell(this);
    }

    
    public string GetDescription()
    {
        string spellTitle = string.Format("<color=#FFD904><b>{0}</b></color>", title);
        string spellStats = string.Format("<color=#ECECEC>Incantation : {0}s</color>", castTime);
        string spellRange = string.Format("<color=#ECECEC>Port? : {0}m</color>", range);
        string spellDescription = string.Format("<color=#E0D0AE>{0}\net cause <color=cyan>{1}</color> points de d??s</color>", description, MyDamage);

        return string.Format("{0}\n\n{1}\n{2}\n\n{3}", spellTitle, spellStats, spellRange, spellDescription);
    }
}