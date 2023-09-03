using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>    //T�����κ����ͣ�Ҳ��������Manager
{
    private static T instance;

    public static T Instance    //��������
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

    public static bool IsInitialized   //��������
    {
        get { return instance != null; }  //�ж�instance�Ƿ�Ϊ��
    }

    protected virtual void OnDestroy()  //���ٵ�������
    {
        if(instance == this)
        {
            instance = null;
        }
    }
}
