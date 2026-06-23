using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using TMPro;
using System.Collections;
using System;
using System.Runtime.InteropServices.WindowsRuntime;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] LevelData tutorialData;
    [SerializeField] public GameObject tutorialContainer;
    [SerializeField] GameObject darkFilter;

    [SerializeField] PlayerController player;

    [SerializeField] TutorialDialogueManager dialogueManager;
    [SerializeField] public List<DialogueData> dialogueList;

    [Header("Item Databases")]
    [SerializeField] ItemDatabase tutorialItemsDatabase;
    [SerializeField] ItemDatabase trueItemsDatabase;

    [Header("Item List")]
    [SerializeField] public GameObject arrow;

    public int currentDialogueIdx = 0;
    
    private void Awake()
    {
        GameManager.Instance.currentRecipe = tutorialData.recipe;
        GameManager.Instance.itemDatabase = tutorialItemsDatabase;
    }

    private void OnDestroy()
    {
        GameManager.Instance.itemDatabase = trueItemsDatabase;
        GameManager.Instance.hasDoneTutorial = true;
    }

    private IEnumerator Start()
    {
        yield return new WaitForSecondsRealtime(2.0f);

        ShowDialogueInactive();
    }

    private void OnEnable()
    {
        TutorialDialogueManager.DialogueEnded += OnDialogueEnded;
        PlayerInteractor.FirstInteract += DoFirstInteractable;
        PlayerInteractor.FirstInteracted += DoFirstInteraction;

        MapManager.OpenedMap += DoMapSequence;
        MapManager.ClosedMap += FinishMapSequence;
    }

    private void OnDisable()
    {
        TutorialDialogueManager.DialogueEnded -= OnDialogueEnded;
        PlayerInteractor.FirstInteract -= DoFirstInteractable;
        PlayerInteractor.FirstInteracted -= DoFirstInteraction;

        MapManager.OpenedMap -= DoMapSequence;
        MapManager.ClosedMap -= FinishMapSequence;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return))
        {
            dialogueManager.DisplayNextSentence();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            ShowDialogueInactive();
        }
        if (Input.GetKeyDown(KeyCode.J) && currentDialogueIdx == (int)TutorialDialogueSequence.Journal && tutorialContainer.activeInHierarchy)
        {
            //tutorialContainer.SetActive(false);
            dialogueManager.DisplayNextSentence();
            
            //StartCoroutine();
        }
    }

    public void CloseTutorialUI()
    {
        darkFilter.SetActive(false);
        player.enabled = true;
        Time.timeScale = 1.0f;

        arrow.SetActive(false);

        tutorialContainer.SetActive(false);
    }

    public void ShowDialogueInactive()
    {
        tutorialContainer.SetActive(true);

        darkFilter.SetActive(true);
        //player.playerAnimator.SetFloat("speed", 0);
        player.enabled = false;
        Time.timeScale = 0.0f;

        dialogueManager.StartDialogue(dialogueList[currentDialogueIdx]);
    }

    public void ShowDialogueActive()
    {
        tutorialContainer.SetActive(true);
        dialogueManager.StartDialogue(dialogueList[currentDialogueIdx]);
    }

    public void OnDialogueEnded()
    {
        CloseTutorialUI();
        ++currentDialogueIdx;
    }

    public void DoFirstInteractable()
    {
        PlayerInteractor.FirstInteract -= DoFirstInteractable;
        dialogueManager.ShowInteractableSequence();
    }

    public void DoFirstInteraction()
    {
        PlayerInteractor.FirstInteracted -= DoFirstInteraction;
        OnDialogueEnded();
        dialogueManager.ShowInteractSequence();
    }

    public void DoMapSequence() 
    {
        MapManager.OpenedMap -= DoMapSequence;
        dialogueManager.DisplayNextSentence();
    }
    
    public void FinishMapSequence()
    {
        MapManager.ClosedMap -= FinishMapSequence;
        dialogueManager.EndDialogue();
    }
}
