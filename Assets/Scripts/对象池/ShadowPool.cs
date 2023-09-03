using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowPool : MonoBehaviour
{
    public static ShadowPool instance;//单例 控制对象池 可以在别的地方直接访问单例内所以东西 使用方法看shadow

    public GameObject shadowPrefab;

    public int shadowCount;

    private Queue<GameObject> availableObjects = new Queue<GameObject>();//队列创建方法，记下来就好了 availableObjects是名字

    void Awake()
    {
        instance = this;//单例
        //初始化对象池   
        FillPool();
    }

    public void FillPool()//填满对象池
    {
        for (int i = 0; i < shadowCount; i++)
        {
            var newShadow = Instantiate(shadowPrefab);
            //var可以用来申明临时变量
            //Instantiate可以用来输出预制体
            newShadow.transform.SetParent(transform);

            //返回对象池
            ReturnPool(newShadow);
        }
    }

    //状态变为FALSE然后放入队列（返回对象池）
    public void ReturnPool(GameObject goj)
    {
        goj.SetActive(false);

        availableObjects.Enqueue(goj);
    }

    public GameObject GetFormPool()//这里的返回值是gameobject
    {
        if (availableObjects.Count == 0)//如果队列里的对象不够，那么再次填充
        {
            FillPool();
        }
        var outShadow = availableObjects.Dequeue();
        outShadow.SetActive(true);
        return outShadow;
    }
}
