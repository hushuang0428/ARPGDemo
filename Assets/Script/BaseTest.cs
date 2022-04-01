using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTest : MonoBehaviour
{
    protected virtual void Awake()
    {
        Debug.Log("Base Awake");
    }
    protected virtual void Start()  
    {
        Debug.Log("Base Start");
    }
    protected virtual void OnEnable()
    {
        Debug.Log("Base OnEnable");
    }
}
