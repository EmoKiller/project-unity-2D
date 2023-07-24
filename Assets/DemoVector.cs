using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.Mathematics;
using UnityEngine;

public class DemoVector : MonoBehaviour
{
    public Transform target = null;
    public float lerpValue = 0.3f;
    public bool move = false;
    //float smootValue = 1;
    
    Vector3 outVec = Vector3.zero;
    public void Update()
    {
        if (!move)
        {
            return;
        }
        //transform.position = Vector3.SmoothDamp(transform.position,target.position,ref outVec,0.5f);
        Vector3 direction = transform.position - target.position;
        Vector3.Angle(direction, transform.up);
    }
}
