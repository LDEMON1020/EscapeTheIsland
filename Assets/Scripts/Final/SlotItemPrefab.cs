using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotItemPrefab : MonoBehaviour, IPointerClickHandler
{
    public Image itemImage;
    public TextMeshProUGUI itemText;
    public ItemType blockType;
    public craftingPanel craftingPanel;
    // Start is called before the first frame update

    public void ItemSetting(Sprite itemSprite, string txt, ItemType type)
    {
        itemImage.sprite = itemSprite;
        itemText.text = txt;
        blockType = type;
    }

    void Awake()
    {
        if (!craftingPanel)
            craftingPanel = FindObjectOfType<craftingPanel>(true);
    }

public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Right) return;
        if (!craftingPanel) return;

        craftingPanel.AddPlanned(blockType, 1);
    }
}
