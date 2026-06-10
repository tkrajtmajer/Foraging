using NUnit.Framework;
using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] int inventorySize = 5;
    [SerializeField] public List<ItemInventoryData> inventory = new();
    [SerializeField] Vector3 displacement;
    [SerializeField] private GameObject inventoryContainer;
    [SerializeField] private Sprite frameActive;
    [SerializeField] private Sprite frameInactive;
    private List<GameObject> inventorySlotsUI = new();

    private GameObject player;

    public int currentSlot = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetSlotActive(currentSlot, false);
            currentSlot = 0;
            SetSlotActive(currentSlot, true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetSlotActive(currentSlot, false);
            currentSlot = 1;
            SetSlotActive(currentSlot, true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetSlotActive(currentSlot, false);
            currentSlot = 2;
            SetSlotActive(currentSlot, true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetSlotActive(currentSlot, false);
            currentSlot = 3;
            SetSlotActive(currentSlot, true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SetSlotActive(currentSlot, false);
            currentSlot = 4;
            SetSlotActive(currentSlot, true);
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            TryRemoveObject(currentSlot);
        }
    }

    [Serializable]
    public class ItemInventoryData
    {
        public ItemInventoryData(ForageableData itemData, bool emptySlot) { data = itemData; empty = emptySlot; }

        public ForageableData data;
        public bool empty;
    }

    public static Inventory Instance;

    private void Awake()
    { 
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        for (int i = 0; i < inventorySize; i++)
        {
            ItemInventoryData empty = new ItemInventoryData(null, true);
            inventory.Add(empty);
        }

        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        Transform child = inventoryContainer.transform;
        for (int i = 0; i < inventorySize; i++)
        {
            inventorySlotsUI.Add(child.GetChild(i).GetChild(0).gameObject);
            inventorySlotsUI[i].GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        }
    }

    public int GetFirstEmptySlot()
    {
        for (int i = 0; i < inventorySize; ++i)
        {
            if (inventory[i].empty)
            {
                return i;
            }
        }

        return -1;
    }

    public void TryAddObject(ForageableInteractable obj)
    {
        int slot = GetFirstEmptySlot();
        if (slot != -1)
        {
            inventory[slot].data = obj.Data;
            inventory[slot].empty = false;
            SetSlotActive(currentSlot, false);
            currentSlot = slot;
            SetSlotActive(currentSlot, true);
            UpdateUISlot(slot);
        }
    }

    public void TryRemoveObject(int slot)
    {
        if (!inventory[slot].empty)
        {
            DropObject(inventory[slot].data);
            inventory[slot].data = null;
            inventory[slot].empty = true;
            UpdateUISlot(slot, true);
        }
    }

    public void DropObject(ForageableData data)
    {
        GameObject newObj = Instantiate(data.modelPrefab);
        newObj.transform.position = player.transform.position + player.transform.forward;
        newObj.transform.position += 0.2f * displacement;
    }

    private void UpdateUISlot(int slot, bool remove=false)
    {
        if (remove)
        {
            inventorySlotsUI[slot].GetComponent<Image>().sprite = null;
            inventorySlotsUI[slot].GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            return;
        }

        inventorySlotsUI[slot].GetComponent<Image>().sprite = inventory[slot].data.silhouetteImage;
        inventorySlotsUI[slot].GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

    void SetSlotActive(int slot, bool isActive) {
        Image borderImage = inventorySlotsUI[slot].transform.parent.GetComponent<Image>();

        borderImage.sprite = isActive ? frameActive : frameInactive;
    }
}
