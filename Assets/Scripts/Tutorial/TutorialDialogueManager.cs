using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Collections;
using System;

public enum TutorialDialogueSequence
{
    Intro,
    ItemList,
    Journal,
    Interactable,
    Interact,
    Empty,
    Inventory,
    Map,
    EmptyAgain,
    Home,
}

public class TutorialDialogueManager : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] TMP_Text continueText;
    [SerializeField] float continueFadeTime;
    private Queue<string> sentences = new Queue<string>();

    [SerializeField] TutorialManager tutorialManager;

    public static event Action DialogueEnded;

    int currentDialogueIdx { get => tutorialManager.currentDialogueIdx; }

    public void StartDialogue(DialogueData dialogueData)
    {
        sentences.Clear();

        foreach (string sentence in dialogueData.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
        
    }

    public void DisplayNextSentence()
    {
        continueText.gameObject.SetActive(false);
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        text.text = "";
        string key = "";
        foreach (char letter in sentence.ToCharArray())
        {

            if (letter == '<') key = "";
            key += letter;
            if (key[0] == '<')
            {
                if (key[key.Length - 1] != '>')
                {
                    goto WriteKey;
                }
                else
                {
                    text.text += key;
                    key = "";
                    goto ContinueWritting;
                }
            }

            text.text += letter;

        ContinueWritting:
            yield return new WaitForSecondsRealtime(0.03f);
            if (letter == '.') yield return new WaitForSecondsRealtime(0.5f);
        WriteKey:
            continue;
        }

        yield return new WaitForSecondsRealtime(1.5f);
        yield return (currentDialogueIdx != (int)TutorialDialogueSequence.Journal) ? StartCoroutine(PressSpace()) : null;
    }

    public IEnumerator PressSpace()
    {
        continueText.gameObject.SetActive(true);
        continueText.alpha = 0.0f;
        for (float i = 0; i < continueFadeTime; i += Time.unscaledDeltaTime)
        {
            continueText.alpha += i / continueFadeTime;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        DialogueEnded?.Invoke();

        switch (currentDialogueIdx)
        {
            // Item list
            case ((int)TutorialDialogueSequence.ItemList):
                StartCoroutine(ShowListSequence());
                tutorialManager.arrow.SetActive(true);
                break;

            // Journal
            case ((int)TutorialDialogueSequence.Journal):
                ShowJournalSequence();
                break;
            
            // Empty
            case ((int)TutorialDialogueSequence.Empty):
                tutorialManager.CloseTutorialUI();
                break;
            
            case ((int)TutorialDialogueSequence.EmptyAgain):
                tutorialManager.CloseTutorialUI();
                break;

            // Inventory
            case ((int)TutorialDialogueSequence.Inventory):
                ShowInventorySequence();
                break;

            // Map
            case ((int)TutorialDialogueSequence.Map):
                ShowMapSequence();
                break;

            // Home
            case ((int)TutorialDialogueSequence.Home):
                ShowHomeSequence();
                break;
            default:
                break;
        }
    }

    private IEnumerator ShowListSequence()
    {
        yield return new WaitForSecondsRealtime(3.0f);

        tutorialManager.ShowDialogueInactive();
        yield return null;
    }

    private void ShowJournalSequence()
    {
        tutorialManager.ShowDialogueInactive();
    }

    public void ShowInteractableSequence()
    {
        tutorialManager.ShowDialogueActive();
    }

    public void ShowInteractSequence()
    {
        tutorialManager.ShowDialogueActive();
    }

    public void ShowInventorySequence()
    {
        tutorialManager.tutorialContainer.GetComponent<RectTransform>().localPosition = new Vector3(0, 790, 0);
        tutorialManager.ShowDialogueActive();
    }

    public void ShowMapSequence()
    {
        tutorialManager.tutorialContainer.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        tutorialManager.ShowDialogueActive();
    }

    public void ShowHomeSequence()
    {
        tutorialManager.ShowDialogueActive();
        StartCoroutine(LastDialogue());
    }

    public IEnumerator LastDialogue()
    {
        yield return new WaitForSecondsRealtime(4.0f);

        DisplayNextSentence();

        yield return new WaitForSecondsRealtime(3.0f);
        tutorialManager.CloseTutorialUI();
        yield return null;
    }
}

