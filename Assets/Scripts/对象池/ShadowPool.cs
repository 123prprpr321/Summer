using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowPool : MonoBehaviour
{
    public static ShadowPool instance;//���� ���ƶ���� �����ڱ�ĵط�ֱ�ӷ��ʵ��������Զ��� ʹ�÷�����shadow

    public GameObject shadowPrefab;

    public int shadowCount;

    private Queue<GameObject> availableObjects = new Queue<GameObject>();//���д����������������ͺ��� availableObjects������

    void Awake()
    {
        instance = this;//����
        //��ʼ�������   
        FillPool();
    }

    public void FillPool()//���������
    {
        for (int i = 0; i < shadowCount; i++)
        {
            var newShadow = Instantiate(shadowPrefab);
            //var��������������ʱ����
            //Instantiate�����������Ԥ����
            newShadow.transform.SetParent(transform);

            //���ض����
            ReturnPool(newShadow);
        }
    }

    //״̬��ΪFALSEȻ�������У����ض���أ�
    public void ReturnPool(GameObject goj)
    {
        goj.SetActive(false);

        availableObjects.Enqueue(goj);
    }

    public GameObject GetFormPool()//����ķ���ֵ��gameobject
    {
        if (availableObjects.Count == 0)//���������Ķ��󲻹�����ô�ٴ����
        {
            FillPool();
        }
        var outShadow = availableObjects.Dequeue();
        outShadow.SetActive(true);
        return outShadow;
    }
}
