using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMono<T> : MonoBehaviour where T: SingletonMono<T>
{
    private static T instance;
    public static T Instance { get { return instance; } }

    protected virtual void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        instance = (T)this;
        DontDestroyOnLoad(instance);
    }

    public bool IsInitialized { get { return instance != null; } }

    public virtual void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }

}
