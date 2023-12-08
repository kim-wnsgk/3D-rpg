using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    void Awake(){
        GameObject obj2 = GameObject.FindWithTag("Player");
        target = obj2.transform;
    }
    void Update()
    {
        transform.position = target.position + offset;
    }
    void LateUpdate()
    {
        // Player는 싱글톤이기에 전역적으로 접근할 수 있습니다.
        Vector3 direction = (target.position - transform.position).normalized;
        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, Mathf.Infinity,
                            1 << LayerMask.NameToLayer("EnvironmentObject"));

        for (int i = 0; i < hits.Length; i++)
        {
            TransparentObject[] obj = hits[i].transform.GetComponentsInChildren<TransparentObject>();

            for (int j = 0; j < obj.Length; j++)
            {
                obj[j]?.BecomeTransparent();
            }
        }
    }

}
