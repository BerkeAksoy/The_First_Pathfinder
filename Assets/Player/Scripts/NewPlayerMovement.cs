using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewPlayerMovement : MonoBehaviour
{
    [SerializeField] float startDashTime = 0.3f; // CHANGE --- Better starting number.
    [SerializeField] float dashSpeed = 15f; // CHANGE --- Better starting number.
    [SerializeField] float startDashCooldownTime = 0.3f; // CHANGE --- Better starting number.
    [SerializeField] float moveSpeed = 3f;
    private PlayerInput input = null;
    private Vector2 moveVector = Vector2.zero;
    private Rigidbody2D rb = null;

    float currentDashTime;
    float currentDashCooldownTime;
 
    bool canDash = true;
    bool canMove = true; // CHANGE --- Need to disable movement when dashing.


    public PlayerInput GetPlayerInput()
    {
        return input;
    }


    private void Awake(){
        input = new PlayerInput();
        rb = GetComponent<Rigidbody2D>();
    }    

    private void OnEnable(){
        input.Enable();
        input.Player.Movement.performed += OnMovementPerformed;
        input.Player.Movement.canceled += OnMovementCanceled;
    }

    private void OnDisable(){
        input.Disable();
        input.Player.Movement.performed -= OnMovementPerformed;
        input.Player.Movement.canceled -= OnMovementCanceled;
    }

    void Update()
    {
        float dash = input.Player.Dash.ReadValue<float>();
        if (canDash && dash == 1)
            StartCoroutine(Dash());
    }

    private void FixedUpdate(){
        if (canMove == true){
            rb.velocity = moveVector * moveSpeed;
        }
    }

    private void OnMovementPerformed(InputAction.CallbackContext value){
        moveVector = value.ReadValue<Vector2>();
    }

    private void OnMovementCanceled(InputAction.CallbackContext value){
        moveVector = Vector2.zero;
    }

    IEnumerator Dash()
    {
        canDash = false;
        canMove = false; // CHANGE --- Need to disable movement when dashing.
        currentDashTime = startDashTime; // Reset the dash timer.
        currentDashCooldownTime = startDashCooldownTime;
        Vector2 dashDirection = moveVector;

 
        while (currentDashTime > 0f)
        {
            currentDashTime -= Time.deltaTime; // Lower the dash timer each frame.
 
            rb.velocity = dashDirection * dashSpeed; // Dash in the direction that was held down.
                                                 // No need to multiply by Time.DeltaTime here, physics are already consistent across different FPS.
 
            yield return null; // Returns out of the coroutine this frame so we don't hit an infinite loop.
        }

        rb.velocity = new Vector2(0f, 0f); // Stop dashing.
        canMove = true; // CHANGE --- Need to enable movement after dashing.

        while (currentDashCooldownTime > 0f)
        {
            currentDashCooldownTime -= Time.deltaTime; // Lower the dash cooldown timer each frame.
 
            yield return null; // Returns out of the coroutine this frame so we don't hit an infinite loop.
        }

        canDash = true;
    }
}
