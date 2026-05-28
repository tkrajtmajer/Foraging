using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Scriptable Objects/Recipe")]
public class Recipe : ScriptableObject
{
    public List<ForageableInteractable> forageablesInRecipe = new List<ForageableInteractable>();
}
