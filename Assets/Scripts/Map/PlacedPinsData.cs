using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlacedPinsData : MonoBehaviour
{
    [Serializable]
    public class PlacedData
    { 
        public Vector2 pos = new Vector2(0, 0);
        public MapManager.PinType type;
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

    private void OnEnable()
    {
        //SceneManager.sceneLoaded += OnSceneLoaded;
        MapPinPooler.LoadPins += LoadPins;
    }

    private void OnDisable()
    {
        //SceneManager.sceneLoaded -= OnSceneLoaded;
        MapPinPooler.LoadPins -= LoadPins;
    }

    public List<PlacedData> placedPinsList = new();

    public void AddPlacedPin(Vector2 pos, MapManager.PinType type)
    {
        PlacedData pinData = new PlacedData();
        pinData.pos = pos;
        pinData.type = type;
        placedPinsList.Add(pinData);
    }

    public void RemovePlacedPin(MapPin pin)
    {
        foreach (PlacedData pinData in placedPinsList) 
        {
            if (pin.RectContainsPoint(pinData.pos))
            {
                placedPinsList.Remove(pinData);
                return;
            }
        }
    }

    //public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    if (scene.buildIndex == GameManager.Instance.finalMainSceneIdx)
    //    {
    //        //StartCoroutine(LoadPins());
    //        LoadPins();
    //    }
    //}

    //private IEnumerator LoadPins()
    //{
    //    for (float i = 0; i < 5; i += Time.deltaTime)
    //    {
    //        yield return null;
    //    }

    //    MapManager.Instance.PlacePinsOnReload(placedPinsList);
    //    yield return null;
    //}

    public void LoadPins()
    {
        MapManager.Instance.PlacePinsOnReload(placedPinsList);
    }
}