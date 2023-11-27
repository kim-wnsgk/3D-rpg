
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
    // public bool isNav;
    private Vector3 originalPosition;
    public int damage;

    void Awake()
    {
        anim = GetComponent<Animator>();
        // isNav = false;
        rigid = GetComponent<Rigidbody>();
    }
    void Start()
    {
        curHealth = maxHealth;
        originalPosition = transform.position;
        nav = GetComponent<NavMeshAgent>();
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
        if (other.tag == "Player")
        {
            Weapon weapon = other.GetComponentInChildren<Weapon>();
            if (weapon != null)
            {
                curHealth -= weapon.damage;
                Debug.Log("enemy's health : " + curHealth);
                Vector3 reactVec = transform.position - other.transform.position;
                StartCoroutine(OnDamage(reactVec));
                Player player = other.GetComponent<Player>();
                player.health += damage;
            }
        }
    }


    // Start is called before the first frame update
    IEnumerator OnDamage(Vector3 reactVec)
    {
        yield return new WaitForSeconds(0.1f);

        if (curHealth > 0)
        {
            anim.SetTrigger("doDamage");
            gameObject.layer = 7;
            reactVec = reactVec.normalized;
            reactVec += Vector3.up;
            rigid.AddForce(reactVec * 5, ForceMode.Impulse);

        }
        else
        {
            anim.SetTrigger("doDie");
            Destroy(gameObject, 4);
        }
    }
}
