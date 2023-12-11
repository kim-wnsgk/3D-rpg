using UnityEngine;




public class SpellScript : MonoBehaviour
{
    
    [SerializeField]
    private float speed = default;

   
    private Rigidbody2D myRigidbody;

   
    private float damage;


    
    public Transform MyTarget { get; private set; }

    
    public Character source;



   
    private void Start()
    {
        
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    
    public void Initialize(Transform spellTarget, float spellDamage, Character spellSource)
    {
        
        MyTarget = spellTarget;

        damage = spellDamage;

        
        source = spellSource;
    }

   



    private void FixedUpdate()
    {
        
        if (MyTarget != null)
        {
            
            Vector2 direction = MyTarget.position - transform.position;

            
            myRigidbody.velocity = direction.normalized * speed;

           
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.CompareTag("HitBox") && collision.transform == MyTarget)
        {
            
            Character characterAttacked = collision.GetComponentInParent<Character>();

            
            speed = 0;

           
            
            //characterAttacked.TakeDamage(damage, source);

           

            
            GetComponent<Animator>().SetTrigger("impact");

           
            myRigidbody.velocity = Vector2.zero;

            
            MyTarget = null;
        }
    }
}