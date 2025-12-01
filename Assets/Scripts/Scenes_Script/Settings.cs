using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    /*
    public Slider VolumeSlider;
    public Text VolumeText;

    void Start()
    {
        // Initialize slider value
        float initialVolume = PlayerPrefs.GetFloat("MasterVolume", 1f); // default volume 1.0
        VolumeSlider.value = initialVolume;
        SetVolume(initialVolume);

        VolumeSlider.onValueChanged.AddListener(SetVolume);

    }

    public void SetVolume(float value)
    {
        AudioListener.volume = value; // Set master volume
        VolumeText.text = "Volume: " + Mathf.RoundToInt(value * 100) + "%";
        PlayerPrefs.SetFloat("MasterVolume", value);
    }
    */
}