using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenFader : MonoBehaviour
{
    public static ScreenFader Instance { get; private set; }

    [Header("References")]
    [SerializeField] private CanvasGroup faderCanvasGroup;

    [Header("Settings")]
    [SerializeField] private float fadeDuration = 0.5f;

    private void Awake()
    {
        // (singleton)
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// Fades to black, run code, then fades back to clear.
    /// midFadeAction: code to run when the screen is black
    public void FadeAndExecute(Action midFadeAction)
    {
        StartCoroutine(FadeRoutine(midFadeAction));
    }

    private IEnumerator FadeRoutine(Action midFadeAction)
    {
        faderCanvasGroup.blocksRaycasts = true; // block UI clicks

        // fade to black
        float time = 0;
        while (time < fadeDuration)
        {
            //  unscaledDeltaTime so it works when game paused (if Time.timeScale is 0)
            time += Time.unscaledDeltaTime;
            faderCanvasGroup.alpha = Mathf.Lerp(0, 1, time / fadeDuration);
            yield return null;
        }
        faderCanvasGroup.alpha = 1;

        // run code (e.g., teleport player, open UI, etc.)
        midFadeAction?.Invoke();

        // wait one frame
        yield return new WaitForEndOfFrame();

        // fade to transparent
        time = 0;
        while (time < fadeDuration)
        {
            time += Time.unscaledDeltaTime;
            faderCanvasGroup.alpha = Mathf.Lerp(1, 0, time / fadeDuration);
            yield return null;
        }
        faderCanvasGroup.alpha = 0;

        // unblock UI clicks
        faderCanvasGroup.blocksRaycasts = false;
    }

    /// Fades to black, load and change scene, then fades back to clear.
    public void FadeAndLoadScene(string sceneName)
    {
        StartCoroutine(FadeAndLoadRoutine(sceneName));
    }

    private IEnumerator FadeAndLoadRoutine(string sceneName)
    {
        faderCanvasGroup.blocksRaycasts = true;

        float time = 0;
        while (time < fadeDuration)
        {
            time += Time.unscaledDeltaTime;
            faderCanvasGroup.alpha = Mathf.Lerp(0, 1, time / fadeDuration);
            yield return null;
        }
        faderCanvasGroup.alpha = 1;

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // ? prevent the scene from activating instantly 
        // asyncLoad.allowSceneActivation = false; 

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        yield return new WaitForEndOfFrame();

        time = 0;
        while (time < fadeDuration)
        {
            time += Time.unscaledDeltaTime;
            faderCanvasGroup.alpha = Mathf.Lerp(1, 0, time / fadeDuration);
            yield return null;
        }
        faderCanvasGroup.alpha = 0;

        faderCanvasGroup.blocksRaycasts = false;
        // asyncLoad.allowSceneActivation = false; 
    }
}