using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        damage = 10 + transform.parent.gameObject.GetComponent<Player>().str*10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other){
         if (other.gameObject.CompareTag("Enemy")){
            other.gameObject.GetComponent<enemy>().curHealth -= damage;
         }
    }
}
