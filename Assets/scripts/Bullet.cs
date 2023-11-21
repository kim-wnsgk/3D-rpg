using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bullet : MonoBehaviour
{
    public int damage;
    public bool isMelee;
    void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag == "ground"){
            Destroy(gameObject,3);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(!isMelee && other.gameObject.tag == "wall"){
            Destroy(gameObject);
        }
    }
}
