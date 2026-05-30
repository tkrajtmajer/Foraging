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
    private List<GameObject> inventorySlotsUI = new();

    private GameObject player;

    public int currentSlot;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentSlot = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentSlot = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentSlot = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentSlot = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            currentSlot = 4;
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            TryRemoveObject(currentSlot);
        }
    }

    [Serializable]
    public class ItemInventoryData
    {
        public ItemInventoryData(ForageableInteractable item, bool emptySlot) { interactable = item; empty = emptySlot; }

        public ForageableInteractable interactable;
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
        Transform child = transform.GetChild(0);
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
            inventory[slot].interactable = obj;
            inventory[slot].empty = false;
            UpdateUISlot(slot);
        }
    }

    public void TryRemoveObject(int slot)
    {
        if (!inventory[slot].empty)
        {
            DropObject(inventory[slot].interactable);
            inventory[slot].interactable = null;
            inventory[slot].empty = true;
            UpdateUISlot(slot, true);
        }
    }

    public void DropObject(ForageableInteractable obj)
    {
        GameObject newObj = Instantiate(obj.Data.modelPrefab);
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

        inventorySlotsUI[slot].GetComponent<Image>().sprite = inventory[slot].interactable.Data.silhouetteImage;
        inventorySlotsUI[slot].GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }
}
