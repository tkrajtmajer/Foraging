using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;

public class MapManager : MonoBehaviour
{
    [SerializeField] GameObject map;
    [SerializeField] GameObject pinPrefab;
    public bool mapOpen = false;

    public PinType selectedPinType = PinType.Mushroom;
    [SerializeField] MapPinPooler pinPooler;

    public static MapManager Instance { get; private set; }
    public InputActionMap toggleMapActions;
    public InputActionMap pinActions;

    //public static event UnityAction<MapPinPooler, PinType> pinPlacedEvent;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        toggleMapActions["Toggle Map"].performed += ToggleMap;
        pinActions["Place Pin"].performed += PlacePin;
        pinActions["Switch Pin Type"].performed += SwitchPinType;
    }

    private void OnEnable()
    {
        toggleMapActions.Enable();
        pinActions.Enable();
    }

    private void OnDisable()
    {
        toggleMapActions.Disable();
        pinActions.Disable();
    }

    private void ToggleMap(InputAction.CallbackContext context) 
    {
        mapOpen = !mapOpen;
        map.SetActive(mapOpen);
    }

    public enum PinType
    {
        Mushroom,
        Banana,
    }

    [Serializable]
    public class PinData
    {
        public PinType type;
        public Sprite sprite;
        public GameObject prefab;
    }

    public List<PinData> PinList = new();

    public PinData GetPinData(PinType type) => PinList[(int)type];
    public PinData GetPinData(int idx) => PinList[idx]; 

    public void PlacePin(InputAction.CallbackContext context)
    {
        //pinPlacedEvent.Invoke(pinPooler, selectedPinType);
        if (!mapOpen) return;
        Vector2 mousePos = Input.mousePosition;
        MapPin newPin = pinPooler.GetMapPin(selectedPinType);
        newPin.Spawn(mousePos);
    }

    public void SwitchPinType(InputAction.CallbackContext context)
    {
        selectedPinType = (PinType)(((int)(selectedPinType) + 1) % PinList.Count);
    }
}
