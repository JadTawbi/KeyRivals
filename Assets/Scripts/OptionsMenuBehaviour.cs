using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OptionsMenuBehaviour : MonoBehaviour
{
    public GameObject volume_value_TMP, volume_slider;
    private TextMeshProUGUI volume_value_TMP_TMPUGUI;
    public static float volume_value;
    private static int volume_value_displayed;
    private Slider volume_slider_slider;
    public List<AudioSource> audio_sources;
    [System.NonSerialized]
    public const float DEFAULT_VOLUME = 0.5f;

    private void OnEnable()
    {
        volume_slider_slider = volume_slider.GetComponent<Slider>();
        volume_slider_slider.value = PlayerPrefs.GetFloat("volume", DEFAULT_VOLUME);
    }
    private void Start()
    {
        volume_value_TMP_TMPUGUI = volume_value_TMP.GetComponent<TextMeshProUGUI>();
        volume_value_displayed = (int)(volume_value * 100);
    }
    private void Update()
    {
        PlayerPrefs.SetFloat("volume", volume_slider_slider.value);
        volume_value_displayed = (int)(PlayerPrefs.GetFloat("volume") * 100);
        volume_value_TMP_TMPUGUI.text = volume_value_displayed.ToString();
        checkVolume();
    }
    public void checkVolume()
    {
        foreach(AudioSource audioSource in audio_sources)
        if (audioSource.volume != OptionsMenuBehaviour.volume_value)
        {
            audioSource.volume = OptionsMenuBehaviour.volume_value;
        }
    }
}
