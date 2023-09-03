using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>   //�̳з�ʽ���������ֱ��ʹ�õ���
{
    public List<EndGameObserve> endGameObserves = new List<EndGameObserve>();      //���������б�


    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    public void AddObserver(EndGameObserve observer)    //���˼����б�
    {
        endGameObserves.Add(observer);
    }

    public void RemoveObserver(EndGameObserve observe)
    {
        endGameObserves.Remove(observe);
    }

    public void NotifyObservers()
    {
        foreach (var obserber in endGameObserves)   //ÿһ�����˹㲥
        {
            obserber.EndNotify();
        }
    }

}
