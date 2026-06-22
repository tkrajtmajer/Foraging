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
    [SerializeField] GameObject tutorialContainer;
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
    }

    private IEnumerator Start()
    {
        yield return new WaitForSecondsRealtime(2.0f);

        ShowDialogueInactive();
    }

    private void OnEnable()
    {
        TutorialDialogueManager.DialogueEnded += OnDialogueEnded;
    }

    private void OnDisable()
    {
        TutorialDialogueManager.DialogueEnded -= OnDialogueEnded;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && currentDialogueIdx != 3)
        {
            dialogueManager.DisplayNextSentence();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            ShowDialogueInactive();
        }
        if (Input.GetKeyDown(KeyCode.J) && currentDialogueIdx == 3 && tutorialContainer.activeInHierarchy)
        {
            //tutorialContainer.SetActive(false);
            dialogueManager.DisplayNextSentence();
            
            //StartCoroutine();
        }
    }

    public void ShowDialogueInactive()
    {
        tutorialContainer.SetActive(true);

        darkFilter.SetActive(true);
        //player.playerAnimator.SetFloat("speed", 0);
        player.enabled = false;
        Time.timeScale = 0.0f;

        dialogueManager.StartDialogue(dialogueList[currentDialogueIdx++]);
    }

    public void ShowDialogueActive()
    {
        tutorialContainer.SetActive(true);
        dialogueManager.StartDialogue(dialogueList[currentDialogueIdx++]);
    }

    public void OnDialogueEnded()
    {
        darkFilter.SetActive(false);
        player.enabled = true;
        Time.timeScale = 1.0f;

        arrow.SetActive(false);

        tutorialContainer.SetActive(false);
    }
}
