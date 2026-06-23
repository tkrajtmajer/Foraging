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
    [SerializeField] public LevelDatabase levelDatabase;
    public int currentLevel = 1;

    //[SerializeField] private bool[][] levelStates = new bool[5][];

    public static event Action<LevelData> LevelLoaded;

    public static LevelManager Instance;

    private void Awake()
    {
        //if (Instance != null && Instance != this)
        //{
        //    Destroy(this);
        //}
        //else
        //{
            Instance = this;
        //    DontDestroyOnLoad(this.gameObject);
        //}
    }

    public void OnLevelSelected(int levelNum)
    {
        Debug.Log("Level " + levelNum + " selected");
        LevelLoaded?.Invoke(levelDatabase.levelList[levelNum - 1]);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void UpdateLevelState(int levelNum, List<bool> levelState)
    {
        levelDatabase.levelList[levelNum - 1].levelState = levelState;
    }

}
