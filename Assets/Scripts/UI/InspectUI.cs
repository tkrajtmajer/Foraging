using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InspectUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject inspectPanel; // background panel
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemDescription;

    private ForageableData currentForageableData;

    private void OnEnable()
    {
        ForageableInteractable.OnForageableInteracted += OpenInspectUI;
    }

    private void Start()
    {
        inspectPanel.SetActive(false);
    }

    private void OnDisable()
    {
        ForageableInteractable.OnForageableInteracted -= OpenInspectUI;
    }

    private void OpenInspectUI(ForageableData forageableData)
    {
        // TODO - if journal.IsDiscovered(data) -> show image, else show silhouette
        itemImage.sprite = forageableData.silhouetteImage;
        itemDescription.text = forageableData.description;

        inspectPanel.SetActive(true);

        // pause the game
        Time.timeScale = 0f;
    }

    
    public void OnCollectButtonClicked()
    {
        // TODO - add to inventory logic Inventory.add(currentForageableData)
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
    }


}
