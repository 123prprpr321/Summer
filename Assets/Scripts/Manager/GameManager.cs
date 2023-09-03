using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>   //继承范式单例后可以直接使用单例
{
    public List<EndGameObserve> endGameObserves = new List<EndGameObserve>();      //创建敌人列表


    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    public void AddObserver(EndGameObserve observer)    //敌人加入列表
    {
        endGameObserves.Add(observer);
    }

    public void RemoveObserver(EndGameObserve observe)
    {
        endGameObserves.Remove(observe);
    }

    public void NotifyObservers()
    {
        foreach (var obserber in endGameObserves)   //每一个敌人广播
        {
            obserber.EndNotify();
        }
    }

}
