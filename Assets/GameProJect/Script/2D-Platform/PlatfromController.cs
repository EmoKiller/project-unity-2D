using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class PlatfromController : MonoBehaviour
{
    public float jumbForce = 450f;
    public float moveSpeed = 20f;

    public float currentVelocity = 3f;
    public float maxVelocity = 1.5f;
    public float mutiVelocity = 1.5f;

    public Rigidbody2D rb = null;
    public Animator ani;
    //public Transform character;
    

    public float horizontal = 0f;
    private bool horizontalDown = false;
    private float eulerAngleY = 0f;
    private Vector3 movementPlatform;

    private bool onGround = false;
    private bool isMoving => horizontal != 0;

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


        Jump();
        Run();
        Attack();
    }
    private void FixedUpdate()
    {
        Walk();
    }
    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            
        }
        else if((Input.GetKeyUp(KeyCode.J)))
        {
            
        }

    }
    private void RotationPlayer()
    {
        horizontalDown = horizontal != 0f;
        eulerAngleY = horizontal < 0 ? 180 : 0;
        if (horizontalDown)
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, eulerAngleY, transform.eulerAngles.z);
    }
    
    private void Walk()
    {
        if (rb.velocity.x <= currentVelocity && rb.velocity.x  >= -currentVelocity)
        {
            rb.AddForce(movementPlatform * moveSpeed * Time.deltaTime, ForceMode2D.Impulse);
            

        }
        ani.SetFloat("horizontalMove", horizontal);
    }
    
    private void Run()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            currentVelocity *= mutiVelocity;
            ani.SetBool("isRunning",true);
            
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            currentVelocity = maxVelocity;
            ani.SetBool("isRunning", false);
        }

    }
    private void SlowDown()
    {
        currentVelocity = 1.5f;
        Debug.Log("Slow Down");
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            rb.AddForce(transform.up * jumbForce, ForceMode2D.Force);
            ani.SetBool("isJump", true);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground") || collision.gameObject.layer == 6)
        {
            rb.velocity = new Vector2(0.2f,0);
            onGround = true;
            Debug.Log("OnGround");


        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground") || collision.gameObject.layer == 6)
        {
            onGround = false;
            ani.SetBool("isJump", false);
            Debug.Log("is jump");
        }
    }
}
