using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;

public class HouseInteractable : MonoBehaviour, IInteractable
{

    public List<GameObject> debugInventory = new List<GameObject>();

    public static event Action<int> OnHouseInteracted;

    public void Interact()
    {
        OnHouseInteracted?.Invoke(GameManager.GetRecipeScore(debugInventory));
        Debug.Log($"Score: {GameManager.GetRecipeScore(debugInventory)}!");
    }
}
