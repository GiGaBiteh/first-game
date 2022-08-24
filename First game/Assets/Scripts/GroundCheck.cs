using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public float DistanceToGround;
    void Update()
    {
        //Raycast
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);
        //Prevent error
        if (hit.collider)
        {
            //Check tag
            if (hit.collider.tag == "Blocks" || hit.collider.tag == "Spaceship")
            {
                //Calc dist to ground
                DistanceToGround = (hit.point.y - transform.position.y) * -1;
                //If dist < 0.1 return ground check as true
                if (DistanceToGround < 0.1)
                {
                    Player.GroundCheck = true;
                }
                else
                {
                    Player.GroundCheck = false;
                }
            }
        }
    }
}
