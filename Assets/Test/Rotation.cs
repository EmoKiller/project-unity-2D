using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float speed;
    public Transform object1;
    public Vector3 object1vec;
    public Transform object2;
    public Vector3 object2vec;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        object1vec = object1.transform.position;
        object2vec = object2.transform.position;
        //transform.rotation = Quaternion.LookRotation(Vector3.forward, object2vec);
        Quaternion lookObject1 = Quaternion.LookRotation(Vector3.forward, object1vec);
        Quaternion lookObject2 = Quaternion.LookRotation(Vector3.forward, object2vec);
    }
}
