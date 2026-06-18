using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LevelDatabase", menuName = "Scriptable Objects/LevelDatabase")]
public class LevelDatabase : ScriptableObject
{
    public List<LevelData> levelList;
}
