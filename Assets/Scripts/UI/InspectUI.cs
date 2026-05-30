using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InspectUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject inspectPanel; // UI parent
    [SerializeField] private Image itemImage;
    [SerializeField] DragUI dragUI;
    [SerializeField] private TextMeshProUGUI itemDescription;

    private ForageableData currentForageableData;

    public static event Action<ForageableData> OpenAlmanac;

    private void Awake()
    {
        ForageableInteractable.OnForageableInteracted += OpenInspectUI;
    }

    private void Start()
    {
        inspectPanel.SetActive(false);
    }

    void OnEnable() {
        AlmanacUI.CloseAlmanac += ShowInspect;
    }

    void OnDisable() {
        AlmanacUI.CloseAlmanac -= ShowInspect;
    }

    private void OnDestroy()
    {
        ForageableInteractable.OnForageableInteracted -= OpenInspectUI;
    }

    private void OpenInspectUI(ForageableData forageableData)
    {
        // TODO - if journal.IsDiscovered(data) -> show image, else show silhouette
        itemImage.sprite = forageableData.silhouetteImage;
        itemDescription.text = forageableData.description;
        //Debug.Log("hello?");
        inspectPanel.SetActive(true);

        currentForageableData = forageableData;
        dragUI.SetupDragRender(currentForageableData);

        // pause the game
        Time.timeScale = 0f;
        UIManager.Instance.SetState(UIState.Inspect);
    }

    public void OpenAlmanacForItem() {
        inspectPanel.SetActive(false);
        OpenAlmanac?.Invoke(currentForageableData);
    }

    public void OnCollectButtonClicked()
    {
        // Inventory logic
        //Inventory.Instance.TryAddObject(currentForageableData);


        CloseUI();
    }

    public void OnLeaveButtonClicked()
    {
        CloseUI();
    } 


    public void CloseUI()
    {
        inspectPanel.SetActive(false);
        Time.timeScale = 1f;

        dragUI.CleanUp();
        UIManager.Instance.SetState(UIState.None);
    }

    private void ShowInspect() {
        Debug.Log("show inspect again");
        inspectPanel.SetActive(true);
    }


}
