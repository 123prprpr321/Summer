using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawner : MonoBehaviour    //获得战利品，随机掉落
{
    [System.Serializable]
    public class LootItem
    {
        public GameObject item;
        [Range(0, 1)]      //限定掉落率
        public float weight;
    }

    public LootItem[] lootItems;

    public void Spwanloot() //死亡调用
    {
        float currentValue = Random.value;

        for (int i = 0; i < lootItems.Length; i++)
        {
            if (currentValue <= lootItems[i].weight)
            {
                GameObject obj = ObjectPool.Instance.GetObject(lootItems[i].item);
                obj.transform.position = transform.position + Vector3.up * 2;
                //break;  //只掉落一个物品
            }
        }
    }
}
