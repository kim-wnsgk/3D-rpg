using System;
using System.Collections;
// using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;




public class SpellBook : MonoBehaviour
{

    private static SpellBook instance;


    public static SpellBook MyInstance
    {
        get
        {
            if (instance == null)
            {

                instance = FindObjectOfType<SpellBook>();
            }

            return instance;
        }
    }



    [SerializeField]
    private Spell[] spells = default;

    [Header("CastBar")]

    [SerializeField]
    public Image castingBar = default;


    [SerializeField]
    private Image icon = default;


    [SerializeField]
    private Text spellName = default;


    [SerializeField]
    private Text castTime = default;


    [SerializeField]
    private CanvasGroup canvasGroup = default;






    private void Start()
    {

        canvasGroup.alpha = 0;
    }


    public Spell GetSpell(string spellName)
    {
        return Array.Find(spells, aSpell => aSpell.MyTitle.ToLower() == spellName.ToLower());
    }

}