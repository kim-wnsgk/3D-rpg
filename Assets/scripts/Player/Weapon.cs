using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int originDamage = 1;
    public int damage = 0;
    public float rate = 1f;
    public BoxCollider meleeArea;
    public TrailRenderer trailEffect;

    void Start()
    {
        damage = 0;
    }
    public void Use()
    {
        StopCoroutine(Swing());
        StartCoroutine(Swing());
    }

    IEnumerator Swing()
    {
        damage = originDamage;
        yield return new WaitForSeconds(0.1f);

        meleeArea.enabled = true;
        trailEffect.enabled = true;

        yield return new WaitForSeconds(0.5f);
        trailEffect.enabled = false;

        meleeArea.enabled = false;

        yield return new WaitForSeconds(1f);

        damage = 0;
        yield break;

    }
}