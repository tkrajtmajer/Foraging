using UnityEngine;
using UnityEngine.SceneManagement;

public class FeedbackScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject scoreScreen;
    [SerializeField] private GameObject itemFeedbackScreen;
    [SerializeField] private GameObject finalScreen;
    
    void OnEnable() {
        DialogueManager.DialogueEnded += ChangeToFeedback;
        FeedbackUI.ShowFinalScreen += ChangeToFinalScreen;
    }

    void OnDisable() {
        DialogueManager.DialogueEnded -= ChangeToFeedback;
        FeedbackUI.ShowFinalScreen -= ChangeToFinalScreen;
    }

    void ChangeToFeedback() {
        scoreScreen.SetActive(false);
        itemFeedbackScreen.SetActive(true);
    }

    void ChangeToFinalScreen() {
        itemFeedbackScreen.SetActive(false);
        finalScreen.SetActive(true);
    }

    public void OnNextButtonClicked()
    {
        //GameManager.Instance.NextDay();
        ScreenFader.Instance.FadeAndLoadScene(GameManager.Instance.levelSelectSceneIdx);
    }

    public void OnRetryButtonClicked()
    {
        GameManager.Instance.RestartDay();
    }
}
