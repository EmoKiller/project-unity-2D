using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;

public class PlatfromControllerCuteGirl : MonoBehaviour
{
    public float jumbForce = 450f;
    public float moveSpeed = 5f;

    public float currentVelocity = 3f;
    public float maxVelocity = 3f;
    public float mutiVelocity = 1.5f;

    public Rigidbody2D rb = null;
    public Animator ani;
    

    private float horizontal = 0f;
    //private float vertical = 0f;
    private bool horizontalDown = false;
    private float eulerAngleY = 0f;

    private bool onGround = false;
    private bool movingDetec => horizontal != 0;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GameObject.FindWithTag("Player").GetComponent<Animator>();
    }

    void Update()
    {
        //vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        Vector3 movementPlatform = new Vector3(horizontal, 0);
        

        RotationPlayer();

        if (!movingDetec)
            Idle();
        else
            Walk(movementPlatform);
        Run();
        Jump();
        Attack();
    }
    private void FixedUpdate()
    {
       
    }
    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            ani.SetBool("attack", true);
        }
        else if((Input.GetKeyUp(KeyCode.J)))
        {
            ani.SetBool("attack", false);
        }

    }
    private void RotationPlayer()
    {
        horizontalDown = horizontal != 0f;
        eulerAngleY = horizontal < 0 ? 180 : 0;
        if (horizontalDown)
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, eulerAngleY, transform.eulerAngles.z);
    }
    private void Idle()
    {
        ani.SetBool("isRun", false);
    }
    private void Walk(Vector3 movementPlatform)
    {
        if (rb.velocity.x <= currentVelocity && rb.velocity.x  >= -currentVelocity && movingDetec)
        {
            rb.AddForce(movementPlatform * moveSpeed * Time.deltaTime, ForceMode2D.Impulse);
            ani.SetBool("isRun",true);

        }
        
    }
    
    private void Run()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            currentVelocity *= mutiVelocity;
            //ani.SetBool("IsRun",true);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            currentVelocity = maxVelocity;
            //ani.SetBool("IsRun", false);
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
            ani.SetBool("isJump",true);
            Debug.Log("Jump");
        }
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            rb.velocity = new Vector2(0.2f,0);
            onGround = true;
            Debug.Log("OnGround");
            ani.SetBool("isJump", false);

        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            onGround = false;
            Debug.Log("no OnGround");
        }
    }
}
