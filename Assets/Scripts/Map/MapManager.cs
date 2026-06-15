using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;
using TMPro;

public class MapManager : MonoBehaviour
{
    [SerializeField] GameObject mapContainer;
    [SerializeField] GameObject pinPrefab;
    [SerializeField] RectTransform mapRect;
    public bool mapOpen = false;

    public PinType selectedPinType = PinType.WildStrawberry;
    [SerializeField] MapPinPooler pinPooler;

    public static MapManager Instance { get; private set; }
    public InputActionMap toggleMapActions;
    public InputActionMap pinActions;

    [SerializeField] private TMP_Text currentDayText;

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
        }

        toggleMapActions["Toggle Map"].performed += ToggleMap;
        pinActions["Place Pin"].performed += PlacePin;
        //pinActions["Switch Pin Type"].performed += SwitchPinType;
    }

    private void OnEnable()
    {
        toggleMapActions.Enable();
        pinActions.Enable();

        UIManager.ClosedUI += CloseMap;
    }

    private void OnDisable()
    {
        toggleMapActions.Disable();
        pinActions.Disable();

        UIManager.ClosedUI -= CloseMap;
    }

    private void ToggleMap(InputAction.CallbackContext context) 
    {
        if (mapOpen)
        {
            CloseMap();
        }
        else if (UIManager.Instance.currentUIState == UIState.None)
        {
            OpenMap();
        }
    }

    private void OpenMap()
    {
        mapOpen = true;
        mapContainer.SetActive(true);
        UIManager.Instance.SetState(UIState.Map);
        Time.timeScale = 0.0f;
    }

    private void CloseMap()
    {
        mapOpen = false;
        mapContainer.SetActive(false);
        UIManager.Instance.SetState(UIState.None);
        Time.timeScale = 1.0f;
    }

    public enum PinType
    {
        WildStrawberry,
        MockStrawberry,
        Rosemary,
        Dandelion,
        AloeVera,
        StingingNettle,
        WildGarlic,
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
        //Debug.Log(mousePos);
        if (!MapContainsMouse(mousePos)) return;
        MapPin newPin = pinPooler.GetMapPin(selectedPinType);
        newPin.Spawn(mousePos);
    }

    private bool MapContainsMouse(Vector2 mousePos)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(mapRect, mousePos);
    }

    public void SwitchPinType(InputAction.CallbackContext context)
    {
        selectedPinType = (PinType)(((int)(selectedPinType) + 1) % PinList.Count);
    }

    public void SelectPinType(int idx)
    {
        selectedPinType = (MapManager.PinType)idx;
    }
}
