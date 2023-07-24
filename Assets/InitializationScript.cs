using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializationScript : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        Debug.Log("Awake");
    }
    private void OnEnable()
    {
        Debug.Log("OnEnable");
    }
    void Start()
    {
        Debug.Log("Start");
    }
    private void OnDisable()
    {
        Debug.Log("OnDisable");

    }
    private void OnDestroy()
    {
        Debug.Log("OnDestroy");
    }
}
