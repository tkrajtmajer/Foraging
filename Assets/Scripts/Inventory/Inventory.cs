using NUnit.Framework;
using System;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Inventory", menuName = "Scriptable Objects/Inventory")]
public class Inventory : MonoBehaviour
{
    [SerializeField] int inventorySize = 5;
    [SerializeField] public List<ItemInventoryData> inventory = new();

    [Serializable]
    public class ItemInventoryData
    {
        public ForageableInteractable interactable = null;
        public bool empty = true;
    }

    private static ItemInventoryData emptyInventorySlot = new ItemInventoryData();

    private void Awake()
    {
        for (int i = 0; i < inventorySize; i++)
        {
            inventory.Add(emptyInventorySlot);
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
            Debug.Log("Item added to slot " + slot);
        }
        else
        {
            Debug.Log("Inventory is full");
        }
    }

    public void TryRemoveObject(int slot)
    {
        if (!inventory[slot].empty)
        {
            DropObject(inventory[slot].interactable);
            inventory[slot] = emptyInventorySlot;
            Debug.Log("Item removed");
        }
        else
        {
            Debug.Log("Empty inventory slot! Cannot drop");
        }
    }

    public void DropObject(ForageableInteractable obj)
    {
        Debug.Log("Item dropped");
    }
}
