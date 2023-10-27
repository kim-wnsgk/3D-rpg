using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Item : MonoBehaviour
{
    public enum Type { Weapon, Coin, Exp, Heart, Grenade }
    public Type type;
    public int value;

    void Update()
    {
        transform.Rotate(Vector3.right * 40 * Time.deltaTime);
    }
}
