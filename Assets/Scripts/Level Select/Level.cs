using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class Level : MonoBehaviour
{
    [SerializeField] public LevelData levelData;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI totalText;
    [SerializeField] public Button button;
    [SerializeField] private List<TMP_Text> itemTexts;
    [SerializeField] private List<GameObject> strikethroughs;

    public static event Action<int> LevelSelected;

    private void OnEnable()
    {
        levelText.text = levelData.recipe.recipeName.ToString();
        scoreText.text = levelData.score.ToString();
        totalText.text = levelData.totalItems.ToString();

        // update recipe item names
        List<ForageableData> recipeItems = levelData.recipe.forageablesInRecipe;

        for (int i = 0; i < itemTexts.Count; i++) {
            if (i < recipeItems.Count) {
                // item should be active                
                itemTexts[i].gameObject.SetActive(true);
                itemTexts[i].text = "◦ " + recipeItems[i].itemName;
            }
            else {
                // else inactive
                itemTexts[i].gameObject.SetActive(false);
            }

            strikethroughs[i].SetActive(false);
        }
    }

    public void SelectLevel()
    {
        LevelSelected?.Invoke(levelData.levelNumber);
    }

    public void TryStrike(int itemIdx, bool strike)
    {
        if (itemTexts[itemIdx].gameObject.activeInHierarchy)
        {
            strikethroughs[itemIdx].SetActive(strike);
        }
    }
}
