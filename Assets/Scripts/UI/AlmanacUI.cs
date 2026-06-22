using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class AlmanacUI : MonoBehaviour
{
    [Header("Containers")]
    [SerializeField] private GameObject almanacUIContainer;
    [SerializeField] private GameObject itemizedViewContainer;
    [SerializeField] private GameObject individualViewContainer;

    [Header("Itemized View Setup")]
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Transform itemContainer; 
    [SerializeField] private Transform leftPageStart; 
    [SerializeField] private float itemGap;
    [SerializeField] private float heightGap;
    [SerializeField] private Transform rightPageStart;
    [SerializeField] int itemsPerRow = 3;
    [SerializeField] int itemsPerPage = 12;

    [Header("Individual View Setup")]
    [SerializeField] private TMP_Text itemNameUI;
    [SerializeField] private TMP_Text itemLocationUI;
    [SerializeField] private TMP_Text itemPoisonousUI;
    [SerializeField] private Image itemSpriteUI;
    [SerializeField] private TMP_Text itemDescriptionUI;
    [SerializeField] private GameObject itemRenderUI;

    private int currentPage = 1;
    private int nrOfPages;
    private bool bookOpen = false;
    private bool individualView = false;
    private int currentSelected = 1;
    private List<AlmanacItemUI> currentItems = new();

    private bool viewFromInspect = false;
    public static event Action CloseAlmanac;
    [Header("Drag")]
    [SerializeField] DragUI dragUI;

    void Start() {
        int allItemsSize = GameManager.Instance.itemDatabase.allItemPrefabs.Count;

        nrOfPages = Mathf.CeilToInt(allItemsSize / (itemsPerPage*2.0f));
        HideAlmanac();
    }

    void OnEnable() {
        InspectUI.OpenAlmanac += ShowFromInspect;
        UIManager.ClosedUI += TryHideAlmanac;
    }

    void OnDisable() {
        InspectUI.OpenAlmanac -= ShowFromInspect;
        UIManager.ClosedUI -= TryHideAlmanac;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.J)) {
            if (bookOpen) {
                if (viewFromInspect) return;

                HideAlmanac();
                Time.timeScale = 1f;
                UIManager.Instance.SetState(UIState.None);
            }
            else {
                if(UIManager.Instance.currentUIState == UIState.None) {
                    ShowAlmanac();
                    ShowItemizedView();
                    DrawItemsUI();
                    //ChangeSelected(0);
                    Time.timeScale = 0f;
                    UIManager.Instance.SetState(UIState.Almanac);
                }
            }
        }

        if(bookOpen && !individualView) {
            if (Input.GetKeyDown(KeyCode.RightArrow)) ChangePage(1);
            if (Input.GetKeyDown(KeyCode.LeftArrow)) ChangePage(-1);

            if (Input.GetKeyDown(KeyCode.L)) ChangeSelected(1);
            if (Input.GetKeyDown(KeyCode.K)) ChangeSelected(-1);

            if (Input.GetKeyDown(KeyCode.Return)) {
                ToggleItemDetails();
            }
        }

        else if(bookOpen && individualView) {
            if(Input.GetKeyDown(KeyCode.Backspace)) {
                ToggleItemDetails();
            }
        }
    }

    private void DrawItemsUI() {
        for (int i = itemContainer.childCount - 1; i >= 0; i--) Destroy(itemContainer.GetChild(i).gameObject);
        currentItems.Clear();

        List<GameObject> allItems = GameManager.Instance.itemDatabase.allItemPrefabs;

        int startIdx = (currentPage - 1) * (itemsPerPage * 2);
        int endIdx = Mathf.Min(startIdx + (itemsPerPage * 2), allItems.Count);

        for (int i = startIdx; i < endIdx; i++) {
            ForageableData currentItem = allItems[i].GetComponent<ForageableInteractable>().Data;

            GameObject itemUI = Instantiate(itemPrefab, itemContainer);

            int localIndex = i - startIdx;
            int pageIndex = localIndex % itemsPerPage;
            int column = pageIndex % itemsPerRow;
            int row = pageIndex / itemsPerRow;

            if (localIndex < itemsPerPage) {
                itemUI.GetComponent<RectTransform>().anchoredPosition = leftPageStart.GetComponent<RectTransform>().anchoredPosition + new Vector2(column * itemGap, row * -heightGap);
            }
            else {
                itemUI.GetComponent<RectTransform>().anchoredPosition = rightPageStart.GetComponent<RectTransform>().anchoredPosition + new Vector2(column * itemGap, row * -heightGap);
            }

            AlmanacItemUI ui = itemUI.GetComponent<AlmanacItemUI>();
            ui.UseItemData(currentItem);
            ui.ItemSelected += HandleButtonSelect;
            currentItems.Add(ui);
        }
    }

    public void ChangePage(int direction) {
        if (currentPage + direction < 1 || currentPage + direction > nrOfPages) return;
        
        currentPage += direction;
        currentSelected = 1;
        DrawItemsUI();
        //ChangeSelected(0);
    }

    private void ChangeSelected(int direction) {
        if (currentSelected + direction < 1 || currentSelected + direction > currentItems.Count) return;
        
        // deselect previous
        currentItems[currentSelected-1].ToggleActive(false);

        currentSelected += direction;
        // select current
        currentItems[currentSelected-1].ToggleActive(true);
    }

    public void ToggleItemDetails() {
        individualView = !individualView;

        if(individualView) {
            Select(currentItems[currentSelected-1].itemData);
            ShowIndividualView();
        }
        else {
            if(viewFromInspect) {
                Debug.Log("view from inspect");
                CloseAlmanac?.Invoke();
                viewFromInspect = false;
                individualView = false;
                HideAlmanac();
            }
            else {
                ShowItemizedView();
            }

            dragUI.CleanUp();
        }
    }

    private void Select(ForageableData selectedItem) {
        itemNameUI.text = selectedItem.itemName;

        itemDescriptionUI.text = "";
        foreach (string desc in selectedItem.description) {
            itemDescriptionUI.text += "- " + desc + "\n";
        }
        if(selectedItem.wasDiscovered) {
            foreach (string extra in selectedItem.extraInfo) {
                itemDescriptionUI.text += "- " + extra + "\n";
            }
        }

        itemPoisonousUI.text = selectedItem.isPoisonous? "Poisonous" : "Not poisonous";

        itemLocationUI.text = "Found in " + selectedItem.season.ToString();
        if(selectedItem.wasDiscovered) itemLocationUI.text += ", in " + string.Join(", ", selectedItem.locations);

        itemSpriteUI.sprite = selectedItem.silhouetteImage;
        
        if(selectedItem.wasDiscovered) {
            dragUI.SetupDragRender(selectedItem);
            itemRenderUI.SetActive(true);
        }
        else {
            itemRenderUI.SetActive(false);

            // dont need to black out sprite w current setup lol
        }
    }

    private void HandleButtonSelect(AlmanacItemUI uiItem) {
        Select(uiItem.itemData);
        ShowIndividualView();
    }

    private void ShowFromInspect(ForageableData selectedItem) {
        // Select(selectedItem);

        // viewFromInspect = true;
        ShowAlmanac();
        ShowItemizedView();
        DrawItemsUI();
        //ChangeSelected(0);
    }

    private void ShowItemizedView()
    {
        individualView = false;
        itemizedViewContainer.SetActive(true);
        individualViewContainer.SetActive(false);
    }

    private void ShowIndividualView()
    {
        individualView = true;
        itemizedViewContainer.SetActive(false);
        individualViewContainer.SetActive(true);
    }

    private void ShowAlmanac()
    {
        UIManager.Instance.currentUIState = UIState.Almanac;
        bookOpen = true;
        almanacUIContainer.SetActive(true);
    }

    private void HideAlmanac()
    {
        bookOpen = false;
        almanacUIContainer.SetActive(false);
    }
    
    public void TryHideAlmanac()
    {
        if (UIManager.Instance.currentUIState != UIState.Almanac) return;

        if (viewFromInspect) 
        { 
            ToggleItemDetails();
            Debug.Log("Closed");
            return; 
        }
        
        
        HideAlmanac();
        Time.timeScale = 1f;
        UIManager.Instance.SetState(UIState.None);
    }
}
