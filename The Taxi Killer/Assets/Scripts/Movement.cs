/*
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    float currentSpeed;
    [SerializeField] Rigidbody rb;
    Vector3 direction;
    bool isGrounded = true;
    [SerializeField] float jumpForce = 7f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        direction = new Vector3(moveHorizontal, 0.0f, moveVertical);
        direction = transform.TransformDirection(direction);
        if (direction.x != 0 || direction.z != 0)
        {
            //anim.SetBool("Run", true);
            //if(!characterSounds.isPlaying && isGrounded)
            //{
            //    characterSounds.Play();
            //}
        }
        if (direction.x == 0 && direction.z == 0)
        {
            //anim.SetBool("Run", false);
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            isGrounded = false;
            //anim.SetBool("Jump", true);
            //characterSounds.Stop();
            //AudioSource.PlayClipAtPoint(jump, transform.position);
        
        }

    }
    void FixedUpdate()
    {
        rb.MovePosition(transform.position + direction * currentSpeed * Time.deltaTime);
    }
}
*/

using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundMask;

    private Vector3 direction;
    private bool isGrounded;

    private void Awake()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    private void Update()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        direction = new Vector3(moveHorizontal, 0f, moveVertical).normalized;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void FixedUpdate()
    {
        Vector3 groundPos = groundCheck != null
            ? groundCheck.position
            : transform.position + Vector3.down * 0.6f;

        isGrounded = Physics.CheckSphere(groundPos, groundCheckRadius, groundMask);

        Vector3 move = transform.TransformDirection(direction);
        rb.MovePosition(transform.position + move * speed * Time.fixedDeltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 groundPos = groundCheck != null
            ? groundCheck.position
            : transform.position + Vector3.down * 0.6f;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundPos, groundCheckRadius);
    }
}