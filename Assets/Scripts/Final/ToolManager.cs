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
}
