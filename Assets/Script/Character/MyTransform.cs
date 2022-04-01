using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTransform
{
    public CharactersName active;
    public Vector3 postion;
    public float Yaw;
    public MyTransform(CharactersName name ,Vector3 pos,float Yaw)
    {
        active = name;
        postion = pos;
        this.Yaw = Yaw;
    }
}
