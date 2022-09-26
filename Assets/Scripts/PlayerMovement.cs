using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;

    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    
    void Update()
    {
        Run();
        FlipSprite();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnJump(InputValue value)
    {
        if(value.isPressed)
        {
            myRigidbody.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    private void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        bool isMoving = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if(isMoving)
        {
            myAnimator.SetBool("isRunning", isMoving);
        }
        
    }

    private void FlipSprite()
    {
        bool isMoving = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if(isMoving)
        {
            transform.localScale = new Vector2( Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
        else
        {
            myAnimator.SetBool("isRunning", false);
        }
    }
}
