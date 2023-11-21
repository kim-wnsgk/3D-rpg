using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public class Player : MonoBehaviour
{
    public float moveSpeed = 10.0f; // 이동 속도
    public GameObject[] weapons;  // Weapons들 저장
    public bool[] hasWeapons;  // 해당 인덱스 weapon 갖고 있는지

    public int coin;
    public int health;
    public int mana;
    public int hasGrenades;
    public int maxCoin;
    public int maxHealth;
    public int maxMana;
    public int maxHasGrenades;

    Rigidbody rigid;
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

    Vector3 moveVec;
    public Animator anim;
    public TrailRenderer trailRenderer;

    GameObject nearObject;
    Weapon equipWeapon;
    private float fireDelay = 0.4f;
    private bool isFireReady = true;

    int weaponIndex = -1;

    void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        isJump = false;

        // equipWeapon = weapons[0];
        // equipWeapon.SetActive(true);  // 첫번째 무기 기본 설정

    }

    void Update()
    {
        GetInput();
        Move();

        Jump();
        Attack();
        Interaction();
        Swap();
    }

    void FixedUpdate()
    {
        FreezeRotation();
        StopToWall();
    }
    void StopToWall()
    {
        Debug.DrawRay(transform.position, transform.forward*5, Color.green);
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
        if(!isBorder)
            transform.position += moveVec * (isRun ? moveSpeed * 2f : moveSpeed) * Time.deltaTime;

        transform.LookAt(transform.position + moveVec);

        // 달릴 때 트레일 렌더러 활성화/비활성화
        if (isRun)
        {
            trailRenderer.emitting = true;
        }
        else
        {
            trailRenderer.emitting = false;
        }

    }

    void Jump()
    {
        if (!isJump && spaceDown)
        {
            anim.SetTrigger("Jump");
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            isJump = true;
            Debug.Log(isJump);
        }
    }

    // 점프 연속으로 되지 않도록
    void OnCollisionEnter(Collision collision)
    {
        //땅에 닿으면 점프 초기화
        if (collision.gameObject.tag == "Ground")
        {
            isJump = false;
            Debug.Log(isJump);

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
        if (eDown && nearObject != null && !isJump)
        {
            if (nearObject.tag == "Melee")
            {
                Item item = nearObject.GetComponent<Item>();
                int weaponIndex = item.value;
                hasWeapons[weaponIndex] = true;  // 해당 무기는 소유한 상태가 됨

                Destroy(nearObject);
            }
        }
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
        if (other.tag == "Item")
        {
            Item item = other.GetComponent<Item>();
            switch (item.type)
            {
                case Item.Type.Coin:
                    coin += item.value;
                    if (coin > maxCoin)
                        coin = maxCoin;
                    break;
                case Item.Type.Heart:
                    health += item.value;
                    if (health > maxHealth)
                        health = maxHealth;
                    break;
                case Item.Type.Mana:
                    mana += item.value;
                    if (mana > maxMana)
                        mana = maxMana;
                    break;
                case Item.Type.Grenade:
                    hasGrenades += item.value;
                    if (hasGrenades > maxHasGrenades)
                        hasGrenades = maxHasGrenades;
                    break;
            }
            Destroy(other.gameObject);
        }
    }

    void Attack()
    {
        if (equipWeapon == null) //무기가 있을때만 실행되도록 장비체크
            return;

        fireDelay += Time.deltaTime; //공격딜레이에 시간을 더해줌
        isFireReady = equipWeapon.rate < fireDelay; //공격가능 여부 확인

        if (ctrlDown && isFireReady)
        {
            equipWeapon.Use(); //조건 충족시 Use 실행
            anim.SetTrigger("Attack1");
            fireDelay = 0; //공격딜레이
        }
    }

}