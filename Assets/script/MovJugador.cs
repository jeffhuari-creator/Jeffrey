using UnityEngine;
using UnityEngine.InputSystem;

public class MovJugador : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float checkRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 moveInput;
    private bool estaEnElSuelo;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        HandleInput();
        CheckGround();
        ApplyMovement();
        HandleAnimation();
    }

    private void HandleInput()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard != null)
        {
            Vector2 input = Vector2.zero;
            if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed) input.x -= 1;
            if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed) input.x += 1;
            
            moveInput = input;

            //Salto
            if ((keyboard.spaceKey.wasPressedThisFrame || keyboard.wKey.wasPressedThisFrame) && estaEnElSuelo)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            }
        }
    }

    private void CheckGround()
    {
        estaEnElSuelo = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
    }

    private void ApplyMovement()
    {
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);

        if (moveInput.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveInput.x) * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    private void HandleAnimation()
    {
        if (animator == null) return;

        animator.SetFloat("Speed", Mathf.Abs(moveInput.x));

        animator.SetBool("isGround", estaEnElSuelo);

        animator.SetFloat("inAir", rb.linearVelocity.y);
    }
}