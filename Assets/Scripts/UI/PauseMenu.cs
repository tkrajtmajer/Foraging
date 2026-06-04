using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    bool isPaused;

    void Start() {
        pauseMenuUI.SetActive(false);
        isPaused = false;
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.P)) {
            if(isPaused) {
                ResumeGame();
            }
            else {
                PauseGame();
            }
        }
    }

    public void PauseGame() {
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
        isPaused = true;
    }

    public void ResumeGame() {
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        isPaused = false;
    }

    public void QuitGame() {
        Debug.Log("quit");
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
