using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;

public class PlatfromController : MonoBehaviour
{
    public float horizontal { get; private set; }

    [Header("Configuration Move")]
    
    [SerializeField] private float moveSpeed = 1.5f;
    [SerializeField] private float moveSpeedRunning = 3f;
    [SerializeField] private float gravity = 1f;
    [SerializeField] private float jumpForce = 570f;
    [SerializeField] private float smoothTime = 0.0167f;
    [SerializeField] private float blendDamp = 0.0167f;

    [Header("Ground Check")]
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float groundCheckRadius = 0.05f;
    

    [Header("Object Reference")]
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private Animator ani = null;
    [SerializeField] private Transform groundCheckPoint = null;

    [Header("Status")]
    [SerializeField] private bool onGround = false;
    

    private Vector2 refVelocity = Vector2.zero;
    private Vector2 targetVelocity = Vector2.zero;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GameObject.FindWithTag("Player").GetComponent<Animator>();
    }
    private void Update()
    {
        
    }
    private void FixedUpdate()
    {
        onGround = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayerMask) != null;
        if (onGround)
        {
            ani.SetBool("IsJump", false);
        }
        else if(!onGround)
        {
            horizontal = 0;
        }
        rb.AddForce(Vector2.down * gravity);
        Move();
        Jump();
    }
    
    private void Move()
    {
        horizontal = Input.GetAxis("Horizontal");
        bool horizontalDown = horizontal != 0;
        if (horizontalDown)
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, horizontal < 0 ? 180 : 0, transform.eulerAngles.z);
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            targetVelocity = new Vector2(horizontal * moveSpeed * moveSpeedRunning, rb.velocity.y);
            Break;
        }
        targetVelocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
        
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref refVelocity, smoothTime);
        
        ani.SetFloat("Speed", horizontal, blendDamp, Time.deltaTime);
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce);
            ani.SetBool("IsJump", true);
        }
        
    }
    private void Run()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            ani.SetBool("IsRunning", true);

        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            ani.SetBool("IsRunning", false);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = onGround == true ? Color.blue : Color.red;
        Gizmos.DrawWireSphere(groundCheckPoint.position, groundCheckRadius);
        //Gizmos.DrawLine(groundCheckPoint.position - new Vector3(0.5f, 0, 0), groundCheckPoint.position + new Vector3(0.5f, 0, 0));

    }
}
