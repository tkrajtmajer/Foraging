using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System;

public class FeedbackUI : MonoBehaviour
{
    [SerializeField] private RectTransform goodSpawnArea;
    [SerializeField] private RectTransform badSpawnArea;
    [SerializeField] private Image spritePrefab;

    [Header("Almanac in feedback screen")]
    [SerializeField] private GameObject fakeAlmanac;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private TMP_Text itemNameUI;
    [SerializeField] private TMP_Text itemLocationUI;
    [SerializeField] private TMP_Text itemPoisonousUI;
    [SerializeField] private Image itemSpriteUI;
    [SerializeField] private TMP_Text itemDescriptionUI;
    [SerializeField] DragUI dragUI;

    int currentItemToShow = 0;

    List<ForageableData> goodObjects = new();
    List<ForageableData> currentRecipe;
    List<ForageableData> playerInventory;

    public static event Action ShowFinalScreen;


    void Start() {
        fakeAlmanac.SetActive(false);
        SpawnObjectSprites();
    }

    void SpawnObjectSprites() {
        currentRecipe = GameManager.Instance.currentRecipe.forageablesInRecipe;
        playerInventory = GameManager.Instance.previousInventory;

        List<ForageableData> remainingItems = new(currentRecipe);

        foreach (ForageableData item in playerInventory) {
            if (remainingItems.Contains(item)) {
                goodObjects.Add(item);
                remainingItems.Remove(item);
                SpawnSpriteInArea(goodSpawnArea, item.silhouetteImage);
            }

            else SpawnSpriteInArea(badSpawnArea, item.silhouetteImage);
        }
    }

    void SpawnSpriteInArea(RectTransform area, Sprite sprite) {
        Rect rect = area.rect;

        float x = UnityEngine.Random.Range(rect.xMin, rect.xMax);
        float y = UnityEngine.Random.Range(rect.yMin, rect.yMax);

        Vector2 spawnPos = new Vector2(x, y);

        Image image = Instantiate(spritePrefab, area);
        image.sprite = sprite;
        image.rectTransform.anchoredPosition = spawnPos;
    }

    public void ShowGoodItems() {
        List<ForageableData> uniqueGood = new List<ForageableData>();

        foreach (ForageableData item in goodObjects) {
            if (!uniqueGood.Contains(item)) {
                uniqueGood.Add(item);
            }
        }

        goodObjects = uniqueGood;

        fakeAlmanac.SetActive(true);
        nextButton.SetActive(false);

        ShowNextGoodItem();
    }

    public void ShowNextGoodItem() {
        if(currentItemToShow >= goodObjects.Count) {
            ShowReturnScreen();
            return;
        }

        ForageableData currentObj = goodObjects[currentItemToShow];

        itemNameUI.text = currentObj.itemName;
        // itemDescriptionUI.text = currentObj.description;
        itemDescriptionUI.text = "";
        foreach (string desc in currentObj.description) {
            itemDescriptionUI.text += "- " + desc + "\n";
        }
        foreach (string extra in currentObj.extraInfo) {
            itemDescriptionUI.text += "- " + extra + "\n";
        }

        itemPoisonousUI.text = "Not poisonous";

        itemLocationUI.text = "Found in " + currentObj.season + ", in " + currentObj.location.ToString();

        itemSpriteUI.sprite = currentObj.silhouetteImage;

        dragUI.SetupDragRender(currentObj);

        currentItemToShow++;
    }

    void ShowReturnScreen() {
        ShowFinalScreen?.Invoke();
    }
}
