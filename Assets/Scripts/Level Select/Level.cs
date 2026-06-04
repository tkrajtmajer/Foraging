using UnityEngine;
using TMPro;

public class Level : MonoBehaviour
{
    [SerializeField] int levelNum;
    [SerializeField] TextMeshProUGUI levelText;

    private void Start()
    {
        levelText.text = levelNum.ToString();
    }
}
