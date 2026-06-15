using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    AudioSource music;
    [SerializeField] UnityEngine.UI.Slider volumeSlider;

    private void Awake()
    {
        music = MusicManager.Instance.GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }
    public void SetVolume(float volume) {
        //Debug.Log("volume " + volume);
        PlayerPrefs.SetFloat("musicVolume", volume);
        music.volume = volume;
    }
}
