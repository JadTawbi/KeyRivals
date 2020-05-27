using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AudioCalibrationBehaviour : MonoBehaviour
{
    private float audio_calibration_timer, metronome_timer;
    private const float AUDIO_CALIBRATION_INTERVAL = 60.0f, METRONOME_INTERVAL = 0.5f;
    public AudioSource audio_source;

    private bool timer_done;

    private List<float> timestamps;
    private List<float> audio_lag_values_per_timestamp;
    private static float final_audio_lag;

    public TextMeshProUGUI display_TMP;

    public GameObject back_button, accept_button;


    // Start is called before the first frame update
    void Start()
    {
        initialize();
    }

    private void OnEnable()
    {
        initialize();
    }

    private void initialize()
    {
        audio_calibration_timer = metronome_timer = 0.0f;

        timestamps = new List<float>();
        audio_lag_values_per_timestamp = new List<float>();

        timer_done = false;

        display_TMP.text = "Press Z every time you hear the metronome to calibrate audio lag.\nThis will take about one minute.";

        back_button.SetActive(true);
        accept_button.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer_done == false)
        {
            if (audio_calibration_timer < AUDIO_CALIBRATION_INTERVAL)
            {
                timestampInput();
                audio_calibration_timer += Time.deltaTime;
            }
            else
            {
                timer_done = true;
                displayAudioLag();
            }
        }
    }

    private void timestampInput()
    {
        if (metronome_timer >= METRONOME_INTERVAL)
        {
            audio_source.Play();
            metronome_timer -= METRONOME_INTERVAL;
        }
        if (Input.GetKeyDown(KeyCode.Z) == true)
        {
            timestamps.Add(audio_calibration_timer);
        }

        metronome_timer += Time.deltaTime;
    }

    private void displayAudioLag()
    {
        for (int i = 0; i < timestamps.Count; i++)
        {
            float audio_lag_value = timestamps[i] - METRONOME_INTERVAL * (int)(timestamps[i] / METRONOME_INTERVAL); //Calculation of the modulus
            audio_lag_value = Mathf.Min(audio_lag_value, METRONOME_INTERVAL - audio_lag_value); //Value closest to the interval
            audio_lag_values_per_timestamp.Add(audio_lag_value);
        }

        float audio_lag_total = 0.0f;
        for (int i = 0; i < audio_lag_values_per_timestamp.Count; i++)
        {
            audio_lag_total += audio_lag_values_per_timestamp[i];
        }
        if (audio_lag_values_per_timestamp.Count != 0)
        {
            final_audio_lag = audio_lag_total / audio_lag_values_per_timestamp.Count;
        }
        else
        {
            final_audio_lag = 0.0f;
        }

        MainMenuBehaviour.audio_lag = final_audio_lag;

        int audio_lag_in_ms_rounded = Mathf.RoundToInt(final_audio_lag * 1000);

        display_TMP.text = "The audio lag has been set to\n" + audio_lag_in_ms_rounded.ToString() + " milliseconds";
        back_button.SetActive(false);
        accept_button.SetActive(true);
    }
}
