using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc1 : MonoBehaviour
{
    public GameObject Model;
    public bool transf;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Transform();
    }
    void Transform(){
        if(transf){
            gameObject.GetComponent<Boss1>().enabled = true;
            Boss1 boss1 = GetComponent<Boss1>();
            if (boss1 != null)
            {
                boss1.angry=true; // 다른 스크립트의 public 변수에 접근
                Vector3 currentScale = transform.localScale;
                // X, Y, Z 각각 3배씩 곱하여 크기를 증가시킵니다.
                currentScale.x *= 3f;
                currentScale.y *= 3f;
                currentScale.z *= 3f;

                // 변경된 크기를 적용합니다.
                transform.localScale = currentScale;
                gameObject.GetComponent<Npc1>().enabled = false;
            }       
        }

    }
}
