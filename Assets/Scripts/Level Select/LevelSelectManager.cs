using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LevelSelectManager : MonoBehaviour
{
    //public int maxLevel { get => GameManager.Instance.maxDays; }
    [SerializeField] private List<Level> levelList;
    LevelDatabase Database { get => LevelManager.Instance.levelDatabase; }

    private void OnEnable()
    {
        UpdateLevelStates();
        Level.LevelSelected += OnLevelSelected;
    }

    private void OnDisable()
    {
        Level.LevelSelected -= OnLevelSelected;
    }

    public void OnLevelSelected(int level)
    {
        LevelManager.Instance.OnLevelSelected(level);
    }

    private void UpdateLevelStates()
    {
        for (int i = 0; i < levelList.Count; ++i)
        {
            Level level = levelList[i];
            List<bool> correctLevelState = Database.levelList[i].levelState;
            for (int j = 0; j < level.levelData.levelState.Count; ++j)
            {
                level.TryStrike(j, correctLevelState[j]);
            }
        }
    }

}
