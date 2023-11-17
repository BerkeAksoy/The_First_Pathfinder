using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class PlayerMovement : MonoBehaviour
{
    private Animator animator;
    Vector2 movement;
 
    bool isRight = false; //Player Sprite Flip
    public Transform weapon;
    bool wRight, wLeft, wUp, wDown, wUL, wUR, wDL, wDR, inIdle; 
 
    private void Awake()
    {
        animator = transform.Find("P_Sprite").GetComponent<Animator>();
        weapon = transform.Find("Weapon");
    }

    private void MakeAllDirectionsFalse()
    {
        wRight = false;
        wLeft = false;
        wUp = false;
        wDown = false;
        wUL = false;
        wUR = false;
        wDL = false;
        wDR = false;
        inIdle = false;
    }

    void Update()
    {
        //Player Movement Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.x == 0 && movement.y == 0 && !inIdle)
        {
            MakeAllDirectionsFalse();
            animator.SetTrigger("Idle");
            inIdle = true;
        }

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            if (!wUR)
            {
                MakeAllDirectionsFalse();
                animator.SetTrigger("W_UR");
                wUR = true;
            }
        }

        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            if (!wDR)
            {
                MakeAllDirectionsFalse();
                animator.SetTrigger("W_DR");
                wDR = true;
            }

        }

        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
        {
            if (!wUL)
            {
                MakeAllDirectionsFalse();
                animator.SetTrigger("W_UL");
                wUL = true;
            }
        }

        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S))
        {
            if (!wDL)
            {
                MakeAllDirectionsFalse();
                animator.SetTrigger("W_DL");
                wDL = true;
            }
        }

        else if (Input.GetKey(KeyCode.W))
        {
            if (!wUp)
            {
                MakeAllDirectionsFalse();
                animator.SetTrigger("W_UP");
                wUp = true;
            }
        }

        else if (Input.GetKey(KeyCode.A))
        {
            if (!wLeft)
            {
                MakeAllDirectionsFalse();
                animator.SetTrigger("W_LEFT");
                wLeft = true;
                FlipWeaponSprite();
            }

        }

        else if (Input.GetKey(KeyCode.S))
        {
            if (!wDown)
            {
                MakeAllDirectionsFalse();
                animator.SetTrigger("W_DOWN");
                wDown = true;
            }

        }

        else if (Input.GetKey(KeyCode.D))
        {
            if (!wRight)
            {
                MakeAllDirectionsFalse();
                animator.SetTrigger("W_RIGHT");
                wRight = true;
                FlipWeaponSprite();
            }

        }
    }
    void FlipWeaponSprite() //Player Sprite Flip Function
    {
        isRight = !isRight;
        //this.transform.Rotate(0f, 180f, 0f);
        weapon.transform.Rotate(0, 0, 180f);
    }
 
}