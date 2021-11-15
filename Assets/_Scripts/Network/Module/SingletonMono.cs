using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T>
{
    public static T instance { get; private set; }
    protected virtual void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Debug.Log(this);
        DontDestroyOnLoad(gameObject);
        instance = (T)this;
    }
}