using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBox : MonoBehaviour
{
    private bool canOpen;
    private bool isOpen;
    private Animator anim;

    [System.Serializable]
    public class LootItem
    {
        public GameObject item;
        [Range(0, 1)]
        public float weight;
    }

    public LootItem[] lootItems;


    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (canOpen && !isOpen)
            {
                anim.SetTrigger("opening");
                isOpen = true;
                Invoke("GetObject", 1f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canOpen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canOpen = false;
        }
    }
    public void Spwanloot() //动画调用
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
