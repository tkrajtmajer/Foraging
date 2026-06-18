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
    [SerializeField] private List<GameObject> items;
    [SerializeField] private List<GameObject> strikethroughs;

    public static event Action<int> LevelSelected;

    private void OnEnable()
    {
        levelText.text = levelData.recipe.recipeName.ToString();
        scoreText.text = levelData.score.ToString();
        totalText.text = levelData.totalItems.ToString();

    }

    public void SelectLevel()
    {
        LevelSelected?.Invoke(levelData.levelNumber);
    }

    public void TryStrike(int itemIdx, bool strike)
    {
        if (items[itemIdx].activeInHierarchy)
        {
            strikethroughs[itemIdx].SetActive(strike);
        }
    }
}
