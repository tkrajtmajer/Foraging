using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class RecipeUI : MonoBehaviour
{
    [SerializeField] private RectTransform recipeBg;
    [SerializeField] private TMP_Text ingredientList;

    [SerializeField] private float heightPerIngredient = 30f;
    [SerializeField] private float padding = 20f;

    void Start()
    {
        ingredientList.text = "";

        List<ForageableData> currentRecipe = GameManager.Instance.currentRecipe.forageablesInRecipe;

        foreach (ForageableData item in currentRecipe) {
            ingredientList.text += "- " + item.itemName + "\n";
        }

        float heightBg = currentRecipe.Count * heightPerIngredient + padding * 2;

        recipeBg.sizeDelta = new Vector2(recipeBg.sizeDelta.x, heightBg);
    }
}
