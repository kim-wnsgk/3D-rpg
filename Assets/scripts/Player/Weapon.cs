using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    // public int originDamage = 1;
    
    public int damage = 2;
    public float rate = 0.5f;
    public BoxCollider meleeArea;
    public TrailRenderer trailEffect;
    void Start()
    {
        // damage = 0;
        
    }
    public void Use()
    {
        StopCoroutine(Swing());
        StartCoroutine(Swing());
    }

    IEnumerator Swing()
    {
        Player obj2 = GameObject.FindWithTag("Player").GetComponent<Player>();
        damage = 2 + obj2.str*2;
        // damage = originDamage;
        yield return new WaitForSeconds(0.1f);

        meleeArea.enabled = true;
        trailEffect.enabled = true;

        yield return new WaitForSeconds(0.3f);

        meleeArea.enabled = false;

        yield return new WaitForSeconds(0.2f);
        trailEffect.enabled = false;

        trailEffect.Clear();

        // damage = 0;
        yield break;

    }
}