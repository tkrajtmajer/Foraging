using System.Collections.Generic;
using UnityEngine;

public class MapPinPooler : MonoBehaviour
{
    List<List<GameObject>> PinPools;
    [SerializeField] int PrespawnGraveAmount;

    private void Start()
    {
        PinPools = new();

        for (int i = 0; i < MapManager.Instance.PinList.Count; i++)
        {
            List<GameObject> PinPool = new();

            for (int j = 0; j < PrespawnGraveAmount; j++)
            {
                GameObject pinObject = Instantiate(MapManager.Instance.PinList[i].prefab, this.transform);
                pinObject.gameObject.SetActive(false);
                pinObject.GetComponent<UnityEngine.UI.Image>().sprite = MapManager.Instance.PinList[i].sprite;
                MapPin mapPin = pinObject.GetComponent<MapPin>();
                PinPool.Add(mapPin.gameObject);
            }

            PinPools.Add(PinPool);
        }
    }

    public MapPin GetMapPin(MapManager.PinType type)
    {
        List<GameObject> pool = PinPools[(int)type];

        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].gameObject.activeInHierarchy)
            {
                return pool[i].gameObject.GetComponent<MapPin>();
            }
        }

        GameObject pinObject = Instantiate(MapManager.Instance.PinList[(int)type].prefab, this.transform);
        pinObject.gameObject.SetActive(false);
        pinObject.GetComponent<UnityEngine.UI.Image>().sprite = MapManager.Instance.PinList[(int)type].sprite;
        MapPin mapPin = pinObject.GetComponent<MapPin>();
        pool.Add(mapPin.gameObject);
        return mapPin;
    }
}
