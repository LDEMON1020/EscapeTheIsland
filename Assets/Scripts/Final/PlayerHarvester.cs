using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Block;

public class PlayerHarvester : MonoBehaviour
{
    public float rayDistance = 5f;
    public LayerMask hitMask = ~0;
    public int toolDamage = 1;
    public float hitCooldown = 0.15f;

    private float _nextHitTime;
    private Camera _cam;
    public Inventory inventory;
    InventoryUI inventoryUI;
    public GameObject selectedBlock;
    public ItemType equippedTool;


    void Awake()
    {
        _cam = Camera.main;
        if (inventory == null) inventory = gameObject.AddComponent<Inventory>();
        inventoryUI = FindObjectOfType<InventoryUI>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isToolSelected =
    inventoryUI.selectedIndex >= 0 &&
    (inventoryUI.GetInventorySlot() == ItemType.Stone_Axe ||
     inventoryUI.GetInventorySlot() == ItemType.Stone_Shovel);

        if (inventoryUI.selectedIndex < 0 || isToolSelected)
        {
            // 채굴
            if (Input.GetMouseButton(0) && Time.time >= _nextHitTime)
            {
                _nextHitTime = Time.time + hitCooldown;

                Ray ray = _cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                if (Physics.Raycast(ray, out var hit, rayDistance, hitMask))
                {
                    var block = hit.collider.GetComponent<Block>();
                    if (block != null)
                    {
                        ItemType currentTool = ItemType.Stick;

                        if (isToolSelected)
                            currentTool = inventoryUI.GetInventorySlot();

                        int damage = ToolDamageTable.GetDamage(currentTool, block);

                        Debug.Log($"[HARVEST] Tool={currentTool}, Block={block.type}, Damage={damage}");

                        block.Hit(damage, inventory);
                    }
                }
            }
        }
        else
        {
            // 🔨 설치 모드 (블록 선택 중)
            Ray ray = _cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            if (Physics.Raycast(ray, out var hit, rayDistance, hitMask))
            {
                Vector3Int placePos = AdjacentCellOnHitFace(hit);

                selectedBlock.transform.localScale = Vector3.one;
                selectedBlock.transform.position = placePos;
                selectedBlock.transform.rotation = Quaternion.identity;

                if (Input.GetMouseButtonDown(0))
                {
                    ItemType selected = inventoryUI.GetInventorySlot();

                    if (inventory.Consume(selected, 1))
                    {
                        FindObjectOfType<NoiseVoxelMap>().PlaceTile(placePos, selected);
                    }
                }
            }
        }
    }

    static Vector3Int AdjacentCellOnHitFace(in RaycastHit hit)
    {
        Vector3 baseCenter = hit.collider.transform.position;
        Vector3 adjCenter = baseCenter + hit.normal;
        return Vector3Int.RoundToInt(adjCenter);
    }
}
