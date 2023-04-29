using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AudioCalibrationBehaviour : MonoBehaviour
{
    private float audioCalibrationTimer, metronomeTimer;
    private const float AUDIO_CALIBRATION_INTERVAL = 60.0f, METRONOME_INTERVAL = 0.5f;
    public AudioSource audioSource;

    private bool isTimerDone;

    private List<float> timestamps;
    private List<float> audioLagValuesPerTimestamp;

    public TextMeshProUGUI displayTMP, currentLagTMP;

    public GameObject backButton, acceptButton;

    void Start()
    {
        Initialize();
    }

    private void OnEnable()
    {
        Initialize();
    }

    private void Initialize()
    {
        audioCalibrationTimer = metronomeTimer = 0.0f;

        timestamps = new List<float>();
        audioLagValuesPerTimestamp = new List<float>();

        isTimerDone = false;

        displayTMP.text = "Press Z every time you hear the metronome to calibrate audio lag.\nThis will take about one minute.";
        currentLagTMP.text = "Current audio lag: " + ((int)(1000 * (PlayerPrefs.GetFloat("audio lag", 0.0f)))).ToString() + " ms";

        backButton.SetActive(true);
        acceptButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimerDone == false)
        {
            if (audioCalibrationTimer < AUDIO_CALIBRATION_INTERVAL)
            {
                timestampInput();
                audioCalibrationTimer += Time.deltaTime;
            }
            else
            {
                isTimerDone = true;
                DisplayAudioLag();
            }
        }
    }

    private void timestampInput()
    {
        if (metronomeTimer >= METRONOME_INTERVAL)
        {
            audioSource.Play();
            metronomeTimer -= METRONOME_INTERVAL;
        }
        if (Input.GetKeyDown(KeyCode.Z) == true)
        {
            timestamps.Add(audioCalibrationTimer);
        }

        metronomeTimer += Time.deltaTime;
    }

    private void DisplayAudioLag()
    {
        for (int i = 0; i < timestamps.Count; i++)
        {
            float audioLagValue = timestamps[i] - METRONOME_INTERVAL * (int)(timestamps[i] / METRONOME_INTERVAL); //Calculation of the modulus
            audioLagValue = Mathf.Min(audioLagValue, METRONOME_INTERVAL - audioLagValue); //Value closest to the interval
            audioLagValuesPerTimestamp.Add(audioLagValue);
        }

        float audioLagTotal = 0.0f;
        for (int i = 0; i < audioLagValuesPerTimestamp.Count; i++)
        {
            audioLagTotal += audioLagValuesPerTimestamp[i];
        }
        if (audioLagValuesPerTimestamp.Count != 0)
        {
            PlayerPrefs.SetFloat("audio lag", audioLagTotal / audioLagValuesPerTimestamp.Count);
        }
        else
        {
            PlayerPrefs.SetFloat("audio lag", 0.0f);
        }

        int audioLagInMsRounded = Mathf.RoundToInt(PlayerPrefs.GetFloat("audio lag", 0.0f) * 1000);

        displayTMP.text = "The audio lag has been set to\n" + audioLagInMsRounded.ToString() + " milliseconds";
        currentLagTMP.text = "Current audio lag: " + audioLagInMsRounded.ToString() + " ms";
        backButton.SetActive(false);
        acceptButton.SetActive(true);
    }
}
