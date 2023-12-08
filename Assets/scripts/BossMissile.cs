using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMissile : MonoBehaviour
{
    public Transform target;
    NavMeshAgent nav;
    public int damage;
    public bool isMelee;
    private float existTime;
    void Awake(){
        nav = GetComponent<NavMeshAgent>();
    }
    void Start(){
        existTime = Time.time;
    }
    void Update(){
        nav.SetDestination(target.position);
        if(Time.time >existTime+ 5f){
            Destroy(gameObject);
        }
    }
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
