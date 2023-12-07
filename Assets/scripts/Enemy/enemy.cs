
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class enemy : MonoBehaviour
{
    public int maxHealth;
    public int curHealth;
    public Transform target;
    public Rigidbody rigid;
    NavMeshAgent nav;
    public Animator anim;
    // public bool isNav;
    private Vector3 originalPosition;
    public int damage;
    public int exp;
    private Image hpBar;
    void Awake()
    {
        anim = GetComponent<Animator>();
        // isNav = false;
        rigid = GetComponent<Rigidbody>();
        hpBar = transform.Find("HpBar/Canvas/HPFront").GetComponent<Image>();
    }
    void Start()
    {
        SetHPBar();
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
    void SetHPBar(){
         hpBar.rectTransform.localScale = new Vector3(1f, 1f, 1f);
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
            Debug.Log("아야!");
            Weapon weapon = other.GetComponentInChildren<Weapon>();
            Player player = other.GetComponent<Player>();
            if (weapon != null && weapon.damage != 0)
            {
                curHealth -= (weapon.damage + player.level);
                Debug.Log("enemy's health : " + curHealth);
                Vector3 reactVec = transform.position - other.transform.position;
                StartCoroutine(OnDamage(reactVec));
                player.health += damage;
            }
        }
    }


    // Start is called before the first frame update
    IEnumerator OnDamage(Vector3 reactVec)
    {
        yield return new WaitForSeconds(0.1f);
        hpBar.rectTransform.localScale = new Vector3((float)curHealth / (float)maxHealth, 1f, 1f);
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
