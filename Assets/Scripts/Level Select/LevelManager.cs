using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LevelManager : MonoBehaviour
{
    //[SerializeField] ItemDatabase itemDatabase;
    [SerializeField] private List<Recipe> allRecipes = new List<Recipe>();
    [SerializeField] public float stdScale = 1.72f;
    [SerializeField] public float smallScale = 0.96f;
    public int currentLevel = 1;

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
    private void OnEnable()
    {
        LevelSelectManager.LevelSelected += OnLevelSelected;
    }

    private void OnDisable()
    {
        LevelSelectManager.LevelSelected -= OnLevelSelected;
    }

    public void OnLevelSelected(int levelNum)
    {
        Debug.Log("Level " + levelNum + " selected");
        LevelLoaded?.Invoke(levelList[levelNum - 1]);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    
}
