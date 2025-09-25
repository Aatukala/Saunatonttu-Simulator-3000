using UnityEngine;

[RequireComponent(typeof(UnityEngine.CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 5f;
    public float sprintSpeed = 8f;
    public float jumpHeight = 1.25f;
    public float gravity = -30f;

    [Header("Mouse")]
    public float mouseSensitivity = 2f;
    public Transform playerCamera;
    public float eyeHeight = 1.6f;

    private UnityEngine.CharacterController controller;
    private Vector3 velocity;
    private float yaw;
    private float pitch;

    void Start()
    {
        controller = GetComponent<UnityEngine.CharacterController>();

        if (playerCamera == null)
        {
            Debug.LogError("camera ist määrätty");
            enabled = false;
            return;
        }

   
        playerCamera.localPosition = new Vector3(0f, eyeHeight, 0f);

        yaw = transform.eulerAngles.y;
        pitch = 0f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMouseLook();
        HandleMovement();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -90f, 90f);

        transform.rotation = Quaternion.Euler(0f, yaw, 0f);
        playerCamera.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }

    void HandleMovement()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * inputX + transform.forward * inputZ;

     
        if (move.sqrMagnitude > 1f) move.Normalize();

        bool sprint = Input.GetKey(KeyCode.LeftShift);
        float speed = sprint ? sprintSpeed : walkSpeed;

        controller.Move(move * speed * Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f; 
        }

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
           
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}

