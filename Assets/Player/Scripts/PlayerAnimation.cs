using System.Collections;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Rigidbody2D rb; // CHANGE --- No need for public, we're already getting the reference on line 24.
    private Animator animator;

    private Transform weapons;    
    
    private Vector2 movement;
    [SerializeField] float moveSpeed; //Player Movement Speed
    [SerializeField] float startDashTime = 0.3f; // CHANGE --- Better starting number.
    [SerializeField] float dashSpeed = 15f; // CHANGE --- Better starting number.
    [SerializeField] float startDashCooldownTime = 0.3f; // CHANGE --- Better starting number.
    private PlayerInput input;

    bool canDash = true;
    bool canMove = true; // CHANGE --- Need to disable movement when dashing.
                         // 


    private float direction = 0;
    public float getDirection() { return direction; }




    private void Awake()
    {
        input = GetComponent<NewPlayerMovement>().GetPlayerInput();
        weapons = transform.Find("Weapons");        

        animator = transform.Find("P_Sprite").GetComponent<Animator>();                
        
    }


    void Update()
    {
        //Player Movement Input

        //movement.x = Input.GetAxisRaw("Horizontal");
        //movement.y = Input.GetAxisRaw("Vertical");

        movement = input.Player.Movement.ReadValue<Vector2>();
        

        if (canMove)
        {
            if (movement.x != 0 || movement.y != 0)
            {
                animator.SetFloat("Horizontal", movement.x);
                animator.SetFloat("Vertical", movement.y);

                direction = (Mathf.Atan2(movement.y, movement.x) * 180 / Mathf.PI);


                for(int i = 0; i < weapons.childCount; i++)
                {
                    weapons.GetChild(i).GetComponent<Weapon>().UpdatePosition(direction);
                }
                

                weapons.localEulerAngles = new Vector3(0f, 0f, direction + 90f);

            }


            animator.SetFloat("Speed", movement.sqrMagnitude);


            if (canDash && Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetTrigger("Roll");                
            }            
        }
        
    }




    public IEnumerator StunCoroutine(float stunTime)
    {
        canMove = false;
        rb.velocity = new Vector3(0f, 0f, 0f);
        animator.SetFloat("Horizontal", 0f);
        animator.SetFloat("Vertical", 0f);
        animator.SetFloat("Speed", 0f);
        Debug.Log("stunned");

        /* - TODO - animation stun */

        yield return new WaitForSeconds(stunTime);
        canMove = true;
        Debug.Log("no more stunned");
    }




}