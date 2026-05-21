using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Scriptable Objects/ItemDatabase")]
public class ItemDatabase : ScriptableObject
{
    public List<GameObject> allItemPrefabs = new List<GameObject>();

    //// helper function to get a prefab by the data it holds, 
    //public GameObject GetPrefabByData(ForageableData dataToFind)
    //{
    //    foreach (GameObject prefab in allItemPrefabs)
    //    {
    //        var interactable = prefab.GetComponent<ForageableInteractable>();
    //        if (interactable != null && interactable.Data == dataToFind)
    //        {
    //            return prefab;
    //        }
    //    }
    //    return null; // Not found
    //}
}
