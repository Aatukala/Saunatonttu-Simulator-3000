using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float speed = 5f;      
    public float jumpForce = 7f;    

    private Rigidbody rb;
    private bool isGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
     
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");     

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
        Vector3 newVelocity = movement * speed;
        newVelocity.y = rb.linearVelocity.y; 
        rb.linearVelocity = newVelocity;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }


}
