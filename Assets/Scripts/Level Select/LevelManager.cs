using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    //[SerializeField] ItemDatabase itemDatabase;
    [SerializeField] private List<Recipe> allRecipes = new List<Recipe>();
    [SerializeField] GameObject levelsContainer;
    [SerializeField] public float stdScale = 1.72f;
    [SerializeField] public float smallScale = 0.96f;
    public int currentLevel = 1;
    public int maxLevel = 3;

    public static event Action<LevelData> LevelLoaded;


    public static LevelManager Instance;

    public List<LevelData> levelList;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void OnLevelSelected(int levelNum)
    {
        Debug.Log("LevelSelected");
        LevelLoaded?.Invoke(levelList[levelNum]);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void MoveLevels(int side)
    {
        // -1 -> left, 1 -> right
        if (currentLevel == 1 & side == 1) return;
        if (currentLevel == maxLevel & side == -1) return;
        currentLevel -= side;
        levelsContainer.transform.localPosition += side * new Vector3(310, 0, 0);
        foreach (Level level in levelsContainer.GetComponentsInChildren<Level>())
        {
            float alpha = (level.levelData.levelNumber == currentLevel) ? 98 : 49;
            foreach (UnityEngine.UI.Image img in level.GetComponentsInChildren<UnityEngine.UI.Image>())
            {
                img.color = new Color(img.color.r, img.color.g, img.color.b, alpha / 100);
            }
            TextMeshProUGUI text = level.GetComponentInChildren< TextMeshProUGUI>();
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha / 100);
        }
    }
}
