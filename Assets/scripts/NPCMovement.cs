using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Rigidbody cube;
    public float speed = 1f;
    public Animator anim;
    public bool iswalk;
    public float waitSeconds;
    
    // Start is called before the first frame update
    void Start()
    {
        waitSeconds = 4;
        iswalk = false;
        anim = GetComponent<Animator>();
        //4초에 한번씩 움직이거나 멈춤
        StartCoroutine(MoveObject());
    
    }
    IEnumerator MoveObject(){
        while(true){
            yield return new WaitForSeconds(waitSeconds);
            float random = Random.Range(-1,1);
            if(random==0){
                Walk();
            }
            else{
                Idle();
            }
        }
    }
    void Update()
    {
        if(iswalk){
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            
        }
        
    }
    void Idle(){
        iswalk=false;
        anim.SetBool("Walk", false);
        anim.SetTrigger("Idle");
    }
    void Walk(){
        iswalk=true;
        float dir1 = Random.Range(-180f,180f);
        float dir2 = Random.Range(-180,180f);
        transform.rotation = Quaternion.Euler(new Vector3(0f,dir2,0f));
        Debug.Log(dir1+dir2);
        anim.SetBool("Walk",true);
    }
}
