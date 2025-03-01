using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerMoveScript : MonoBehaviour
{

    [SerializeField] float runSpeed = 5f;
    Vector2 moveInput;

    Animator myAnimator;

    CapsuleCollider2D[] myCapsuleColliders;
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
        defaultGravity = myRigidbody.gravityScale;
        animatorSpeed = myAnimator.speed;
        // Debug.Log("myCapsuleCollider:"+ myCapsuleColliders.Length);
    }

    // Update is called once per frame
    void Update()
    {
        FlipSprite();
        Run();
        ClimbLadder();
    }

    void OnMove(InputValue value){

        moveInput = value.Get<Vector2>();
       // transform.Translate(moveInput.x * Time.deltaTime, 0, moveInput.y * Time.deltaTime);
        transform.Translate(moveInput.x * Time.deltaTime, moveInput.y * Time.deltaTime, 0);
    }
    void Run(){
        Vector2 playVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody.linearVelocity.y);
        myRigidbody.linearVelocity = playVelocity;
        myAnimator.SetBool("isRunning", Mathf.Abs(myRigidbody.linearVelocityX) > Mathf.Epsilon);
        // myAnimator.SetBool("isJumping", myRigidbody.linearVelocity.y > Mathf.Epsilon);
    }
    void ClimbLadder(){
        for(int i = 0; i< myCapsuleColliders.Length; i++){
           //  Debug.Log("-->Collider: Ladder "+ i + "Layer:"+ LayerMask.GetMask("Ladder").ToString()+" Collider:"+myCapsuleColliders[i].ToString());
            if(myCapsuleColliders[i].IsTouchingLayers(LayerMask.GetMask("Ladder"))){
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


        for(int i = 0; i< myCapsuleColliders.Length; i++){
            // Debug.Log("-->Collider: "+ i + "Layer:"+ LayerMask.GetMask("Ground").ToString()+" Collider:"+myCapsuleColliders[i].ToString());
            if(myCapsuleColliders[i].IsTouchingLayers(LayerMask.GetMask("Ground"))){
                // myAnimator.SetBool("isJumping", false);
                // Debug.Log("Player is on the ground: "+ myCapsuleColliders[i].name);
                continue;
            }
            // Debug.Log("Player is in the air");
            return;
        }
        if (value.isPressed)
        {
          //  myRigidbody.AddForce(new Vector2(0f, 5f), ForceMode2D.Impulse);
         //  myRigidbody.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        //    Debug.Log("Jumping");
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
}
