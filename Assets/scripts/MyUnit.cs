using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyUnit : MonoBehaviour
{
    public float moveSpeed = 10.0f; // 이동 속도
    public float rotationSpeed = 5.0f; // 회전 속도
    private CharacterController character; // 캐릭터 컨트롤러
    private Transform cameraTransform; // 카메라의 Transform
    Rigidbody rigid;
    bool jump;
    public float jumpPower = 3f;

    void Start()
    {
        character = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        cameraTransform.parent = transform; // 카메라를 물체의 자식으로 설정
        cameraTransform.localPosition = new Vector3(0, 2, -5); // 카메라의 로컬 위치 조절

        rigid = GetComponent<Rigidbody>();
        jump = false;
    }

    void FixedUpdate()
    {
        Move();
        RotateWithMouse();
        if (!jump && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }


    void RotateWithMouse()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // 물체 회전
        Vector3 rotation = new Vector3(0, mouseX * rotationSpeed, 0);
        transform.Rotate(rotation);

        // 카메라 회전
        cameraTransform.Rotate(-mouseY * rotationSpeed, 0, 0);

        // 물체와 카메라가 항상 같은 방향을 보도록 설정
        cameraTransform.LookAt(transform);
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.TransformDirection(new Vector3(moveX, 0, moveZ));
        character.SimpleMove(moveDirection * moveSpeed);
    }

    void Jump()
    {
        if (!jump)
        {
            jump = true;
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            jump = false;
        }

    }
}