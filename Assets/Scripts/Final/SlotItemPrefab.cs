using UnityEngine;
using UnityEngine.UI;

public class SlotItemPrefab : MonoBehaviour
{
    public Image itemImage;
    public Text itemText;
    public BlockType blockType;
    // Start is called before the first frame update

    public void ItemSetting(Sprite itemSprite, string txt, BlockType type)
    {
        itemImage.sprite = itemSprite;
        itemText.text = txt;
        blockType = type;
    }
}
