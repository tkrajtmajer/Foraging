using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LevelData", menuName = "Scriptable Objects/LevelData")]
public class LevelData : ScriptableObject
{
    public int levelNumber;
    public Recipe recipe;

    public List<bool> levelState;
    public int score;
    public int totalItems;
}