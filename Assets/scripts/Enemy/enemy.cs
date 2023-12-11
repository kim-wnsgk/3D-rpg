
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
    public bool isNav;
    private Vector3 originalPosition;
    public int damage;
    public int exp;
    private Image hpBar;
    public int enemyIndex;
    public Player player;
    void Awake()
    {
        anim = GetComponent<Animator>();
        isNav = false;
        rigid = GetComponent<Rigidbody>();
        hpBar = transform.Find("HpBar/Canvas/HPFront").GetComponent<Image>();
        GameObject obj2 = GameObject.FindWithTag("Player");
        target = obj2.transform;

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
    }
    void Start()
    {
        maxHealth = enemyIndex * 10;
        damage = enemyIndex * 10;
        exp = enemyIndex * 10;
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
        hpBar.rectTransform.localScale = new Vector3((float)curHealth / (float)maxHealth, 1f, 1f);

    }
    void SetHPBar()
    {
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
        if (isNav)
        {
            nav.SetDestination(target.position);
            anim.SetBool("isWalk", true);
        }
        else
        {
            nav.SetDestination(originalPosition);
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
            Debug.Log(player.level + "아야!" + curHealth);
            Debug.Log("enemyindex : " + enemyIndex);
            Vector3 reactVec = transform.position - other.transform.position;
            StartCoroutine(OnDamage(reactVec));
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
            rigid.AddForce(reactVec * 200, ForceMode.Impulse);

        }
        else
        {
            anim.SetTrigger("doDie");
            Destroy(gameObject, 1);
        }
    }
}
