using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;

    void Start() {
        pauseMenuUI.SetActive(false);
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.P)) {
            if(UIManager.Instance.currentUIState == UIState.Pause) {
                ResumeGame();
            }
            else {
                if(UIManager.Instance.currentUIState == UIState.None) {
                    PauseGame();
                }
            }
        }
    }

    public void PauseGame() {
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
        UIManager.Instance.SetState(UIState.Pause);
    }

    public void ResumeGame() {
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        UIManager.Instance.SetState(UIState.None);
    }

    public void QuitGame() {
        Debug.Log("quit");
        Time.timeScale = 1f;
        SceneManager.LoadScene(GameManager.Instance.menuScene);
    }
}
