using System;
using UnityEngine;
using System.Collections.Generic;

public class PlacedPinsData : MonoBehaviour
{
    [Serializable]
    public class PlacedData
    {
        public Vector2 pos;
        public byte poisonous;
    }

    public static PlacedPinsData Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public List<PlacedData> placedPinsList = new();

    public void AddPlacedPin(Vector2 pos, byte poisonous)
    {
        PlacedData pinData = new PlacedData();
        pinData.pos = pos;
        pinData.poisonous = poisonous;
        placedPinsList.Add(pinData);
    }

    public void RemovePlacedPin(Vector2 pos)
    {
        foreach (PlacedData pinData in placedPinsList) 
        {
            Debug.Log("Checked");
            if (pinData.pos == pos)
            {
                placedPinsList.Remove(pinData);
                Debug.Log("Removed");
                return;
            }
        }

    }
}
