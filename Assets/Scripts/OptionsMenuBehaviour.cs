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

    private void Start()
    {
        volume_value_TMP_TMPUGUI = volume_value_TMP.GetComponent<TextMeshProUGUI>();
        volume_slider_slider = volume_slider.GetComponent<Slider>();
        volume_value = volume_slider_slider.value;
        volume_value_displayed = (int)(volume_value * 100);
    }
    private void Update()
    {
        volume_value = volume_slider_slider.value;
        volume_value_displayed = (int)(volume_value * 100);
        volume_value_TMP_TMPUGUI.text = volume_value_displayed.ToString();
    }
}
