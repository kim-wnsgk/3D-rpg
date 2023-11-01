
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class enemy : MonoBehaviour
{
    public int maxHealth;
    public int curHealth;
    public Transform target;
    Rigidbody rigid;
    BoxCollider boxCollider;
    Material mat;
    NavMeshAgent nav;
    public Animator anim;
    public bool isNav;

    void Awake()
    {
        anim = GetComponent<Animator>();
        isNav = false;
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        mat = GetComponent<MeshRenderer>().material;
        nav = GetComponent<NavMeshAgent>();

        // NavMeshAgent 컴포넌트가 연결되어 있는지 확인
        if (nav != null)
        {
            nav.enabled = false; // 초기에 비활성화
        }
    }
    void Start()
    {
        curHealth = maxHealth;
    }
    void Update()
    {
        onTrace();
    }
    void onTrace()
    {
        if (isNav)
        {
            nav.SetDestination(target.position);
            anim.SetBool("isWalk", true);
        }
        else
        {
            anim.SetBool("isWalk", false);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isNav = true;
        }
        if (other.tag == "Melee")
        {
            Weapon weapon = other.GetComponent<Weapon>();
            //플레이어 스텟으로 인한 공격력이 weapon.damage 올리는 방식으로 하자
            curHealth -= weapon.damage;

            Debug.Log("curHealth: " + curHealth);
            Vector3 reactVec = transform.position - other.transform.position;
            StartCoroutine(OnDamage(reactVec));
        }
        // else if(other.tag == "magic"){
        //     Magic magic = other.GetComponent<Magic>();
        //     //플레이어 스텟으로 인한 공격력이 weapon.damage 올리는 방식으로 하자
        //     curHealth -= magic.damage;
        //     Vector3 reactVec = transform.position - other.transform.position;
        //     StartCoroutine(OnDamage());
        // }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isNav = false;
        }
    }


    // Start is called before the first frame update
    IEnumerator OnDamage(Vector3 reactVec)
    {
        mat.color = Color.red;
        yield return new WaitForSeconds(0.1f);

        if (curHealth > 0)
        {
            anim.SetTrigger("doDamage");
            mat.color = Color.white;
        }
        else
        {
            anim.SetTrigger("doDie");
            mat.color = Color.gray;
            gameObject.layer = 7;
            Destroy(gameObject, 2);
            reactVec = reactVec.normalized;
            reactVec += Vector3.up;
            rigid.AddForce(reactVec * 5, ForceMode.Impulse);
        }
    }
}
