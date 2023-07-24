using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerControllerGame001 : MonoBehaviour
{
    
    private Rigidbody2D rb;
    private float vertical;
    private float horizontal;

    public GameObject bullet;
    public float DamageOfBullet;


    public Transform offsetBullet;
    public float speedMove;
    public Vector2 inputMove;
    private float DamageTotal;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        inputMove = new Vector2(horizontal, vertical);
        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bullet, offsetBullet.position, transform.rotation);
        }
    }
    private void FixedUpdate()
    {
        //rb.AddForce(inputMove * speedMove, ForceMode2D.Force);
    }
    private void LateUpdate()
    {
        
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.collider.name);

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }
}
