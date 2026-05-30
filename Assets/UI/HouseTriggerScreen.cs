using UnityEngine;

public class HouseTriggerScreen : MonoBehaviour
{
    [SerializeField] private GameObject houseTriggerPanel;


    private void Awake()
    {
        HouseInteractable.OnHouseInteracted += OpenUI;
    }

    private void Start()
    {
        houseTriggerPanel.SetActive(false);        
    }

    public void OnYesButtonClicked()
    {
        Debug.Log("Trigger end of the day");
        CloseUI();
    }

    public void OnNoButtonClicked()
    {
        Debug.Log("Continue day");
        CloseUI();
    }

    public void OpenUI()
    {
        houseTriggerPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseUI()
    {
        houseTriggerPanel.SetActive(false);
        Time.timeScale = 1f;
    }

}
