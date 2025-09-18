using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    public float jumpForce = 10f;        // Increased for Unity 6 gravity
    public float groundCheckDistance = 0.1f; // Small offset from feet

    [Header("Input Actions")]
    public InputActionReference moveAction;
    public InputActionReference jumpAction;

    private Rigidbody rb;
    private Vector2 moveInput;
    private bool jumpPressed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;

        moveAction.action.Enable();
        jumpAction.action.Enable();
    }

    void Update()
    {
        // Read input every frame
        moveInput = moveAction.action.ReadValue<Vector2>();
        if (jumpAction.action.WasPerformedThisFrame())
            jumpPressed = true;
    }

    void FixedUpdate()
    {
        HandleMovement();
        HandleJump();
    }

    private void HandleMovement()
    {
        // Calculate horizontal movement
        Vector3 move = transform.forward * moveInput.y + transform.right * moveInput.x;

        // Keep current vertical velocity
        Vector3 velocity = rb.linearVelocity;
        velocity.x = move.x * speed;
        velocity.z = move.z * speed;
        rb.linearVelocity = velocity;
    }

    private void HandleJump()
    {
        if (jumpPressed && IsGrounded())
        {
            Vector3 velocity = rb.linearVelocity;
            velocity.y = jumpForce;  // Directly set vertical velocity
            rb.linearVelocity = velocity;
        }
        jumpPressed = false;
    }

    private bool IsGrounded()
    {
        // Cast ray from slightly above the feet downwards
        float rayLength = groundCheckDistance + 0.05f;
        Vector3 origin = transform.position + Vector3.up * 0.05f;
        return Physics.Raycast(origin, Vector3.down, rayLength);
    }

    // Optional: visualize the ray in the editor
    void OnDrawGizmosSelected()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Vector3 origin = transform.position + Vector3.up * 0.05f;
            Gizmos.DrawLine(origin, origin + Vector3.down * (groundCheckDistance + 0.05f));
        }
    }
}
