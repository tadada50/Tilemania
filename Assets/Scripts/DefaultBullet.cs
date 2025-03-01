using UnityEngine;

public class DefaultBullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed=10f;
    Rigidbody2D myRigidBody;
    GameObject player;
    float xSpeed;
    [SerializeField] GameObject impactEffect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        xSpeed = player.transform.localScale.x * bulletSpeed; 
    }

    // Update is called once per frame
    void Update()
    {
        // myRigidBody.linearVelocityX = 10.0f;
        myRigidBody.linearVelocity = new Vector2(xSpeed,0);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy"){
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
        Instantiate(impactEffect,transform.position,transform.rotation);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);    
        Instantiate(impactEffect,transform.position,transform.rotation);
    }
}
