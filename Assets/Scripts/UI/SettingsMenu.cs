using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] AudioSource music;
    public void SetVolume(float volume) {
        Debug.Log("volume " + volume);
        PlayerPrefs.SetFloat("musicVolume", volume);
        music.volume = volume;
    }
}
