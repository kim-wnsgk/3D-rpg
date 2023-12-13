using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    public static Player Instance;

    public float moveSpeed = 10.0f; // 이동 속도
    public GameObject[] weapons;  // Weapons들 저장
    public bool[] hasWeapons;  // 해당 인덱스 weapon 갖고 있는지

    public int coin;
    public int health;
    public int mana;
    public int maxHealth;
    public int maxMana;
    public int exp;
    public int level;
    public float zSkillCool;
    float zSkill;


    public Rigidbody rigid;
    bool isJump;
    public float jumpPower = 5f;

    float hAxis;
    float vAxis;
    bool shiftDown;
    bool spaceDown;
    bool ctrlDown;
    bool eDown;
    bool qDown;
    bool isBorder;
    bool zDown;
    SphereCollider slashCollider;
    Vector3 moveVec;
    public Animator anim;
    public TrailRenderer trailRenderer;

    GameObject nearObject;
    Weapon equipWeapon;
    private float fireDelay = 1f;
    private bool isFireReady = true;

    int weaponIndex = -1;
    private int comboStack = 0;
    float lastComboTime = 0;
    float maxComboDelay = 1.5f;


    bool isMoving;
    public AudioClip walkSound;
    public AudioClip runSound;
    public AudioClip jumpSound;
    public AudioClip attack1;
    public AudioClip attack2;
    public AudioSource audioSource;

    void Awake()
    {
        zSkillCool = 10;
        zSkill = -10;
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        isJump = false;
        slashCollider = transform.GetChild(7).gameObject.GetComponent<SphereCollider>();
        // equipWeapon = weapons[0];
        // equipWeapon.SetActive(true);  // 첫번째 무기 기본 설정
        level = 1;
        slashCollider.enabled = false;
        Instance = this;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");


        // 현재 씬에서 플레이어가 두 개 이상인 경우 파괴
        if (players.Length > 1)
        {
            Destroy(gameObject);
        }

        // 중복이 아니라면 플레이어 태그를 추가하고 다음 씬으로 이동
        else
        {
            gameObject.tag = "Player";
            DontDestroyOnLoad(gameObject);
        }
    }

    void Update()
    {
        GetInput();
        Move();
        if (zDown && Time.time - zSkill > zSkillCool)
        {
            zSkill = Time.time;
            StartCoroutine(Slash());
        }
        Jump();
        Attack();
        Interaction();
        Swap();
        handleExp();

        // 플레이어가 죽으면 경험치 0으로 만들고 메인맵으로 이동
        if (health <= 0)
        {
            exp = 0;
            health = 100;
            SceneManager.LoadScene("school");
        }
        if (transform.position.y < -10.2f)
        {
            transform.position = new Vector3(0f, 0f, 0f);
        }
    }

    void FixedUpdate()
    {
        FreezeRotation();
        StopToWall();
    }
    void StopToWall()
    {
        Debug.DrawRay(transform.position, transform.forward * 5, Color.green);
        isBorder = Physics.Raycast(transform.position, transform.forward, 3, LayerMask.GetMask("Wall"));
    }
    void FreezeRotation()
    {
        rigid.angularVelocity = Vector3.zero;
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        shiftDown = Input.GetKey(KeyCode.LeftShift);
        spaceDown = Input.GetKeyDown(KeyCode.Space);
        ctrlDown = Input.GetKeyDown(KeyCode.LeftControl);
        eDown = Input.GetKeyDown(KeyCode.E);
        qDown = Input.GetKeyDown(KeyCode.Q);
        zDown = Input.GetKeyDown(KeyCode.Z);
    }

    void Move()
    {
        bool isRun = shiftDown;

        if (!isFireReady)
        {
            moveVec = Vector3.zero;
        }

        anim.SetBool("Walk", moveVec != Vector3.zero);

        // Shift 버튼을 누르는 동안에만 달리기 애니메이션 활성화
        anim.SetBool("Run", isRun);

        moveVec = new Vector3(hAxis, 0, vAxis).normalized;
        if (!isBorder)
            transform.position += moveVec * (isRun ? moveSpeed * 2f : moveSpeed) * Time.deltaTime;

        transform.LookAt(transform.position + moveVec);

        // 달릴 때 트레일 렌더러 활성화/비활성화
        if (isRun)
        {
            trailRenderer.emitting = true;
            audioSource.clip = runSound;
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
        {
            trailRenderer.emitting = false;
            if (hAxis != 0 || vAxis != 0)
            {
                audioSource.clip = walkSound;
                if (!audioSource.isPlaying)
                    audioSource.Play();
            }
            else
                audioSource.Stop();
        }

    }
    IEnumerator Slash()
    {
        anim.SetTrigger("slash");
        slashCollider.enabled = true;
        audioSource.clip = attack2;
        audioSource.Play();
        yield return new WaitForSeconds(1.3f);
        slashCollider.enabled = false;
    }
    void Jump()
    {
        if (!isJump && spaceDown)
        {
            anim.SetTrigger("Jump");
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            audioSource.clip = jumpSound;
            audioSource.Play();
            isJump = true;
        }
    }

    // 점프 연속으로 되지 않도록
    void OnCollisionEnter(Collision collision)
    {
        //땅에 닿으면 점프 초기화
        if (collision.gameObject.tag == "Ground")
        {
            isJump = false;

        }


    }

    void Swap()  // 나중에는 int index를 파라미터로 받아서 해당 인덱스 무기를 들도록, 이전에 들고있는 무기가 같은거면 실행 안하도록
    {
        if (qDown)
        {
            if (equipWeapon != null) equipWeapon.gameObject.SetActive(false);  // 이전 무기는 비활성화

            if (weaponIndex == weapons.Length - 1)
            {
                weaponIndex = 0;
            }
            else
            {
                weaponIndex += 1;
            }

            if (hasWeapons[weaponIndex])
            {
                equipWeapon = weapons[weaponIndex].GetComponent<Weapon>();
                equipWeapon.gameObject.SetActive(true);  // 새로운 무기 활성화

                anim.SetTrigger("SwapWeapon");
            }


        }

    }

    void Interaction()
    {
        // if (eDown && nearObject != null && !isJump)
        // {
        //     if (nearObject.tag == "Melee")
        //     {
        //         Item item = nearObject.GetComponent<Item>();
        //         int weaponIndex = item.value;
        //         hasWeapons[weaponIndex] = true;  // 해당 무기는 소유한 상태가 됨

        //         Destroy(nearObject);
        //     }
        // }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Melee")
        {
            nearObject = other.gameObject;
        }
    }

    // 나중에 이부분을 트리거가 아닌 함수로 변경
    void OnTriggerEnter(Collider other)
    {
        // if (other.tag == "Item")
        // {
        //     Item item = other.GetComponent<Item>();
        //     switch (item.type)
        //     {
        //         case Item.Type.Coin:
        //             coin += item.value;
        //             break;
        //         case Item.Type.Heart:
        //             health += item.value;
        //             if (health > maxHealth)
        //                 health = maxHealth;
        //             break;
        //         case Item.Type.Mana:
        //             mana += item.value;
        //             if (mana > maxMana)
        //                 mana = maxMana;
        //             break;
        //     }
        //     Destroy(other.gameObject);
        // }
        if (other.tag == "Enemy" && equipWeapon && !isFireReady)  // 공격
        {
            enemy enemy = other.GetComponent<enemy>();
            enemy.curHealth -= (equipWeapon.damage + level);  // 레벨에 따라 추가데미지
            Debug.Log("Enemy's health : " + enemy.curHealth);

            if (enemy.curHealth > 0)  // 적이 죽지 않고 공격 당할때
            {
                enemy.anim.SetTrigger("doDamage");
                // enemy.transform.position = enemy.transform.position.normalized;
                enemy.transform.position += Vector3.up * 10;

                // 적을 뒤로 밀기
                Vector3 attackDirection = other.transform.position - transform.position;
                attackDirection.y = 0;  // 수직 방향은 무시
                enemy.rigid.AddForce(attackDirection.normalized * 50, ForceMode.Impulse);
            }
            else  // 적이 죽으면
            {
                enemy.anim.SetTrigger("doDie");
                Destroy(enemy.gameObject, 0);
                exp += enemy.exp;  // 경험치 쌓임
                coin += enemy.exp;
            }
        }

        if (other.tag == "Enemy")  // 공격 당하면
        {
            enemy enemy = other.GetComponent<enemy>();
            health -= enemy.damage;
            Debug.Log("Player's health : " + health);
            Vector3 attackDirection = transform.position - other.transform.position;
            attackDirection.y = 0;  // 수직 방향은 무시
            rigid.AddForce(attackDirection.normalized * 70, ForceMode.Impulse);
        }
        if (other.tag == "boss" && equipWeapon && !isFireReady)  // 공격
        {
            Boss1 enemy = other.GetComponent<Boss1>();
            enemy.curHealth -= (equipWeapon.damage + level);  // 레벨에 따라 추가데미지
            Debug.Log("Enemy's health : " + enemy.curHealth);

            if (enemy.curHealth > 0)  // 적이 죽지 않고 공격 당할때
            {
                enemy.anim.SetTrigger("doDamage");
                // enemy.transform.position = enemy.transform.position.normalized;
                enemy.transform.position += Vector3.up * 10;

                // 적을 뒤로 밀기
                Vector3 attackDirection = other.transform.position - transform.position;
                attackDirection.y = 0;  // 수직 방향은 무시
                enemy.rigid.AddForce(attackDirection.normalized * 50, ForceMode.Impulse);
            }
            else  // 적이 죽으면
            {
                enemy.anim.SetTrigger("doDie");
                Destroy(enemy.gameObject, 0);
                exp += enemy.exp;  // 경험치 쌓임
            }
        }
        if (other.tag == "bullet")
        {
            Bullet enemy = other.GetComponent<Bullet>();
            health -= enemy.damage;
            Vector3 reactVec = transform.position - enemy.transform.position;
            StartCoroutine(OnDamage(reactVec));
        }
        if(other.tag =="EnemyWeapon"){
            BossMissile enemy = other.GetComponent<BossMissile>();
            health -= enemy.damage;
            Vector3 reactVec = transform.position - enemy.transform.position;
            StartCoroutine(OnDamage(reactVec));
        }
    }

    void Attack()
    {
        if (equipWeapon == null) //무기가 있을때만 실행되도록 장비체크
            return;
        fireDelay += Time.deltaTime; //공격딜레이에 시간을 더해줌
        isFireReady = equipWeapon.rate < fireDelay; //공격가능 여부 확인

        // if (ctrlDown && isFireReady)
        // {
        //     equipWeapon.Use(); //조건 충족시 Use 실행
        //     anim.SetTrigger("Attack1");
        //     fireDelay = 0; //공격딜레이
        // }
        if (Time.time - lastComboTime > maxComboDelay || comboStack > 2)
        {
            comboStack = 0;
        }
        if (ctrlDown && isFireReady)
        {
            equipWeapon.Use();
            // Debug.Log(comboStack);
            lastComboTime = Time.time;
            comboStack++;
            if (comboStack == 1)
            {
                anim.SetTrigger("combo1");
                audioSource.clip = attack1;
                audioSource.Play();
            }
            else if (comboStack == 2)
            {
                anim.SetTrigger("combo2");
                audioSource.clip = attack2;
                audioSource.Play();
            }
            else
            {
                anim.SetTrigger("combo3");
                audioSource.clip = attack1;
                audioSource.Play();
            }
            fireDelay = 0;
        }
    }

    void handleExp()
    {
        if (exp >= 100 + level * 10)  // 레벨 * 10% 의 가중치를 둔다
        {
            level++;
            exp = 0;
        }

    }
    IEnumerator OnDamage(Vector3 reactVec)
    {
        reactVec = reactVec.normalized;
        reactVec += Vector3.up;
        reactVec.y = 0;
        rigid.AddForce(reactVec * 200, ForceMode.Impulse);
        yield return new WaitForSeconds(1.0f);
    }
}