using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettingsManager : MonoBehaviour
{
    public AudioMixer audioMixer;

    [Header("UI Sliders")]
    public Slider masterSlider;
    public Slider bgmSlider;
    public Slider sfxSlider;

    private void Start()
    {
        if (masterSlider != null)
        {
            masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0.75f);
            SetMasterVolume(masterSlider.value);
            masterSlider.onValueChanged.AddListener(SetMasterVolume);
        }

        if (bgmSlider != null)
        {
            bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 0.75f);
            SetBGMVolume(bgmSlider.value);
            bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        }

        if (sfxSlider != null)
        {
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
            SetSFXVolume(sfxSlider.value);
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        }
    }

    public void SetMasterVolume(float volume)
    {
        float db = volume > 0.001f ? Mathf.Log10(volume) * 20f : -80f;
        audioMixer.SetFloat("Master", db);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void SetBGMVolume(float volume)
    {
        float db = volume > 0.001f ? Mathf.Log10(volume) * 20f : -80f;
        audioMixer.SetFloat("BGM", db);
        PlayerPrefs.SetFloat("BGMVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        float db = volume > 0.001f ? Mathf.Log10(volume) * 20f : -80f;
        audioMixer.SetFloat("SFX", db);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }
}