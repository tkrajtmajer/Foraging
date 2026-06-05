using UnityEngine;

public enum UIState
{
    None,
    Inspect,
    Almanac,
    Map,
    Pause,
    Inventory,
    HouseTrigger,
    Score,
    Ending
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
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void SetState(UIState state)
    {
        currentUIState = state;

        switch (currentUIState)
        {
            case UIState.HouseTrigger:
                print("House Trigger UI state");
                TimeManager.Instance.gameObject.SetActive(false);
                break;
            case UIState.None:
                print("UI Closed, restart time");
                TimeManager.Instance.gameObject.SetActive(true);
                break;
            default:
                print("Default UI stop everything!!");
                TimeManager.Instance.gameObject.SetActive(false);
                break;
        }
    }
}

