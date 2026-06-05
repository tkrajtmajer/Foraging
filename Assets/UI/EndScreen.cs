using System;
using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{

    public void OnRestartButtonClicked()
    {
        GameManager.Instance.RestartGame();
    }
}
