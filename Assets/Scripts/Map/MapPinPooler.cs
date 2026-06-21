using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;

public class MapPinPooler : MonoBehaviour
{
    List<List<GameObject>> PinPools;
    [SerializeField] int PrespawnPinAmount;

    public static event Action LoadPins;

    private void Start()
    {
        PinPools = new();

        for (int i = 0; i < MapManager.Instance.PinList.Count; i++)
        {
            List<GameObject> PinPool = new();

            for (int j = 0; j < PrespawnPinAmount; j++)
            {
                GameObject pinObject = Instantiate(MapManager.Instance.PinList[i].prefab, this.transform);
                pinObject.gameObject.SetActive(false);
                pinObject.GetComponent<RectTransform>().localScale = new Vector3(0.39f, 0.39f, 0.39f);
                pinObject.GetComponent<UnityEngine.UI.Image>().sprite = MapManager.Instance.PinList[i].sprite;
                MapPin mapPin = pinObject.GetComponent<MapPin>();
                PinPool.Add(mapPin.gameObject);
            }

            PinPools.Add(PinPool);
        }

        LoadPins?.Invoke();
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
        pinObject.GetComponent<RectTransform>().localScale = new Vector3(0.39f, 0.39f, 0.39f);
        pinObject.GetComponent<UnityEngine.UI.Image>().sprite = MapManager.Instance.PinList[(int)type].sprite;
        MapPin mapPin = pinObject.GetComponent<MapPin>();
        pool.Add(mapPin.gameObject);
        return mapPin;
    }

    public MapPin HasPinAt(Vector2 pos)
    {
        //Vector3 pos3 = new Vector3(pos.x, pos.y);
        for (int i = 0; i < PinPools.Count; i++)
        {
            List<GameObject> PinPool = PinPools[i];

            for (int j = 0; j < PinPool.Count; j++)
            {
                GameObject pin = PinPool[j];
                if (!pin.gameObject.activeInHierarchy) continue;
                if (RectTransformUtility.RectangleContainsScreenPoint(pin.GetComponent<RectTransform>(), pos))
                {
                    return pin.GetComponent<MapPin>();
                }
            }
        }
        return null;
    }
}
