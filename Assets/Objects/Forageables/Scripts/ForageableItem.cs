using UnityEngine;
public enum ItemLocation
{
    Forest,
    RiverBank,
    Beach,
    Field,
    Woodland
}

// create foreagable data in assets menu by Right Click -> Create > Foraging > New Forageable Data
[CreateAssetMenu(fileName = "New Foragable Data", menuName = "Foraging/New Forageable Data")]    
public class ForageableData : ScriptableObject
{
    [Header("Item Info")]
    public string itemName;
    public string description;

    [Header("Stats")]
    public int itemDifficulty;
    public ItemLocation location;
    public bool isPoisonous;
    public string season;

    [Header("Visuals")]
    public Sprite silhouetteImage;
    public Sprite silhouetteImageOccluded;
    public GameObject modelPrefab; 

    [Header("Internal")]
    public bool wasDiscovered = false; //doesn't have to be public
}
