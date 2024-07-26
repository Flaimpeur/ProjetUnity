using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float runSpeed;
    public float currentSpeed;

    public Rigidbody2D rb;
    public Animator animator;

    private bool canDash = true;
    private bool isDashing;
    public float dashingCooldown = 0.2f;
    public float dashingTime = 0.2f;

    private Vector2 moveInput;
    private Vector2 lastMoveInput; // Nouvelle variable pour stocker la dernière direction de mouvement
    public bool isMoving { get; private set; }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
            if (!isMoving)
            {
                return;
            }

            animator.SetBool(AnimationStrings.isDashing, true);
            return;
        }
        else
        {
            animator.SetBool(AnimationStrings.isDashing, false);
        }

        // Update animator parameters
        // Utiliser lastMoveInput au lieu de moveInput si le personnage n'est pas en mouvement
        Vector2 direction = isMoving ? moveInput : lastMoveInput;
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);

        if (moveInput.sqrMagnitude > 0 && CanMove)
        {
            animator.SetFloat("Speed", currentSpeed);
            lastMoveInput = moveInput; // Mettre à jour la dernière direction de mouvement
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1")) && canDash && CanMove)
        {
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        else if (CanMove)
        {
            // Determine if the player is running
            bool isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) || Input.GetButton("Fire3");

            // Calculate the move speed
            currentSpeed = isRunning ? runSpeed : moveSpeed;

            // Move the player
            rb.MovePosition(rb.position + moveInput * currentSpeed * Time.fixedDeltaTime);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        isMoving = moveInput != Vector2.zero;
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        float speed = currentSpeed + 1;

        Vector2 dashDirection = moveInput.normalized; // Get the normalized direction of movement
        rb.velocity = dashDirection * speed;  // Apply dash velocity in the correct direction

        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
        rb.velocity = Vector2.zero; // Stop the dash movement
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}

