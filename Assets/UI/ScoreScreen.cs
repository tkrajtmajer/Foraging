using System;
using TMPro;
using UnityEngine;

public class ScoreScreen : MonoBehaviour
{
    [SerializeField] private GameObject scorePanel;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Awake()
    {
        HouseInteractable.OnHouseInteracted += OpenUI;
    }

    private void Start()
    {
        scorePanel.SetActive(false);
    }



    public void OnNextButtonClicked()
    {
        CloseUI();
    }

    public void OnRetryButtonClicked()
    {
        CloseUI();
    }

    public void OpenUI(int score)
    {
        scoreText.text = $"You got {score}/3";
        scorePanel.SetActive(true);
    }

    public void CloseUI()
    {
        scorePanel.SetActive(false);
        Time.timeScale = 1f;

        UIManager.Instance.SetState(UIState.None);
    }

}
