using UnityEngine;

public class HouseTriggerScreen : MonoBehaviour
{
    [SerializeField] private GameObject houseTriggerPanel;


    private void OnEnable()
    {
        HouseInteractable.OnHouseInteracted += OpenUI;
        UIManager.ClosedUI += OnNoButtonClicked;
    }
    private void OnDisable()
    {
        HouseInteractable.OnHouseInteracted -= OpenUI;
        UIManager.ClosedUI -= OnNoButtonClicked;
    }

    private void Start()
    {
        houseTriggerPanel.SetActive(false);        
    }

    public void OnYesButtonClicked()
    {
        Debug.Log("Trigger end of the day");
        GameManager.Instance.FinishDay();
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
