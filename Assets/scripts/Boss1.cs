
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class Boss1 : MonoBehaviour
{
    public GameObject missile;
    public Transform missilePort;
    public BoxCollider MeleeArea;
    public int maxHealth;
    public int curHealth;
    public int jumpPower;
    public Transform target;
    public Rigidbody rigid;
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
    private Image hpBar;
    public Player player;
    public bool inBound;
    public int exp;
    public SphereCollider SphereCollider;
    
    void Awake(){
        anim = GetComponent<Animator>();
        isNav = false;
        already = false;
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<CapsuleCollider>();
        // mat = GetComponentInChildren<MeshRenderer>().material;
        nav = GetComponent<NavMeshAgent>();    
        hpBar = transform.Find("HpBar/Canvas/HPFront").GetComponent<Image>();
        GameObject obj2 = GameObject.FindWithTag("Player");
        //범위 안에 들면 하게 해야할수도
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        SphereCollider = transform.GetChild(8).gameObject.GetComponent<SphereCollider>();
        SphereCollider.enabled=false;
        target = obj2.transform;
        // players 배열이 비어있지 않은 경우, 첫 번째 플레이어를 선택
        if (players.Length > 0)
        {
            player = players[0].GetComponent<Player>();
        }
        else
        {
            // players 배열이 비어있는 경우, 원하는 처리를 수행하거나 예외 처리를 추가할 수 있습니다.
            Debug.LogError("Player not found in the scene.");
        }
    }
    void Start(){
        if(angry){
            curHealth = maxHealth;
            originalPosition = transform.position;
            isLook = true;
        }
        transform.GetChild(1).gameObject.SetActive(true);
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        // players 배열이 비어있지 않은 경우, 첫 번째 플레이어를 선택
        if (players.Length > 0)
        {
            player = players[0].GetComponent<Player>();
        }
        else
        {
            // players 배열이 비어있는 경우, 원하는 처리를 수행하거나 예외 처리를 추가할 수 있습니다.
            Debug.LogError("Player not found in the scene.");
        }
        SetHPBar();
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
            if(inBound == true && already == false){
                isNav = true;
                already = true;
                StartCoroutine(Think());
            }
            if(inBound == false){
                isNav = false;
                already = false;
                StopAllCoroutines();
            }
        }
        hpBar.rectTransform.localScale = new Vector3((float)curHealth / (float)maxHealth, 1f, 1f);
        if(curHealth<0){
            Destroy(gameObject, 1);
        }
    }
    void SetHPBar()
    {
        hpBar.rectTransform.localScale = new Vector3(1f, 1f, 1f);
    }
    
    void onTrace(){
        if(angry){
            if(isNav){
                nav.isStopped = false;
                nav.SetDestination(target.position);
                anim.SetBool("isWalk",true);
            }
            else{
                nav.isStopped = true;
                anim.SetBool("isWalk",false);
            }
        }
        
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        Weapon weapon = other.GetComponent<Weapon>();
        if (weapon != null)
        {
            curHealth -= weapon.damage;
            curHealth -= player.level;
            // Debug.Log(player.level + "아야!" + curHealth);
            // Vector3 reactVec = transform.position - other.transform.position;
            // StartCoroutine(OnDamage(reactVec));
        }
        if(other.GetComponent<PlayerSkill>()!=null){
            curHealth -= other.GetComponent<PlayerSkill>().damage;
            // Vector3 reactVec = transform.position - other.transform.position;
            // StartCoroutine(OnDamage(reactVec));
            
        }
    }
       
    void FixedUpdate(){
        FreezeVelocity();
    }
    void FreezeVelocity(){
        rigid.angularVelocity = Vector3.zero;
        rigid.velocity = Vector3.zero;
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
    // IEnumerator OnDamage(Vector3 reactVec)
    // {
    //     yield return new WaitForSeconds(0.1f);
    //     if (curHealth > 0)
    //     {
    //         anim.SetTrigger("doDamage");
    //         gameObject.layer = 7;
    //         reactVec = reactVec.normalized;
    //         reactVec += Vector3.up;
    //         rigid.AddForce(reactVec * 200, ForceMode.Impulse);
    //     }
    //     else
    //     {
    //         anim.SetTrigger("doDie");
    //         Destroy(gameObject, 1);
    //     }
    // }
    IEnumerator Think (){
        yield return new WaitForSeconds(0.1f);
        int ranAction = Random.Range(0, 5);
        if(Vector3.Distance(transform.position, target.position)<9f){
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
        yield return new WaitForSeconds (1f);//애니메이션에 맞게 수정
        isNav=false;
        SphereCollider.enabled=true;
        anim.SetTrigger("attack");
        yield return new WaitForSeconds (0.1f);
        isNav=true;
        SphereCollider.enabled=false;
        yield return new WaitForSeconds (1f);
        StartCoroutine(Think());
    }

IEnumerator Jump()
{
    anim.SetTrigger("jump");
    isLook = false;
    MeleeArea.enabled = false;

    rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);

    yield return new WaitForSeconds(1f); // 점프가 끝날 때까지 대기

    MeleeArea.enabled = true;
    yield return new WaitForSeconds(0.5f);
    MeleeArea.enabled = false;

    yield return new WaitForSeconds(4f);
    isLook = true;
    MeleeArea.enabled = true;
    StartCoroutine(Think());
}


    IEnumerator Walk()
    {
        yield return new WaitForSeconds (1f);
        StartCoroutine(Think());
    }

}
