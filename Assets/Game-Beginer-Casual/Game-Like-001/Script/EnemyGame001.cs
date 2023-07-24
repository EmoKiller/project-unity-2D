using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGame001 : MonoBehaviour
{
    private float hp = 10f;
    private bool alive => hp > 0 ;

    public float HP => hp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            
        }
    }
}
