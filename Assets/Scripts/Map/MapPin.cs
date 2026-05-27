using UnityEngine;

public class MapPin : MonoBehaviour
{
    public MapManager.PinType type;

    public virtual void Spawn(Vector2 location)
    {
        gameObject.SetActive(true);
        transform.GetComponent<RectTransform>().position = location;
    }

    public virtual void Remove()
    {
        gameObject.SetActive(false);
    }
}
