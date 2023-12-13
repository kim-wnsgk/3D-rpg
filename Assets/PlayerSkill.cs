using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        Player player = transform.parent.gameObject.GetComponent<Player>();
        damage = 10 + player.str*10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other){
            // other.gameObject.GetComponent<enemy>().curHealth -= damage;
        Player player = transform.parent.gameObject.GetComponent<Player>();
        damage = 10 + player.str*10;
    
    if (other.tag == "Enemy" && player.equipWeapon && !player.isFireReady)  // 공격
        {
            enemy enemy = other.GetComponent<enemy>();
            enemy.curHealth -= (damage);  // 레벨에 따라 추가데미지
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
                player.exp += enemy.exp;  // 경험치 쌓임
                player.coin += enemy.exp;
            }
        }
    }
}
