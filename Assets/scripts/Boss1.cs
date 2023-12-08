
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Boss1 : MonoBehaviour
{
    public GameObject missile;
    public Transform missilePort;
    public BoxCollider MeleeArea;
    public int maxHealth;
    public int curHealth;
    public int jumpPower;
    public Transform target;
    Rigidbody rigid;
    CapsuleCollider boxCollider;
    Material mat;
    NavMeshAgent nav;   
    public Animator anim;
    public bool isNav;
    private Vector3 originalPosition;
    Vector3 lookVec;
    Vector3 jumpVec;
    bool isLook;
    bool isDead;
    public bool angry;
    bool already;
    
    void Awake(){
        anim = GetComponent<Animator>();
        isNav = false;
        already = false;
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<CapsuleCollider>();
        // mat = GetComponentInChildren<MeshRenderer>().material;
        nav = GetComponent<NavMeshAgent>();    
        //범위 안에 들면 하게 해야할수도
    }
    void Start(){
        if(angry){
            curHealth = maxHealth;
            originalPosition = transform.position;
            isLook = true;
        }
    }
    void Update()
    {       
        if(angry){
                if(isDead){
                    StopAllCoroutines();
                    return;
                }
                if(isLook){
                    float h = Input.GetAxisRaw("Horizontal");
                    float v = Input.GetAxisRaw("Vertical");
                    lookVec = new Vector3(h, 0, v)* 5f;
                    transform.LookAt(target.position + lookVec);
                }
                onTrace();
                float distance = Vector3.Distance(transform.position, originalPosition);
                if(distance<0.5f){
                    anim.SetBool("isWalk",false);
                }
        }
    }
    void onTrace(){
        if(angry){
            if(isNav){
                nav.SetDestination(target.position);
                anim.SetBool("isWalk",true);
            }
            else{
                nav.isStopped = true;
                anim.SetBool("isWalk",false);
            }
        }
        
    }
    void OnTriggerEnter(Collider other){
        if(angry){
            if(other.tag == "Player" && already == false){
                isNav = true;
                already = true;
                StartCoroutine(Think());
            }
        }
    void FixedUpdate(){
        FreezeVelocity();
    }
    void FreezeVelocity(){
        rigid.angularVelocity = Vector3.zero;
        rigid.velocity = Vector3.zero;
    }
        
    }

    void OnTriggerExit(Collider other){
        if(angry){
            if(other.tag == "Player"){
                isNav=false;
                StopAllCoroutines();
            }
        }

    }
    
        // if(other.tag == "Melee"){
        //     Weapon weapon = other.GetComponent<Weapon>();
        //     //플레이어 스텟으로 인한 공격력이 weapon.damage 올리는 방식으로 하자
        //     curHealth -= weapon.damage;
        //     Vector3 reactVec = transform.position - other.transform.position;
        //     StartCoroutine(OnDamage());
        // }
        // else if(other.tag == "magic"){
        //     Magic magic = other.GetComponent<Magic>();
        //     //플레이어 스텟으로 인한 공격력이 weapon.damage 올리는 방식으로 하자
        //     curHealth -= magic.damage;
        //     Vector3 reactVec = transform.position - other.transform.position;
        //     StartCoroutine(OnDamage());
        // }
    
    // void OnTriggerExit(Collider other){
    //     if(other.tag == "Player"){
    //         isNav=false;
    //         nav.SetDestination(originalPosition);
    //     }
    // }
        
    
    // Start is called before the first frame update
    IEnumerator OnDamage(Vector3 reactVec){
        mat.color = Color.red;
        yield return new WaitForSeconds(0.5f);

        if(curHealth<0){
            mat.color = Color.white;
            anim.SetTrigger("doDie");
            gameObject.layer = 7;
            Destroy(gameObject,4);
            isDead = true;
        }
        else{
            anim.SetTrigger("doDamage");
            mat.color = Color.gray;
            
        }
    }
    IEnumerator Think (){
        yield return new WaitForSeconds(0.1f);
        int ranAction = Random.Range(0, 5);
        if(Vector3.Distance(transform.position, target.position)<5f){
            StartCoroutine(Attack());
        }
        else if(Vector3.Distance(transform.position, target.position)<15f){
            switch (ranAction) {
                case 0:
                    StartCoroutine(Walk());
                    break;
                case 1:
                    //미사일 발사 패턴
                    StartCoroutine(MissileShot());
                    break;
                case 2:
                    StartCoroutine(Jump());
                    break;
                case 3:
                    //일반 공격
                    StartCoroutine(Walk());
                    break;
                case 4:
                    //점프 공격 패턴
                    StartCoroutine(Jump());
                    break;
            }
        }
        else{
            switch (ranAction) {
                case 0:
                    StartCoroutine(Walk());
                    break;
                case 1:
                    //미사일 발사 패턴
                    // StartCoroutine(Walk());
                    StartCoroutine(MissileShot());
                    break;
                case 2:
                    StartCoroutine(Walk());
                    break;
                case 3:
                    //일반 공격
                    StartCoroutine(Walk());
                    break;
                case 4:
                    //점프 공격 패턴
                    StartCoroutine(Walk());
                    break;
            }
        }
    }
    IEnumerator MissileShot()
    {   
        isNav=false;
        anim.SetTrigger("throw");
        yield return new WaitForSeconds(1.5f);
        GameObject instantMissile = Instantiate(missile, missilePort.position,missilePort.rotation);
        BossMissile bossMissile = instantMissile.GetComponent<BossMissile>();
        bossMissile.target = target;
        yield return new WaitForSeconds (2f);//애니메이션에 맞게 수정
        isNav=true;
        StartCoroutine(Think());
    }
    IEnumerator Attack()
    {
        isNav=false;
        anim.SetTrigger("attack");
        yield return new WaitForSeconds (2.5f);//애니메이션에 맞게 수정
        isNav=true;
        StartCoroutine(Think());
    }

IEnumerator Jump()
{
    anim.SetTrigger("jump");
    isLook = false;
    boxCollider.enabled = false;

    rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);

    yield return new WaitForSeconds(1f); // 점프가 끝날 때까지 대기

    MeleeArea.enabled = true;
    yield return new WaitForSeconds(0.5f);
    MeleeArea.enabled = false;

    yield return new WaitForSeconds(4f);
    isLook = true;
    boxCollider.enabled = true;
    StartCoroutine(Think());
}


    IEnumerator Walk()
    {
        yield return new WaitForSeconds (1f);
        StartCoroutine(Think());
    }

}
