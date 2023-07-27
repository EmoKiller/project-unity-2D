using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class PlatfromController : MonoBehaviour
{
    public float jumbForce = 450f;
    public float moveSpeed = 20f;

    public float currentVelocity = 1.5f;
    public float maxVelocity = 1.5f;
    public float mutiVelocity = 3f;

    public Rigidbody2D rb = null;
    public Animator ani;
    //public Transform character;
    

    public float horizontal = 0f;
    private float eulerAngleY = 0f;
    private Vector3 movementPlatform;

    private bool isMoving => horizontal != 0f;
    private bool onGround = false;
    private bool isJump = false;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GameObject.FindWithTag("Player").GetComponent<Animator>();
    }
    void Start()
    {

    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        movementPlatform = new Vector3(horizontal, 0);
        RotationPlayer();
        
        if (onGround == true && rb.velocity.x <= currentVelocity && rb.velocity.x >= -currentVelocity)
        {
            Walk();
            ani.SetFloat("horizontalMove", horizontal);
        }
        Run();
        Jump();

        Attack();
        SetAnimationCharacter();
    }
    private void FixedUpdate()
    {
        if (!isMoving)
        {
            ani.SetFloat("horizontalMove",0);
        }
        else
        {
            ani.SetFloat("horizontalMove", 0);
        }
        
        
    }
    private void SetAnimationCharacter()
    {
        
    }
    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            ani.SetTrigger("Attack");
            
        }
        //else if ((Input.GetKeyUp(KeyCode.J)))
        //{
        //    ani.ResetTrigger("Attack");
        //}

    }
    private void RotationPlayer()
    {
        eulerAngleY = horizontal < 0 ? 180 : 0;
        if (isMoving)
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, eulerAngleY, transform.eulerAngles.z);
    }
    
    private void Walk()
    {
        rb.AddForce(movementPlatform * moveSpeed * Time.deltaTime, ForceMode2D.Impulse);
    }
    
    private void Run()
    {
        //if (Input.GetKeyDown(KeyCode.LeftShift) && isJump == false && isMoving == true)
        //{
        //    currentVelocity *= mutiVelocity;
        //    ani.SetBool("isRunning",true);
            
        //}
        //if (Input.GetKeyUp(KeyCode.LeftShift))
        //{
        //    currentVelocity = maxVelocity;
        //    ani.SetBool("isRunning", false);
        //}
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (isJump == false && isMoving == true)
            {
                currentVelocity *= mutiVelocity;
                ani.SetBool("isRunning",true);
            }
        }

    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onGround == true )
        {
            rb.AddForce(transform.up * jumbForce, ForceMode2D.Force);
            ani.SetBool("isJump", true);
            isJump = true;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground") || collision.gameObject.layer == 6)
        {
            onGround = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground") || collision.gameObject.layer == 6)
        {
            onGround = false;
        }
    }
}
