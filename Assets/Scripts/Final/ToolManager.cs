using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolManager : MonoBehaviour
{
    public static ToolManager Instance;

    // 가지고 있는 도구들
    public HashSet<ItemType> ownedTools = new HashSet<ItemType>();

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public bool HasTool(ItemType tool)
    {
        return ownedTools.Contains(tool);
    }

    public void AddTool(ItemType tool)
    {
        if (!ownedTools.Contains(tool))
            ownedTools.Add(tool);
    }

    public bool IsUpgrade(ItemType oldTool, ItemType newTool)
    {
        switch (newTool)
        {
            case ItemType.Iron_Pickaxe:
                return oldTool == ItemType.Stone_Pickaxe;

            case ItemType.Iron_Axe:
                return oldTool == ItemType.Stone_Axe;

            case ItemType.iron_Shovel:
                return oldTool == ItemType.Stone_Shovel;
        }

        return false;
    }
}
