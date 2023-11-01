using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage = 1;
    public float rate = 1f;
    public BoxCollider meleeArea;
    // public TrailRenderer trailEffect;

    // Start is called before the first frame update
    public void Use()
    {
        StopCoroutine(Swing());
        StartCoroutine(Swing());
    }

    IEnumerator Swing()
    {
        yield return new WaitForSeconds(0.1f);

        meleeArea.enabled = true;
        // trailEffect.enabled = true;

        yield return new WaitForSeconds(0.3f);

        meleeArea.enabled = false;

        yield return new WaitForSeconds(0.3f);

        // trailEffect.enabled = false;

        yield break;
    }
}
