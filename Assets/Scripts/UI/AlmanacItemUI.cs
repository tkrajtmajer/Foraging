using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AlmanacItemUI : MonoBehaviour
{
    [SerializeField] private Image frame;
    [SerializeField] private Sprite frameActive;
    [SerializeField] private Sprite frameInactive;
    [SerializeField] private Image itemSprite;
    [SerializeField] private TMP_Text itemName;

    public Item itemData;
    
    public void UseItemData(Item item) {
        itemData = item;

        if (item.wasDiscovered) {
            itemSprite.sprite = item.itemFoundSprite;
            itemName.text = item.itemName;
        }
        else {
            itemSprite.sprite = item.itemOccludedSprite;
        }
    }

    public void ToggleActive(bool isActive) {
        if (isActive) {
            itemName.color = new Color(1f, 0f, 0f, 1f);
            frame.sprite = frameActive;
        }
        else {
            itemName.color = new Color(1f, 1f, 1f, 1f);
            frame.sprite = frameInactive;
        }
    }
}
