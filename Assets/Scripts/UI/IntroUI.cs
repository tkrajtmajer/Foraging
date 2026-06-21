using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Collections;

public class IntroUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private DialogueData introDialogue;
    [SerializeField] private DialogueData dialogueWife;
    public static event Action<DialogueData> StartIntroDialogue;

    int currentDialogue;

    IEnumerator Start()
    {
        dialogueBox.SetActive(false);
        nextButton.SetActive(false);

        yield return StartCoroutine(WaitForIntro());

        dialogueBox.SetActive(true);
        nextButton.SetActive(true);

        currentDialogue = 0;

        StartIntroDialogue?.Invoke(introDialogue);        
    }

    void OnEnable() {
        DialogueManager.DialogueEnded += TransitionDialogue;
    }
    void OnDisable() {
        DialogueManager.DialogueEnded -= TransitionDialogue;
    }

    IEnumerator WaitForIntro() {
        // do sth else?

        yield return new WaitForSeconds(2f);
    }

    void TransitionDialogue() {
        currentDialogue++;

        if (currentDialogue == 1) StartIntroDialogue?.Invoke(dialogueWife);
        else TransitionScene();
    }

    void TransitionScene() {
        ScreenFader.Instance.FadeAndLoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }    
}
