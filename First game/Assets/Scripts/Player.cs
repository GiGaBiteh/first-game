using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Movement section
    //This is all put in fixed update, nothing has to be saved here either.
    public float JumpHeight, MovementSpeed;
    public Animator MovementAnimation;
    public static bool GroundCheck;
    public Rigidbody2D Rigidbody;
    public GameObject PlayerObject;
    bool WalkingLeft, WalkingRight;
    void FixedUpdate()
    {
        if (Input.GetKey("d"))
        {
            Rigidbody.velocity = new Vector2(+MovementSpeed, Rigidbody.velocity.y);
            WalkingRight = true;
            WalkingLeft = false;
        }
        else
        {
            if (Input.GetKey("a"))
            {
                Rigidbody.velocity = new Vector2(-MovementSpeed, Rigidbody.velocity.y);
                WalkingRight = false;
                WalkingLeft = true;
            }
            else
            {
                Rigidbody.velocity = new Vector2(0, Rigidbody.velocity.y);
                WalkingRight = false;
                WalkingLeft = false;
            }
        }
        if (Input.GetKey("space") && GroundCheck == true)
        {
            Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, JumpHeight);
            GroundCheck = false;
        }
        if (Input.GetKey("left shift"))
        {
            MovementSpeed = 15;
        }
        else
        {
            MovementSpeed = 10;
        }

        //Animations
        if (WalkingRight == true)
        {
           MovementAnimation.SetBool("WalkingLeft", false);
           MovementAnimation.SetBool("WalkingRight", true);
           MovementAnimation.SetBool("Idle", false);
        }
        if (WalkingLeft == true)
        {
           MovementAnimation.SetBool("WalkingLeft", true);
           MovementAnimation.SetBool("WalkingRight", false);
           MovementAnimation.SetBool("Idle", false);
        }
        if (WalkingLeft == false && WalkingRight == false)
        {
           MovementAnimation.SetBool("WalkingLeft", false);
           MovementAnimation.SetBool("WalkingRight", false);
           MovementAnimation.SetBool("Idle", true);
        }
    }
}
