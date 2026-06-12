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
            Destroy(this);
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
        if (scene.buildIndex == GameManager.Instance.finalMainSceneIdx && currentMusic == chillMusic)
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
        source.resource = (UnityEngine.Audio.AudioResource)currentMusic;
        source.pitch = 1;
        source.Play();
    }

    public void EndOfDayMusic(float duration)
    {
        StartCoroutine(SlowMusicDown(duration));
    }

    private IEnumerator SlowMusicDown(float duration)
    {
        for (float i  = 0; i < duration; i += Time.deltaTime)
        {
            source.pitch = 1 * (1 - i / duration) + 0.8f * i / duration;
            yield return null;
        }
        source.pitch = 0.8f;
    }

    public void TransitionMusicEndOfDay()
    {
        StartCoroutine(FadeMusicOut(fadeDuration, chillMusic));
    }

    public void TransitionMusicStartOfDay()
    {
        StartCoroutine(FadeMusicOut(fadeDuration, hypeMusic));
    }

    private IEnumerator FadeMusicOut(float fadeDuration, AudioClip music) 
    {
        float initialVolume = PlayerPrefs.GetFloat("musicVolume");
        for (float i = 0; i < fadeDuration; i += Time.deltaTime)
        {
            source.volume = initialVolume * (1 - i / fadeDuration);
            yield return null;
        }

        source.Pause();

        currentMusic = music;
        ChangeMusic();
        source.Play();
        StartCoroutine(FadeMusicIn(fadeDuration));
    }

    private IEnumerator FadeMusicOut(float fadeDuration)
    {
        float initialVolume = PlayerPrefs.GetFloat("musicVolume");
        for (float i = 0; i < fadeDuration; i += Time.deltaTime)
        {
            source.volume = initialVolume * (1 - i / fadeDuration);
            yield return null;
        }
    }

    private IEnumerator FadeMusicIn(float fadeDuration)
    {
        float finalVolume = PlayerPrefs.GetFloat("musicVolume");
        for (float i = 0; i < fadeDuration; i += Time.deltaTime)
        {
            source.volume = finalVolume * i / fadeDuration;
            yield return null;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            StartCoroutine(FadeMusicIn(fadeDuration));
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            StartCoroutine(FadeMusicOut(fadeDuration));
        }
    }
}
