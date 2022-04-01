using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ATest :BaseTest
{
    private float xVelocity;
    private float yVelocity;

    /// <summary>
    /// 写成一个加载类
    /// </summary>
    public virtual void Start()
    {
        base.Start();

        
        JsonMgr.Instance.SaveByJson("Resources", "TestA", new A());

        A a=JsonMgr.Instance.LoadJson<A>("Resources", "TestA");
        Debug.Log(a.a);
        
    }
    private void Update()
    {
        xVelocity = Input.GetAxis("Horizontal");
        yVelocity = Input.GetAxis("Vertical");

        transform.Translate(xVelocity*0.1f, 0f, yVelocity*0.1f);
    }

}


public class A{
    public int a = 10;
}
