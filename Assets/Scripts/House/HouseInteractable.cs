using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using UnityEngine;

public class HouseInteractable : MonoBehaviour, IInteractable
{

    public List<GameObject> debugInventory = new List<GameObject>();
    public void Interact()
    {
        Debug.Log($"Score: {GameManager.GetRecipeScore(debugInventory)}!");
    }
}
