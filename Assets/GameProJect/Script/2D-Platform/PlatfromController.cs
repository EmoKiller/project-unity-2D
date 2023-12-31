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
    [Header("State of health")]
    [SerializeField] private int level = 1;
    [SerializeField] private float expUp = 1f;
    [SerializeField] private float expCurrent = 0f;
    [SerializeField] private float hp = 100f;
    [SerializeField] private float mp = 100f;
    [SerializeField] private float sp = 100f;

    [Header("Regenerate")]
    [SerializeField] private bool hpRegenerate = false;
    [SerializeField] private bool mpRegenerate = false;
    [SerializeField] private bool spRegenerate = false;
    [SerializeField] private float countDown = 0f;
    [SerializeField] private float startRegenerate = 3f;

    [Header("Status")]
    [SerializeField] private bool alive = false;
    [SerializeField] private bool isEnemy = false;
    [SerializeField] private bool theWall = false;
    [SerializeField] private bool isIdel = false;
    [SerializeField] private bool onGround = false;
    [SerializeField] private bool onJump = false;
    [SerializeField] private bool isJump = false;
    [SerializeField] private bool isCrouch = false;
    [SerializeField] private bool isWalk = false;
    [SerializeField] private bool isRunning = false;
    [SerializeField] private bool isKick = false;
    [SerializeField] private bool isHoldWeapon = false;

    [Header("Configuration Move")]
    [SerializeField] private float moveSpeed = 1.5f;
    [SerializeField] private float moveSpeedRunning;
    [SerializeField] private float gravity = 1f;
    [SerializeField] private float jumpForce = 550f;
    [SerializeField] private float smoothTime = 0.01f;
    [SerializeField] private float blendDamp = 0.025f;

    [Header("Ground Check")]
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private LayerMask theWallLayerMask;
    [SerializeField] private float groundCheckRadius = 0.1f;
    [SerializeField] private float theWallCheckRadius = 0.1f;

    [Header("Object Reference")]
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private Animator ani = null;
    [SerializeField] private Transform groundCheckPoint = null;
    [SerializeField] private Transform theWallCheckPoint = null;
    [SerializeField] private CanvasUiManager uiManager = null;

    bool reload;

    public float HP => hp;
    public float MP => mp;
    public float SP => sp;
    public bool Alive => alive;

    private Vector2 refVelocity = Vector2.zero;
    private Vector2 targetVelocity = Vector2.zero;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GameObject.FindWithTag("Character").GetComponent<Animator>();
        uiManager = GameObject.FindWithTag("UIManager").GetComponent<CanvasUiManager>();
        alive = true;
    }
    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal") * moveSpeedRunning;
        isRunning = Input.GetKey(KeyCode.LeftShift);
        isJump = Input.GetKey(KeyCode.Space);
        isCrouch = Input.GetKey(KeyCode.C);
        isWalk = horizontal != 0;
        isIdel = !isWalk && !theWall;
        if (Input.GetKeyDown(KeyCode.LeftShift) && !onJump && !theWall && !isIdel)
        {
            uiManager.Reduce(uiManager.SPSlider, uiManager.SPPoint, 5f);
            spRegenerate = false;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && !spRegenerate)
        {
            reload = true;
        }
        if (isRunning && !onJump && !theWall && !isIdel)
            uiManager.Reduce(uiManager.SPSlider, uiManager.SPPoint, 0.05f);
        isKick = Input.GetKey(KeyCode.F);

        if (reload)
        {
            reload = false;
            StartCoroutine(WaitRegenerate());
        }


    }
    IEnumerator WaitRegenerate()
    {
        spRegenerate = true;
        countDown = 0;
        while (countDown < startRegenerate)
        {
            if (isRunning || !spRegenerate)
                break;
            yield return new WaitForSeconds(1f);
            countDown++;
        }
        while (uiManager.SPSlider.value < uiManager.SPSlider.maxValue && !isRunning)
        {
            if (isRunning || !spRegenerate)
                break;
            yield return new WaitForSeconds(1f);
            uiManager.Regenerate(uiManager.SPSlider, uiManager.SPPoint, 5f);
        }


    }
    private void FixedUpdate()
    {
        if (!alive)
            return;

        theWall = Physics2D.OverlapCircle(theWallCheckPoint.position, theWallCheckRadius, theWallLayerMask) != null;
        isEnemy = Physics2D.OverlapCircle(theWallCheckPoint.position, theWallCheckRadius, theWallLayerMask) != null;
        onGround = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayerMask) != null;

        rb.AddForce(Vector2.down * gravity);
        if (onGround && !theWall)
        {
            Run();
        }
        else if (!onGround)
        {

        }
        Move();
        Jump();
        SetAnimation();
    }
    private void SetAnimation()
    {
        if (horizontal == 0)
        {
            ani.SetFloat("Speed", 0);
        }
        ani.SetBool("IsJump", onJump);
        ani.SetBool("Crouch", isCrouch);
        ani.SetBool("Kick", isKick);

    }
    private void SetAnimationMovement(float speed)
    {
        ani.SetFloat("Speed", speed, blendDamp, Time.deltaTime);
    }

    private void Move()
    {
        SetAnimationMovement(MathF.Abs(horizontal));
        if (isWalk)
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, horizontal < 0 ? 180 : 0, transform.eulerAngles.z);
        if (theWall || isEnemy)
        {
            horizontal = 0;
        }
        targetVelocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref refVelocity, smoothTime);
    }
    private void Run()
    {
        moveSpeedRunning = Input.GetKey(KeyCode.LeftShift) && uiManager.SPSlider.value != 0f ? 2.5f : 1f;
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
        if (collision.collider.CompareTag("Ground") || collision.gameObject.layer == 10)
        {
            onJump = false;
        }
        //if (collision.collider.CompareTag("TheWall"))
        //{
        //    theWall = true;
        //}
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //if (collision.collider.CompareTag("TheWall"))
        //{
        //    theWall = false;
        //}
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = onGround == true ? Color.blue : Color.red;
        Gizmos.DrawWireSphere(groundCheckPoint.position, groundCheckRadius);
        //Gizmos.DrawLine(groundCheckPoint.position - new Vector3(0.5f, 0, 0), groundCheckPoint.position + new Vector3(0.5f, 0, 0));
        Gizmos.color = theWall == true ? Color.blue : Color.red;
        Gizmos.DrawWireSphere(theWallCheckPoint.position, theWallCheckRadius);
    }
}
