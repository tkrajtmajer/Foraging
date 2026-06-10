using System;
using TMPro;
using UnityEngine;

public class ScoreScreen : MonoBehaviour
{
    [SerializeField] private GameObject scorePanel;
    [SerializeField] private TextMeshProUGUI scoreText;

    //[SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private DialogueData dialogueGood;
    [SerializeField] private DialogueData dialogueBad;

    public static event Action<DialogueData> StartDialogue;

    public int tempScore;
    public int tempNrItems;

    private void Awake()
    {
        //HouseInteractable.OnHouseInteracted += OpenUI;
    }

    private void Start()
    {
        //scorePanel.SetActive(true);
        OpenUI();
    }

    public void OpenUI()
    {
        int nrItems, score = 0;

        if (GameManager.Instance == null) {
            nrItems = tempNrItems;
            score = tempScore;
        }
        else {
            nrItems = GameManager.Instance.currentRecipe.forageablesInRecipe.Count;
            score = GameManager.Instance.score;
        }

        scoreText.text = $"You got {score}/{nrItems}";

        DialogueData chosenDialogue;

        //Debug.Log(score*1.0f/nrItems);
        if(score*1.0f / nrItems > 0.5) {
            chosenDialogue = dialogueGood;
        }
        else chosenDialogue = dialogueBad;

        StartDialogue?.Invoke(chosenDialogue);
    }

}
