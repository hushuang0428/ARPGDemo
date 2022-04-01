using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;


/// <summary>
/// 
/// 实时存档的实现 暂定为定时存档 关键操作存档 退出存档
/// 
/// *** 需处理细节
/// 先存副本 再覆盖 覆盖时发生中断则启用副本
/// 

/// </summary>


public class JsonMgr :Singleton<JsonMgr>
{
    
 
    ///  /// <summary>
    /// 序列化为Json存储
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="fileName"></param>
    /// <param name="data">需序列化的数据</param>
    public void SaveByJson<T>(string  filePath,string fileName,T data)
    {

        
        var path = Path.Combine(Application.dataPath,filePath, fileName);
        
        
        var json = JsonUtility.ToJson(data,true);
       
        try
        {
            File.WriteAllText(path,json);
        
#if UNITY_EDITOR
            Debug.Log($"Susscessfully saved to {path}");
#endif
        }
        catch(System.Exception e)   
        {
#if UNITY_EDITOR
            Debug.LogError($"Failed saved to {path}");
#endif
        }
        
    }

    /// <summary>
    /// 将Json反序列化
    /// </summary>
    /// <typeparam name="T">返回类型</typeparam>
    /// <param name="filePath"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public T LoadJson<T>(string filePath, string fileName)
    {

        var path = Path.Combine(Application.dataPath, filePath,fileName);
        try
        {
            var json = File.ReadAllText(path);

            T tmp = JsonUtility.FromJson<T>(json);

#if UNITY_EDITOR

            Debug.Log($"Susscessfully loaded to {path}");
#endif
            return tmp ;
        }
        catch (System.Exception e)
        {
#if UNITY_EDITOR
            Debug.LogError($"Failed loaded to {path}");
#endif
            return default;
        }
    }

    /// <summary>
    /// 删除存档
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="fileName"></param>
    public void delete(string filePath, string fileName)
    {
        var path = Path.Combine(Application.dataPath, fileName);

        try
        {
            File.Delete(path);
#if UNITY_EDITOR
            Debug.Log($"Susscessfully deleted to {path}");
#endif
        }
        catch (System.Exception e)
        {
#if UNITY_EDITOR
            Debug.LogError($"Failed deleted to {path}");
#endif
        }
    }
}



/// <summary>
/// 针对list无法被序列化
/// </summary>
/// <typeparam name="T"></typeparam>


[Serializable]
public class Serialization<T>
{
    [SerializeField] List<T> target;
    public List<T> ToList() { return target; }

    public Serialization(List<T> target){
        this.target = target;
    }
}