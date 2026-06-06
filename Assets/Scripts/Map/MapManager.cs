using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    [SerializeField] GameObject mapContainer;
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
        }

        toggleMapActions["Toggle Map"].performed += ToggleMap;
        pinActions["Place Pin"].performed += PlacePin;
        pinActions["Switch Pin Type"].performed += SwitchPinType;
    }

    private void OnEnable()
    {
        toggleMapActions.Enable();
        pinActions.Enable();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        toggleMapActions.Disable();
        pinActions.Disable();

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == GameManager.Instance.finalMainSceneIdx)
        {
            GameObject mapUI = GameObject.Find("MapUI");
            mapContainer = mapUI.transform.GetChild(0).gameObject;
            pinPooler = mapContainer.GetComponentInChildren<MapPinPooler>();
        }
    }

    private void ToggleMap(InputAction.CallbackContext context) 
    {
        if (mapOpen)
        {
            mapOpen = false;
            mapContainer.SetActive(false);
            UIManager.Instance.SetState(UIState.None);
        }
        else if (UIManager.Instance.currentUIState == UIState.None)
        {
            mapOpen = true;
            mapContainer.SetActive(true);
            UIManager.Instance.SetState(UIState.Map);
        }
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
