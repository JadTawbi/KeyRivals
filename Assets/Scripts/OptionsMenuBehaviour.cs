using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OptionsMenuBehaviour : MonoBehaviour
{
    public GameObject volume_value_TMP, volume_slider;
    private TextMeshProUGUI volume_value_TMP_TMPUGUI;
    private Slider volume_slider_slider;
    public List<AudioSource> audio_sources;

    public static readonly float DEFAULT_VOLUME = 0.5f;

    private void OnEnable()
    {
        volume_slider_slider = volume_slider.GetComponent<Slider>();
        volume_slider_slider.value = PlayerPrefs.GetFloat("volume", DEFAULT_VOLUME);
    }
    private void OnDisable()
    {
        PlayerPrefs.Save();
    }

    private void Start()
    {
        volume_value_TMP_TMPUGUI = volume_value_TMP.GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        PlayerPrefs.SetFloat("volume", volume_slider_slider.value);
        checkVolume();
        volume_value_TMP_TMPUGUI.text = ((int)(PlayerPrefs.GetFloat("volume", DEFAULT_VOLUME) * 100)).ToString();
    }
    public void checkVolume()
    {
        foreach (AudioSource audio_source in audio_sources)
        {
            if (audio_source.volume != PlayerPrefs.GetFloat("volume", DEFAULT_VOLUME))
            {
                audio_source.volume = PlayerPrefs.GetFloat("volume", DEFAULT_VOLUME);
            }
        }
    }

    public void pauseSound()
    {
        foreach (AudioSource audio_source in audio_sources)
        {
            audio_source.Pause();
        }
    }

    public void unPauseSound()
    {
        foreach (AudioSource audio_source in audio_sources)
        {
            audio_source.UnPause();
        }
    }
}
