using System.Collections.Generic;
using UnityEngine;
public enum ItemLocation
{
    Forest,
    RiverBank,
    Beach,
    Field,
    Woodland
}
public enum ItemSeason
{
    spring
}

// create foreagable data in assets menu by Right Click -> Create > Foraging > New Forageable Data
[CreateAssetMenu(fileName = "New Foragable Data", menuName = "Foraging/New Forageable Data")]    
public class ForageableData : ScriptableObject
{
    [Header("Item Info")]
    public string itemName;
    public List<string> description;
    public List<string> extraInfo;

    [Header("Stats")]
    //public int itemDifficulty;
    public List<ItemLocation> locations;
    public bool isPoisonous;
    public ItemSeason season;

    [Header("Visuals")]
    public Sprite silhouetteImage;
    //public Sprite silhouetteImageOccluded;
    public GameObject modelPrefab; 

    [Header("Internal")]
    public bool wasDiscovered = false; //doesn't have to be public
}
