
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
    NavMeshAgent nav;
    public Animator anim;
    public bool isNav;
    private Vector3 originalPosition;
    public int damage;

    void Awake()
    {
        anim = GetComponent<Animator>();
        isNav = false;
        rigid = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        curHealth = maxHealth;
        originalPosition = transform.position;
    }
    void Update()
    {
        onTrace();
        float distance = Vector3.Distance(transform.position, originalPosition);
        if (distance < 0.5f)
        {
            anim.SetBool("isWalk", false);
        }
    }
    void FixedUpdate()
    {
        FreezeVelocity();
    }
    void FreezeVelocity()
    {
        rigid.angularVelocity = Vector3.zero;
        rigid.velocity = Vector3.zero;
    }
    void onTrace()
    {
        // if (isNav)
        // {
        nav.SetDestination(target.position);
        anim.SetBool("isWalk", true);
        // }
    }
    void OnTriggerEnter(Collider other)
    {
        // if (other.tag == "Player")
        // {
        //     isNav = true;
        // }

        if (other.tag == "Melee")
        {
            Weapon weapon = other.GetComponent<Weapon>();
            if (weapon != null)
            {
                curHealth -= weapon.damage;
                Debug.Log(curHealth);
                Vector3 reactVec = transform.position - other.transform.position;
                StartCoroutine(OnDamage(reactVec));
            }
        }
        // else if(other.tag == "magic"){
        //     Magic magic = other.GetComponent<Magic>();
        //     //플레이어 스텟으로 인한 공격력이 weapon.damage 올리는 방식으로 하자
        //     curHealth -= magic.damage;
        //     Vector3 reactVec = transform.position - other.transform.position;
        //     StartCoroutine(OnDamage());
        // }
    }

    // void OnTriggerExit(Collider other)
    // {
    //     if (other.tag == "Player")
    //     {
    //         isNav = false;
    //         nav.SetDestination(originalPosition);
    //     }
    // }


    // Start is called before the first frame update
    IEnumerator OnDamage(Vector3 reactVec)
    {
        yield return new WaitForSeconds(0.1f);

        if (curHealth < 0)
        {
            anim.SetTrigger("doDie");
            Destroy(gameObject, 4);
        }
        else
        {
            anim.SetTrigger("doDamage");
            gameObject.layer = 7;
            reactVec = reactVec.normalized;
            reactVec += Vector3.up;
            rigid.AddForce(reactVec * 5, ForceMode.Impulse);
        }
    }
}
