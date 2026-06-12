using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
    public int maxLevel = 3;
    [SerializeField] GameObject levelsContainer;
    //[SerializeField] GameObject levelPrefab;
    [SerializeField] int levelMoveAmount = 310;

    public static event UnityAction<int> LevelSelected;

    private void Start()
    {
        MoveLevels(0);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += SetLevelState;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SetLevelState;
    }

    private void SetLevelState(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == GameManager.Instance.menuScene + 1)
        {
            levelsContainer.transform.localPosition = new Vector3((LevelManager.Instance.currentLevel - 1) * -levelMoveAmount, levelsContainer.transform.localPosition.y, 0);
            MoveLevels(0);
        }
    }

    public void MoveLevels(int side)
    {
        // -1 -> left, 1 -> right
        // Call MoveLevels(0) to set up correct opacity and button state for each of the level buttons
        if (LevelManager.Instance.currentLevel == 1 & side == 1) return;
        if (LevelManager.Instance.currentLevel == maxLevel & side == -1) return;
        LevelManager.Instance.currentLevel -= side;
        levelsContainer.transform.localPosition += side * new Vector3(levelMoveAmount, 0, 0);
        foreach (Level level in levelsContainer.GetComponentsInChildren<Level>())
        {
            float alpha;
            if (level.levelData.levelNumber == LevelManager.Instance.currentLevel)
            {
                alpha = 98;
                level.button.interactable = true;
            }
            else
            {
                alpha = 49;
                level.button.interactable = false;
            }
            foreach (UnityEngine.UI.Image img in level.GetComponentsInChildren<UnityEngine.UI.Image>())
            {
                img.color = new Color(img.color.r, img.color.g, img.color.b, alpha / 100);
            }
            TextMeshProUGUI text = level.GetComponentInChildren<TextMeshProUGUI>();
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha / 100);
        }
    }

    public void OnLevelSelected(int level)
    {
        LevelSelected?.Invoke(level);
    }

}
