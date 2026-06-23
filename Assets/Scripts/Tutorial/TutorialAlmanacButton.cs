using UnityEngine;
using UnityEngine.UI;

public class TutorialAlmanacButton : MonoBehaviour
{
    [SerializeField] GameObject items;
    [SerializeField] TutorialDialogueManager tutorial;
    [SerializeField] TutorialManager tutorialManager;
    [SerializeField] Button exitButton;
    [SerializeField] Button collectButton;
    [SerializeField] Button leaveButton;
    AlmanacItemUI item;
    Button button;

    void Start()
    {
        item = items.GetComponentInChildren<AlmanacItemUI>();
        button = item.GetComponentInChildren<Button>();
        button.onClick.AddListener(item.SelectItem);
        button.onClick.AddListener(tutorial.DisplayNextSentence);
        button.onClick.AddListener(RemoveClickedButton);

        collectButton.onClick.AddListener(OnCollectButtonClicked);
        leaveButton.onClick.AddListener(OnLeaveButtonClicked);
    }

    public void OnExitButtonClicked()
    {
        exitButton.onClick.RemoveListener(tutorial.DisplayNextSentence);
    }

    public void RemoveClickedButton()
    {
        button.onClick.RemoveListener(tutorial.DisplayNextSentence);
    }

    public void OnCollectButtonClicked()
    {
        collectButton.onClick.RemoveListener(tutorial.DisplayNextSentence);
    }

    public void OnLeaveButtonClicked()
    {
        leaveButton.onClick.RemoveListener(tutorialManager.CloseTutorialUI);
    }
}
