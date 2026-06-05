using System;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class EndScreen : MonoBehaviour
{
    private void Start()
    {
        OpenUI();
    }

    public void OnRestartButtonClicked()
    {
        CloseUI();
        GameManager.Instance.RestartGame();
    }

    public void OpenUI()
    {
        UIManager.Instance.SetState(UIState.Ending);
    }

    public void CloseUI()
    {
        UIManager.Instance.SetState(UIState.None);
    }
}
