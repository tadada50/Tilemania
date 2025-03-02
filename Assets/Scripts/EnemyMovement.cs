using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    float timerCooldown = 0.2f;
    float timer = 0f;
    bool flipLockout = false;
    Rigidbody2D myRigidbody;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        myRigidbody.linearVelocity = new Vector2 (moveSpeed, 0f);
        if ( flipLockout == true )
        {
            timer -= Time.deltaTime;
        
            if ( timer <= 0 )
            {
                // Debug.Log("Lockout = false");
                timer = timerCooldown;
                flipLockout = false;
                
                // a delayed action could be called from here
                // once the lock-out period expires
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(!flipLockout){
            flipLockout = true;
            moveSpeed = -moveSpeed;
            FlipEnemyFacing();
        }
        // if(collision.tag == "Ground" && !flipLockout){
        //     flipLockout = true;
        //     moveSpeed = -moveSpeed;
        //     FlipEnemyFacing();
        // }

            // if(lastFlipElapsed > flipRateMinSec){
            //     moveSpeed = -moveSpeed;
            //     lastFlipElapsed = 0f;
            //     FlipEnemyFacing();
            // }else{
            //     lastFlipElapsed += Time.deltaTime;
            //     Debug.Log("lastFlipElapsed: "+lastFlipElapsed);
            // }


        // Debug.Log("Enemy Exiting Trigger");

    }
    void FlipEnemyFacing(){
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody.linearVelocityX)),1f);
    }
}
