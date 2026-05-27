using System;
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

    internal ForageableData itemData;

    public Action<AlmanacItemUI> ItemSelected;
    
    public void UseItemData(ForageableData data) {
        itemData = data;

        if (data.wasDiscovered) {
            itemSprite.sprite = data.silhouetteImage;
            itemName.text = data.itemName;
        }
        else {
            itemSprite.sprite = data.silhouetteImageOccluded;
        }
    }

    public void SelectItem() {
        ItemSelected?.Invoke(this);
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
