using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerMoveScript : MonoBehaviour
{

    // [SerializeField] GameObject backGround;
    // Rigidbody2D backgroundRigitBody;
    // [SerializeField] float backGroundMoveFactorX;
    // [SerializeField] float backGroundMoveFactorY;
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float maxVelocity = 15f;
    // [SerializeField] float maxGravity = 20f;
    bool isAlive = true;
    Vector2 moveInput;
    Animator myAnimator;

    CapsuleCollider2D[] myCapsuleColliders;
    BoxCollider2D[] myFeetColliders;
    Rigidbody2D myRigidbody;
    [SerializeField] float jumpSpeed = 8f;
    [SerializeField] float climbSpeed = 5f;
    float defaultGravity = 5f;
    float animatorSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleColliders = GetComponents<CapsuleCollider2D>();
        myFeetColliders = GetComponents<BoxCollider2D>();
        defaultGravity = myRigidbody.gravityScale;
        animatorSpeed = myAnimator.speed;

        // backgroundRigitBody = backGround.GetComponentInChildren<Rigidbody2D>();
        // Debug.Log("myCapsuleCollider:"+ myCapsuleColliders.Length);
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAlive){
            return;
        }
     //   CapVelocity();
        // CapGravity();
        FlipSprite();
        Run();
        ClimbLadder();
 
    }

 /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        // string layerName = LayerMask.LayerToName(other.gameObject.layer);
        //  Debug.Log("Collider layer: "+other.gameObject.layer + " Collider layer: " + LayerMask.GetMask("Hazard"));
        // if(other.gameObject.layer == LayerMask.GetMask("Enemies")){ // Enemy layer
        //     Die();
        // }

        if(myFeetColliders[0].IsTouchingLayers(LayerMask.GetMask("Enemies")) || myCapsuleColliders[0].IsTouchingLayers(LayerMask.GetMask("Enemies"))){
            // Debug.Log("Touching Enemies");
            myRigidbody.linearVelocity += new Vector2(0f,8f);
            myRigidbody.linearVelocityX = 0;
            if(isAlive)
                Die();
        }

        if(myFeetColliders[0].IsTouchingLayers(LayerMask.GetMask("Hazard")) || myCapsuleColliders[0].IsTouchingLayers(LayerMask.GetMask("Hazard"))){
            //  Debug.Log("Touching Hazard");
            if(isAlive)
                Die();
        }


    }
    void FixedUpdate()
    {
        if (myRigidbody.linearVelocity.magnitude > maxVelocity)
        {
            myRigidbody.linearVelocity = myRigidbody.linearVelocity.normalized * maxVelocity;
        }
    }
    void OnMove(InputValue value){
        if(!isAlive){
            return;
        }
        moveInput = value.Get<Vector2>();
       // transform.Translate(moveInput.x * Time.deltaTime, 0, moveInput.y * Time.deltaTime);
        transform.Translate(moveInput.x * Time.deltaTime, moveInput.y * Time.deltaTime, 0);
        // backGround.transform.Translate(moveInput.x * Time.deltaTime, moveInput.y * Time.deltaTime, 0);
    }
    void Run(){
        Vector2 playVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody.linearVelocity.y);
        myRigidbody.linearVelocity = playVelocity;
        myAnimator.SetBool("isRunning", Mathf.Abs(myRigidbody.linearVelocityX) > Mathf.Epsilon);

        // Vector2 backGroundVelocity = new Vector2(moveInput.x * runSpeed * backGroundMoveFactorX, myRigidbody.linearVelocity.y* backGroundMoveFactorY);
        // backgroundRigitBody.linearVelocity = backGroundVelocity;
        // myAnimator.SetBool("isJumping", myRigidbody.linearVelocity.y > Mathf.Epsilon);
    }
    void ClimbLadder(){
        for(int i = 0; i< myFeetColliders.Length; i++){
           //  Debug.Log("-->Collider: Ladder "+ i + "Layer:"+ LayerMask.GetMask("Ladder").ToString()+" Collider:"+myCapsuleColliders[i].ToString());
            if(myFeetColliders[i].IsTouchingLayers(LayerMask.GetMask("Ladder"))){
                // myAnimator.SetBool("isJumping", false);
                //  Debug.Log("Player is touching Ladder: "+ myCapsuleColliders[i].name);
                continue;
            }
            myRigidbody.gravityScale = defaultGravity;
            myAnimator.SetBool("isClimbing", false);
            myAnimator.speed = animatorSpeed;
            
            // Debug.Log("Player is in the air");
            return;
        }
        Vector2 playVelocity = new Vector2(myRigidbody.linearVelocity.x,moveInput.y * climbSpeed);
        myRigidbody.linearVelocity = playVelocity;
        myRigidbody.gravityScale = 0f;
        if(Math.Abs(playVelocity.y)>0.1){
            myAnimator.SetBool("isClimbing", true);
            myAnimator.speed = animatorSpeed;
        }else{
            // myAnimator.SetBool("isClimbing", false);
            if(myAnimator.GetBool("isClimbing")){
                myAnimator.speed = 0;
            }
            
        }
        // transform.Translate(moveInput.x * Time.deltaTime, 0, moveInput.z * Time.deltaTime);
    }
    void OnJump(InputValue value){
        if(!isAlive){
            return;
        }

        for(int i = 0; i< myFeetColliders.Length; i++){
            //  Debug.Log("-->Collider: "+ i + "Layer:"+ LayerMask.GetMask("Ground").ToString()+" Collider:"+myFeetColliders[i].ToString());
            if(myFeetColliders[i].IsTouchingLayers(LayerMask.GetMask("Ground"))){
                // myAnimator.SetBool("isJumping", false);
                //  Debug.Log("Player is on the ground: "+ myFeetColliders[i].name);
                continue;
            }
            //  Debug.Log("Player is in the air");
            return;
        }
        if (value.isPressed)
        {
          //  myRigidbody.AddForce(new Vector2(0f, 5f), ForceMode2D.Impulse);
         //  myRigidbody.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            // Debug.Log("JumpSpeed: "+ jumpSpeed);
           myRigidbody.linearVelocity += new Vector2(0f,jumpSpeed);
            // myAnimator.SetBool("isJumping", true);
        }
    }

    void FlipSprite(){
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.linearVelocityX) > 0.05;
       // bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.GetPointVelocity().x) > Mathf.Epsilon;



        if (playerHasHorizontalSpeed)
        {
            // Debug.Log("-->Linear Velocity X (ABS): " + Mathf.Abs(myRigidbody.linearVelocityX));
            // Debug.Log("HasHorizontalSpeed : " + playerHasHorizontalSpeed);
            // Debug.Log("<--Point Velocity X : " + myRigidbody.GetPointVelocity(myRigidbody.position).x);
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.linearVelocityX), 1f);
        }
    }

    void Die(){
        isAlive = false;
        myAnimator.SetTrigger("Dying");

    }
}
