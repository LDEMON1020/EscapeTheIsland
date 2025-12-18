using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Dictionary<ItemType, int> items = new();
    InventoryUI inventoryUI;
    // Start is called before the first frame update
    void Start()
    {
        inventoryUI = FindObjectOfType<InventoryUI>();

        // 🔧 ToolManager에 저장된 도구 복원
        if (ToolManager.Instance != null)
        {
            foreach (var tool in ToolManager.Instance.ownedTools)
            {
                Add(tool, 1);
            }
        }
    }
    public int GetCount(ItemType id)
    {
        items.TryGetValue(id, out var count);
        return count;
    }
    public void Add(ItemType type, int count = 1)
    {
        if (!items.ContainsKey(type)) items[type] = 0;
    items[type] += count;

    // 🔧 도구면 ToolManager에 저장
    if (IsTool(type))
    {
        ToolManager.Instance.AddTool(type);
    }

    inventoryUI.UpdateInventory(this);
    }

    public bool Consume(ItemType type, int count = 1)
    {
        if (!items.TryGetValue(type, out var have) || have < count) return false;
        items[type] = have - count;
        Debug.Log($"[Inventory] -{count} {type} (총 {items[type]})");
        if (items[type] == 0)
        {
            items.Remove(type);
            inventoryUI.selectedIndex = -1;
            inventoryUI.ResetSelection();
        }
        
        inventoryUI.UpdateInventory(this);
        return true;
    }

    bool IsTool(ItemType type)
    {
        return type == ItemType.Stone_Axe
            || type == ItemType.Stone_Shovel
            || type == ItemType.Stone_Pickaxe
            || type == ItemType.Iron_Axe
            || type == ItemType.Iron_Pickaxe
            || type == ItemType.iron_Shovel;
    }
}
