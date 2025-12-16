using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Sprite GrassSprite;
    public Sprite DirtSprite;
    public Sprite WaterSprite;
    public Sprite StoneSprite;
    public Sprite Stone_AxeSprite;
    public Sprite Stone_ShovelSprite;
    public Sprite IronStoneSprite;
    public Sprite StickSprite;

    public List<Transform> Slot = new List<Transform>();
    public GameObject Slotitem;
    List<GameObject> items = new List<GameObject>();

    public int selectedIndex = -1;

    private void Update()
    {
        for(int i = 0; i < Mathf.Min(9, Slot.Count); i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SetSelectIndex(i);
            }
        }
    }

    public void SetSelectIndex(int idx)
    {
        ResetSelection();
        if(selectedIndex == idx)
        {
            selectedIndex = -1;
        }
        else
        {
            if(idx >= items.Count)
            {
                selectedIndex = -1;
            }
            else
            {
                SetSelection(idx);
                selectedIndex = idx;
            }
        }
    }

    public void OnSlotSelected(ItemType item)
    {
        if (item == ItemType.Stone_Axe || item == ItemType.Stone_Shovel)
        {
            FindObjectOfType<PlayerHarvester>().equippedTool = item;
        }
    }

    public void ResetSelection()
    {
        foreach(var slot in Slot)
        {
            slot.GetComponent<Image>().color = Color.white;
        }
    }

    void SetSelection(int _idx)
    {
        Slot[_idx].GetComponent<Image>().color = Color.yellow;
    }

    public ItemType GetInventorySlot()
    {
        return items[selectedIndex].GetComponent<SlotItemPrefab>().blockType;
    }

    public void UpdateInventory(Inventory myInven)
    {
        // 기존 슬롯 정리
        foreach (var Slotitems in items)
        {
            Destroy(Slotitems);
        }

        items.Clear();

        int idx = 0;

        foreach (var item in myInven.items)
        {
            if (idx >= Slot.Count)
            {
                Debug.LogWarning("슬롯 개수가 부족합니다!");
                break;
            }

            // 슬롯 아이템 생성
            var go = Instantiate(Slotitem, Slot[idx], transform);
            go.transform.localPosition = Vector3.zero;
            SlotItemPrefab sitem = go.GetComponent<SlotItemPrefab>();
            items.Add(go);

            switch (item.Key)
            {
                case ItemType.Dirt:
                    sitem.ItemSetting(DirtSprite, "x" + item.Value.ToString(), item.Key);
                    break;

                case ItemType.Grass:
                    sitem.ItemSetting(GrassSprite, "x" + item.Value.ToString(), item.Key);
                    break;

                case ItemType.Water:
                    sitem.ItemSetting(WaterSprite, "x" + item.Value.ToString(), item.Key);
                    break;
                case ItemType.Stone:
                    sitem.ItemSetting(StoneSprite, "x" + item.Value.ToString(), item.Key);
                    break;
                case ItemType.Stone_Axe:
                    sitem.ItemSetting(Stone_AxeSprite, "x" + item.Value.ToString(), item.Key);
                    break;
                case ItemType.Stone_Shovel:
                    sitem.ItemSetting(Stone_ShovelSprite, "x" + item.Value.ToString(), item.Key);
                    break;
                case ItemType.Iron:
                    sitem.ItemSetting(IronStoneSprite, "x" + item.Value.ToString(), item.Key);
                    break;
                         case ItemType.Stick:
                    sitem.ItemSetting(StickSprite, "x" + item.Value.ToString(), item.Key);
                    break;
            }
            idx++;
        }
    }
}