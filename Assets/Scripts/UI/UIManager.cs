using System;
using UnityEngine;

public enum UIState
{
    None, // inventory and recipes don't count since they should stay on
    Inspect,
    Almanac,
    Map,
    Pause
}

public class UIManager : MonoBehaviour
{
    public static event Action ClosedUI;
    public static event Action PauseGame;
    public static UIManager Instance;

    InputSystem_Actions controls;

    internal UIState currentUIState = UIState.None;

    private void Awake()
    {
        //if (Instance != null && Instance != this)
        //{
        //    Destroy(this);
        //}
        //else {
            Instance = this;
        //}

        controls = new InputSystem_Actions();

        controls.UI.CloseUI.performed += ctx => CloseUI();

    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    public void SetState(UIState state)
    {
        currentUIState = state;
    }

    public void CloseUI()
    {
        if (currentUIState == UIState.None)
        {
            PauseGame?.Invoke();
            return;
        }
        //SetState(UIState.None);
        ClosedUI?.Invoke();
    }
}

