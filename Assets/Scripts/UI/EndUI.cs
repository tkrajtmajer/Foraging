using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private DialogueData introDialogue;
    public static event Action<DialogueData> StartIntroDialogue;

    IEnumerator Start()
    {
        dialogueBox.SetActive(false);
        nextButton.SetActive(false);

        yield return StartCoroutine(WaitForIntro());

        dialogueBox.SetActive(true);
        nextButton.SetActive(true);

        StartIntroDialogue?.Invoke(introDialogue);    
    }

    void OnEnable() {
        DialogueManager.DialogueEnded += TransitionScene;
    }
    void OnDisable() {
        DialogueManager.DialogueEnded -= TransitionScene;
    }

    IEnumerator WaitForIntro() {
        // do sth else?

        yield return new WaitForSeconds(1f);
    }

    void TransitionScene() {
        ScreenFader.Instance.FadeAndLoadScene(GameManager.Instance.menuScene);
    }
}
