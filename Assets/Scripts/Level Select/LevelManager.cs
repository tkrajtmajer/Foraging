using NUnit.Framework;
using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    [SerializeField] ItemDatabase itemDatabase;
    [SerializeField] Recipe recipe;

    public event Action<LevelData> LevelLoaded;

    [Serializable]
    public class LevelData
    {
        public int levelNumber;
        public List<Recipe> recipes;
        public bool unlocked;
        //public 
    }

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
}
