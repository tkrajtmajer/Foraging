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

    List<ForageableData> goodObjects;
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

        foreach (ForageableData item in playerInventory) {
            if(currentRecipe.Contains(item)) {
                goodObjects.Add(item);

                SpawnSpriteInArea(goodSpawnArea, item.silhouetteImage);
            }

            else SpawnSpriteInArea(badSpawnArea, item.silhouetteImage);
        }
    }

    void SpawnSpriteInArea(RectTransform area, Sprite sprite) {
        Vector2 size = area.rect.size;

        float x = UnityEngine.Random.Range(-size.x * 0.5f, size.x * 0.5f);
        float y = UnityEngine.Random.Range(-size.y * 0.5f, size.y * 0.5f);

        Vector2 spawnPos = new Vector2(x, y);

        Image image = Instantiate(spritePrefab, area);
        image.sprite = sprite;
        image.rectTransform.anchoredPosition = spawnPos;
    }

    public void ShowGoodItems() {
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
        itemDescriptionUI.text = currentObj.description;
        itemPoisonousUI.text = "Not poisonous";
        itemLocationUI.text = currentObj.location.ToString() + ", " + currentObj.season;
        itemSpriteUI.sprite = currentObj.silhouetteImage;

        dragUI.SetupDragRender(currentObj);

        currentItemToShow++;
    }

    void ShowReturnScreen() {
        ShowFinalScreen?.Invoke();
    }
}
