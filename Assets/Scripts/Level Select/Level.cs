using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    [SerializeField] public LevelData levelData;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] public Button button;
    bool isSelected;

    private void Start()
    {
        levelText.text = levelData.levelNumber.ToString();

        float posInLine = 310 * (levelData.levelNumber - 1);
        transform.localPosition = new Vector3(posInLine, 0, 0);
    }

    public void SelectLevel()
    {
        isSelected = true;
        //float stdScale = LevelManager.Instance.stdScale;
        //transform.localScale = new Vector3(stdScale, stdScale, stdScale);
    }
}
