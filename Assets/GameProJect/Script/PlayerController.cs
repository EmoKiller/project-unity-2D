using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    
    public GameObject gun;
    public GameObject bullet;
    public float Speed;

    private float horizontal;
    private float vertical;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * Input.GetAxis("Horizontal") * Speed * Time.deltaTime + Vector3.up * Input.GetAxis("Vertical") * Speed * Time.deltaTime;

        //MovementPlayer();

        if (Input.GetKeyDown(KeyCode.J) || Input.GetButtonDown("Fire1"))
        {
            Instantiate(bullet, gun.transform.position, gun.transform.rotation);
        }

    }
    
    private void MovementPlayer()
    {
        InputMove();
        transform.Translate(Vector2.up * Time.deltaTime * Speed * vertical);
        transform.Translate(Vector2.right * Time.deltaTime * Speed * horizontal);
    }
    private void InputMove()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(name+ " OnTrigger whit " + collision.name);
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log( name + " collision whit " + collision.gameObject.name);
        
    }

}
