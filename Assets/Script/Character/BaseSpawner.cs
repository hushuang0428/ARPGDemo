using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 所有生成器应存放于GM中
/// 生成器中读取初始化配置数据,这部分数据由策划配置
/// 之后生成实例的数据应额外存贮
/// </summary>

public abstract class BaseSpawner 
{
    public string name;
    

    public abstract GameObject Spawner();
}
