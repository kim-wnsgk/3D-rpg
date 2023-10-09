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
    public GameObject obj;

    void Start()
    {
        // character = GetComponent<CharacterController>();
        // cameraTransform = Camera.main.transform;
        // cameraTransform.parent = transform; // 카메라를 물체의 자식으로 설정
        // cameraTransform.localPosition = new Vector3(0, 2, -5); // 카메라의 로컬 위치 조절
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        jump = false;
    }

    void Update()
    {
        Move();
        RotateWithMouse();
        Jump();
        
    }

    void RotateWithMouse()
    {
        // float mouseX = Input.GetAxis("Mouse X");
        // float mouseY = Input.GetAxis("Mouse Y");

        // // 물체 회전
        // Vector3 rotation = new Vector3(0, mouseX * rotationSpeed, 0);
        // transform.Rotate(rotation);

        // 카메라 회전
        // cameraTransform.Rotate(-mouseY * rotationSpeed, 0, 0);

        // // 물체와 카메라가 항상 같은 방향을 보도록 설정
        // cameraTransform.LookAt(transform);
    }

    void Move()
    {
        anim.SetBool("Walk", moveVec != Vector3.zero);
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        // float moveX = Input.GetAxis("Horizontal");
        // float moveZ = Input.GetAxis("Vertical");
        moveVec = new Vector3(hAxis,0,vAxis).normalized;
        transform.position += moveVec * moveSpeed * Time.deltaTime;
        // Vector3 moveDirection = transform.TransformDirection(new Vector3(moveX, 0, moveZ));
        // character.SimpleMove(moveDirection * moveSpeed);
        transform.LookAt(transform.position + moveVec);
        
    }
    void Jump()
    {
        if (!jump && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Jump");
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            jump = true;
            Debug.Log(jump);
        // rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }
        
    
    void OnCollisionEnter(Collision collision)
    {
        //땅에 닿으면 점프 초기화
        if (collision.gameObject.tag == "Ground")
        {
            jump = false;
            Debug.Log(jump);

        }

    }
}