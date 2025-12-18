using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Dirt, Grass, Water, Stone, Stone_Axe, Stone_Shovel, Iron, Stick, Iron_Axe, iron_Shovel, Stone_Pickaxe, Iron_Pickaxe, Titanium}
public class Block : MonoBehaviour
{ 

    [Header("Block Stat")]
    public ItemType type = ItemType.Dirt;
    public int maxHP = 3;
    [HideInInspector] public int hp;

    public int dropCount = 1;
    public bool mineable = true;

    void Awake()
    {
        hp = maxHP;
        if (GetComponent<Collider>() == null) gameObject.AddComponent<BoxCollider>();
        if (string.IsNullOrEmpty(gameObject.tag) || gameObject.tag == "Untagged")
            gameObject.tag = "Block";
    }

    public bool IsDirtType()
    {
        return type == ItemType.Dirt || type == ItemType.Grass;
    }

    public bool IsWoodType()
    {
        // 지금은 Stick을 나무 계열로 취급
        // 나중에 ItemType.Wood 추가하면 여기에 추가
        return type == ItemType.Stick;
    }

    public bool IsStoneType()
    {
        return type == ItemType.Stone || type == ItemType.Iron || type == ItemType.Titanium;
    }

    public void Hit(int damage, Inventory inven)
    {
        if (!mineable) return;

        hp -= damage;

        if(hp<=0)
        {
            if (inven != null && dropCount > 0)
            {
                inven.Add(type, dropCount);
            }
                

            Destroy(gameObject);
        }
    }
}
