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
    [SerializeField] private bool isIdel => ani.GetFloat("Speed") == 0;
    [SerializeField] private bool isWalk => ani.GetFloat("Speed") != 0 && ani.GetFloat("Speed") < 1.1f;
    [SerializeField] private bool isJump = false;
    [SerializeField] private bool isCrouch = false;

    [Header("Configuration Move")]
    
    [SerializeField] private float moveSpeed = 1.5f;
    [SerializeField] private float moveSpeedRunning = 1f;
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
            isJump = false; 
            Run();
            Crouch();
        }
        else if(!onGround)
        {
            ani.SetBool("IsJump", true);
        }
        rb.AddForce(Vector2.down * gravity);
        
        Move();
        Jump();
        
    }
    
    private void Move()
    {
        horizontal = Input.GetAxis("Horizontal") * moveSpeedRunning;
        SetAnimationMovement(MathF.Abs(horizontal));
        bool horizontalDown = horizontal != 0;
        if (horizontalDown)
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, horizontal < 0 ? 180 : 0, transform.eulerAngles.z);
        
        targetVelocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref refVelocity, smoothTime);
    }
    private void SetAnimationMovement(float speed)
    {
        ani.SetFloat("Speed", speed, blendDamp, Time.deltaTime);
    }
    private void Run()
    {
        moveSpeedRunning = Input.GetKey(KeyCode.LeftShift) ? 2.5f : 1f ;

    }
    private void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.C) && !isWalk)
        {
            Debug.Log(isCrouch);
            isCrouch = !isCrouch;
            Debug.Log(isCrouch);
            ani.SetBool("Crouch", isCrouch);
        }
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            isJump = true;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce);
        }
        
    }
    

    private void OnDrawGizmos()
    {
        Gizmos.color = onGround == true ? Color.blue : Color.red;
        Gizmos.DrawWireSphere(groundCheckPoint.position, groundCheckRadius);
        //Gizmos.DrawLine(groundCheckPoint.position - new Vector3(0.5f, 0, 0), groundCheckPoint.position + new Vector3(0.5f, 0, 0));

    }
}
