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

    [SerializeField] private GameObject crossButtonPrefab;
    [SerializeField] private float crossHeightDelta = 40;

    void Start()
    {
        ingredientList.text = "";

        List<ForageableData> currentRecipe = GameManager.Instance.currentRecipe.forageablesInRecipe;
        int i = 0;

        foreach (ForageableData item in currentRecipe) {
            ingredientList.text += "- " + item.itemName + "\n";
            GameObject button = Instantiate(crossButtonPrefab, ingredientList.GetComponent<RectTransform>());
            button.GetComponent<RectTransform>().localPosition = new Vector3(-76, -crossHeightDelta / 2 -i++ * crossHeightDelta, 0);
        }

        float heightBg = currentRecipe.Count * heightPerIngredient + padding * 2;

        recipeBg.sizeDelta = new Vector2(recipeBg.sizeDelta.x, heightBg);
    }
}
