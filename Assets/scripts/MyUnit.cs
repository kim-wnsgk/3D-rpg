using System;
 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;


 public class MyUnit : MonoBehaviour
 {
     public float moveSpeed = 10.0f; // 이동 속도
     public float rotationSpeed = 5.0f; // 회전 속도
     // private CharacterController character; // 캐릭터 컨트롤러
     // private Transform cameraTransform; // 카메라의 Transform
     Rigidbody rigid;
     bool jump;
     public float jumpPower = 3f;
     float hAxis;
     float vAxis;
     Vector3 moveVec;
     public Animator anim;
     public TrailRenderer trailRenderer;

     bool isBorder;

    //  Attack equipWeapon;
     private float fireDelay = 0.1f;
     private bool isFireReady = false;

     void Awake()
     {
         anim = GetComponent<Animator>();
         rigid = GetComponent<Rigidbody>();
         jump = false;

        //  equipWeapon = GetComponent<Attack>();
     }
     void Start(){
        Physics.IgnoreLayerCollision(0,6);
     }

     void Update()
     {

         Move();

         Jump();
         Attack();

     }


     void Move()
     {
         hAxis = Input.GetAxisRaw("Horizontal");
         vAxis = Input.GetAxisRaw("Vertical");

         bool isRun = Input.GetKey(KeyCode.LeftShift);

         anim.SetBool("Walk", moveVec != Vector3.zero);

         // Shift 버튼을 누르는 동안에만 달리기 애니메이션 활성화
         anim.SetBool("Run", isRun);

         moveVec = new Vector3(hAxis, 0, vAxis).normalized;
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
         if (!jump && Input.GetKeyDown(KeyCode.Space))
         {
             anim.SetTrigger("Jump");
             rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
             jump = true;
             Debug.Log(jump);
         }
     }

     // 점프 연속으로 되지 않도록
     void OnCollisionEnter(Collision collision)
     {
         //땅에 닿으면 점프 초기화
         if (collision.gameObject.tag == "Ground")
         {
             jump = false;
             Debug.Log(jump);

         }

     }

     void Attack()
     {
         // if (equipWeapon == null) //무기가 있을때만 실행되도록 장비체크
         //     return;

         fireDelay += Time.deltaTime; //공격딜레이에 시간을 더해줌
        //  isFireReady = equipWeapon.rate < fireDelay; //공격가능 여부 확인

         if (Input.GetKeyDown(KeyCode.LeftControl) && isFireReady)
         {
            //  equipWeapon.Use(); //조건 충족시 Use 실행
             anim.SetTrigger("Attack1");
             fireDelay = 0; //공격딜레이
         }
     }

 } 