using System;
using TMPro;
using UnityEngine;

public class ScoreScreen : MonoBehaviour
{
    [SerializeField] private GameObject scorePanel;
    [SerializeField] private TextMeshProUGUI scoreText;
    

    private int score = 99;

    private void Awake()
    {
        //HouseInteractable.OnHouseInteracted += OpenUI;
    }

    private void Start()
    {
        OpenUI();
    }



    public void OnNextButtonClicked()
    {
        CloseUI();
        GameManager.Instance.NextDay();
    }

    public void OnRetryButtonClicked()
    {
        CloseUI();
        GameManager.Instance.RestartDay();
    }

    public void OpenUI()
    {
        scoreText.text = $"You got {score}/3";
        // TO DO - logic check to unlock next button (2/3 or higher)
        scorePanel.SetActive(true);
        UIManager.Instance.SetState(UIState.Score);
    }

    public void CloseUI()
    {
        scorePanel.SetActive(false);
        Time.timeScale = 1f;
        UIManager.Instance.SetState(UIState.None);
    }

}
