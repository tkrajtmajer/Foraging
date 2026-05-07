public enum ItemLocation {
    Forest,
    River
} 

[System.Serializable]
public class Item
{
    public int itemDifficulty;
    public string itemName;
    public ItemLocation location;
    public string description;
    public bool isPoisonous;
}