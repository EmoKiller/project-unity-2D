using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerG : MonoBehaviour
{
    public Vector2 inputMoveASDW;
    public Vector3 mousePosition;
    public float speed = 15f;
    public GameObject Gun;
    public Transform target;

    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        inputMoveASDW = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        mousePosition = Input.mousePosition;
        //Vector2 from = Gun.transform.position - mousePosition;
        //Vector3 from = Gun.transform.position - target.transform.position;
        
        Walk();

    }
    private void Walk()
    {
        rb.AddForce(inputMoveASDW * speed * Time.deltaTime, ForceMode2D.Impulse);
    }
}
