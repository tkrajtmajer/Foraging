using System.Collections;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioClip chillMusic;
    [SerializeField] AudioClip hypeMusic;
    [SerializeField] private AudioClip currentMusic;
    private AudioSource source;

    [SerializeField] private float fadeDuration = 0.5f;


    public static MusicManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        source = transform.GetComponent<AudioSource>();
        currentMusic = (AudioClip)source.resource;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += SetSceneMusic;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SetSceneMusic;
    }

    private void SetSceneMusic(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == GameManager.Instance.finalMainSceneIdx && currentMusic != hypeMusic)
        {
            currentMusic = hypeMusic;
            ChangeMusic();
        } 
        else if (scene.buildIndex <= GameManager.Instance.menuScene + 1 && currentMusic != chillMusic)
        {
            currentMusic = chillMusic;
            ChangeMusic();
        }
    }

    private void ChangeMusic()
    {
        source.Pause();
        source.resource = (UnityEngine.Audio.AudioResource)currentMusic;
        source.Play();
    }

    public void TransitionMusicEndOfDay()
    {
        StartCoroutine(FadeMusicOutPartially(fadeDuration));
    }

    public void TransitionMusicStartOfDay()
    {
        StartCoroutine(FadeMusicInPartially(fadeDuration));
    }

    private IEnumerator FadeMusicOutPartially(float fadeDuration)
    {
        float initialVolume = PlayerPrefs.GetFloat("musicVolume");
        for (float i = 0; i < fadeDuration; i += Time.deltaTime)
        {
            source.volume = initialVolume * (1 - i / (fadeDuration * 1.2f));
            yield return null;
        }
    }

    private IEnumerator FadeMusicInPartially(float fadeDuration)
    {
        float finalVolume = PlayerPrefs.GetFloat("musicVolume");
        for (float i = 0; i < fadeDuration; i += Time.deltaTime)
        {
            source.volume = finalVolume * i / (fadeDuration * 1.2f);
            yield return null;
        }
        source.volume = finalVolume;
    }
}
