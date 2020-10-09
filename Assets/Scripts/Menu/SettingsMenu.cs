using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public AudioMixer audioMixer;
    public Slider volumeEffectsSlider;
    public Slider volumeMusicSlider;
    public TMPro.TextMeshProUGUI volumeEffectText;
    public TMPro.TextMeshProUGUI volumeMusicText;

    public float minVolume = 0.00f;
    public float maxVolume = 100.00f;

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("musicVolume", volume);
    }

    public void SetEffectsVolume(float volume)
    {
        audioMixer.SetFloat("effectsVolume", volume);
    }



    void Update() {
        // Reflect Value of Audio to Sliders
        float musicVolume, effectVolume;
        audioMixer.GetFloat("musicVolume", out musicVolume);
        audioMixer.GetFloat("effectsVolume", out effectVolume);
        volumeEffectsSlider.value = effectVolume;
        volumeMusicSlider.value = musicVolume;

        // Remap Values to Percents
        musicVolume = 1f - (musicVolume / -80f);
        effectVolume = 1f - (effectVolume / -80f);
        volumeMusicText.text = $"{musicVolume*100:N0}%";
        volumeEffectText.text = $"{effectVolume*100:N0}%";
    }
}
