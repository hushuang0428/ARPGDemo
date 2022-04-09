using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{

    Dictionary<string, Queue<GameObject>> objectPool = new Dictionary<string, Queue<GameObject>>();

    private GameObject pool;

    public GameObject GetObject(BaseSpawner spawner,string path="ObjectPool")
    {
        
        GameObject obj;

        

        if (!objectPool.ContainsKey(spawner.name) || objectPool[spawner.name].Count == 0)
        {
            obj = spawner.Spawner();
            
            PushObject(obj);
            if (pool == null) pool = new GameObject(path);
            //GameObject.DontDestroyOnLoad(pool);
            GameObject childPool = GameObject.Find(spawner.name + "Pool");
            if (!childPool)
            {
                childPool = new GameObject(spawner.name + "Pool");
                childPool.transform.SetParent(pool.transform);
            }
            obj.transform.SetParent(childPool.transform);
        }
        
        obj = objectPool[spawner.name].Dequeue();
        
        obj.SetActive(true);

        
        return obj;

    }
    public void PushObject(GameObject prefab)
    {
        
        string _name = prefab.name.Replace("(Clone)", string.Empty);
        
        if (!objectPool.ContainsKey(_name))
        {
            objectPool.Add(_name, new Queue<GameObject>());
        }
        objectPool[_name].Enqueue(prefab);

        prefab.SetActive(false);

    }

}
