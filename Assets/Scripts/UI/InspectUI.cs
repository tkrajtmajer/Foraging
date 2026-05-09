using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // for drags
using TMPro;

public class InspectUI : MonoBehaviour, IDragHandler
{
    [Header("UI References")]
    [SerializeField] private GameObject inspectPanel; // UI parent
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemDescription;


    [Header("3d Item Render")]
    [SerializeField] private Transform spawnPointModel; // point where the model is going to spawn
    [SerializeField] private float rotationSpeed = 0.5f;

    private ForageableData currentForageableData;
    private GameObject currentSpawnedModel;

    private void Awake()
    {
        ForageableInteractable.OnForageableInteracted += OpenInspectUI;
    }

    private void Start()
    {
        inspectPanel.SetActive(false);
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
        Debug.Log("hello?");
        inspectPanel.SetActive(true);

        // 3d render part
        if (currentSpawnedModel != null)
        {
            Destroy(currentSpawnedModel);
        }
        
        if (forageableData.modelPrefab != null)
        {
            currentSpawnedModel = Instantiate(forageableData.modelPrefab, spawnPointModel.position, Quaternion.identity);
            currentSpawnedModel.transform.SetParent(spawnPointModel);
        }

        // pause the game
        Time.timeScale = 0f;
    }


    // this method is called ON THIS UI object when you click and drag
    public void OnDrag(PointerEventData eventData)
    {
        if (currentSpawnedModel != null)
        {
            // eventData.delta gives us the mouse movement since the last frame
            float rotX = -eventData.delta.y * rotationSpeed;
            float rotY = -eventData.delta.x * rotationSpeed;

            // Rotate the object around the camera's axes so the rotation feels intuitive
            currentSpawnedModel.transform.Rotate(Vector3.up, rotY, Space.World);
            currentSpawnedModel.transform.Rotate(Vector3.right, rotX, Space.World);
        }
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

        // clean up 3d model
        if (currentSpawnedModel != null)
        {
            Destroy(currentSpawnedModel);
        }
    }


}
