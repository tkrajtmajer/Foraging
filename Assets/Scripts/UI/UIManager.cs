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
    public static UIManager Instance;

    internal UIState currentUIState = UIState.None;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else {
            Instance = this;
        }
    }

    public void SetState(UIState state)
    {
        currentUIState = state;
    }
}

