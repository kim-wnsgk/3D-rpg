using UnityEngine;
using UnityEngine.AI;

public class BossNavCollider : MonoBehaviour
{
    Boss1 parentScript; // Enemy 스크립트 참조 변수
    bool isnav = false; // isnav 변수 초기화

    // Start 함수에서는 NavMeshAgent 컴포넌트를 가져옵니다.
    void Start()
    {
        // 부모 객체의 Enemy 스크립트 가져오기
        if (transform.parent != null)
        {
            parentScript = transform.parent.GetComponent<Boss1>();
        }
    }

    // OnTriggerEnter 함수에서 Player와 충돌 시 isnav 변수를 true로 설정합니다.
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (parentScript != null)
            {
                parentScript.inBound = true;
                parentScript.isNav = true; // Enemy 스크립트의 isNav 값을 변경
                
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (parentScript != null)
            {
                parentScript.inBound = true;
                parentScript.isNav = false; // Enemy 스크립트의 isNav 값을 변경
            }
        }
    }
}
