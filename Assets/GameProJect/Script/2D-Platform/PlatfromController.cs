using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class PlatfromController : MonoBehaviour
{
    public float horizontal { get; private set; }
    [Header("Status")]
    [SerializeField] private float hp = 100f;
    [SerializeField] private float mp = 100f;
    [SerializeField] private float sp = 100f;
    [SerializeField] private bool onGround = false;
    [SerializeField] private bool onJump = false;
    [SerializeField] private bool isJump = false;
    [SerializeField] private bool isIdel = false;
    [SerializeField] private bool isCrouch = false;
    [SerializeField] private bool isWalk = false;
    [SerializeField] private bool isRunning = false;

    [Header("Configuration Move")]
    [SerializeField] private float moveSpeed = 1.5f;
    [SerializeField] private float moveSpeedRunning;
    [SerializeField] private float gravity = 1f;
    [SerializeField] private float jumpForce = 550f;
    [SerializeField] private float smoothTime = 0.01f;
    [SerializeField] private float blendDamp = 0.025f;
    
    [Header("Ground Check")]
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float groundCheckRadius = 0.2f;
    
    [Header("Object Reference")]
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private Animator ani = null;
    [SerializeField] private Transform groundCheckPoint = null;
    [SerializeField] private CanvasUiManager uiManager = null;

    public float HP => hp;
    public float MP => mp;
    public float SP => sp;

    private Vector2 refVelocity = Vector2.zero;
    private Vector2 targetVelocity = Vector2.zero;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GameObject.FindWithTag("Character").GetComponent<Animator>();
        uiManager = GameObject.FindWithTag("UIManager").GetComponent<CanvasUiManager>();
    }
    private void Update()
    {
        onGround = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayerMask) != null;
        horizontal = Input.GetAxis("Horizontal") * moveSpeedRunning;
        isJump = Input.GetKey(KeyCode.Space);
        isCrouch = Input.GetKey(KeyCode.C);
        isRunning = Input.GetKey(KeyCode.LeftShift);
        isWalk = horizontal != 0;
        isIdel = !isWalk;

        if (isRunning)
        {
            uiManager.Reduce(uiManager.SPSlider,uiManager.SPPoint, 0.05f);
        }
    }
    private void FixedUpdate()
    {
        SetAnimation();
        if (onGround)
        {
            Run();
        }
        else if (!onGround)
        {

        }
        Move();
        Jump();

    }
    private void SetAnimation()
    {
        if (horizontal == 0)
        {
            ani.SetFloat("Speed", 0);
        }
        ani.SetBool("IsJump", onJump);
        ani.SetBool("Crouch", isCrouch);
    }
    private void SetAnimationMovement(float speed)
    {
        ani.SetFloat("Speed", speed, blendDamp, Time.deltaTime);
    }

    private void Move()
    {
        rb.AddForce(Vector2.down * gravity);
        SetAnimationMovement(MathF.Abs(horizontal));
        if (isWalk)
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, horizontal < 0 ? 180 : 0, transform.eulerAngles.z);
        targetVelocity = new Vector2(horizontal * moveSpeed, rb.velocity.y); 
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref refVelocity, smoothTime);
    }
    private void Run()
    {
        moveSpeedRunning = Input.GetKey(KeyCode.LeftShift) ? 2.5f : 1f;
    }
    private void Jump()
    {
        if (isJump && onGround && !onJump)
        {
            onJump = true;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            onJump = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = onGround == true ? Color.blue : Color.red;
        Gizmos.DrawWireSphere(groundCheckPoint.position, groundCheckRadius);
        //Gizmos.DrawLine(groundCheckPoint.position - new Vector3(0.5f, 0, 0), groundCheckPoint.position + new Vector3(0.5f, 0, 0));

    }
}
