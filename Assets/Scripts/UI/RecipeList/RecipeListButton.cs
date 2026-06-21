using TMPro;
using UnityEngine;

public class RecipeListButton : MonoBehaviour
{
    [SerializeField] GameObject strikethrough;

    public void OnButtonCliked()
    {
        strikethrough.SetActive(!strikethrough.activeInHierarchy);
    }
}
