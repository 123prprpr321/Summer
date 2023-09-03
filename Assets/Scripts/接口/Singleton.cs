using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>    //T代表任何类型，也代表所有Manager
{
    private static T instance;

    public static T Instance    //构造属性
    {
        get { return instance; }
    }
    
    protected virtual void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = (T)this;
        }
    }

    public static bool IsInitialized   //构造属性
    {
        get { return instance != null; }  //判断instance是否为空
    }

    protected virtual void OnDestroy()  //销毁单例函数
    {
        if(instance == this)
        {
            instance = null;
        }
    }
}
