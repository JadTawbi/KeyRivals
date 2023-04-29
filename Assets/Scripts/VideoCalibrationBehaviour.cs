using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VideoCalibrationBehaviour : MonoBehaviour
{
    private float videoCalibrationTimer, weaponGrowTimer;
    private const float VIDEO_CALIBRATION_INTERVAL = 60.0f, WEAPON_GROW_INTERVAL = 1.0f;
    public GameObject weaponRed, weaponGreen, weaponBlue, weaponYellow;

    private bool isTimerDone;

    private List<float> timestamps;
    private List<float> videoLagValuesPerTimestamp;

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
        videoCalibrationTimer = weaponGrowTimer = 0.0f;

        timestamps = new List<float>();
        videoLagValuesPerTimestamp = new List<float>();

        isTimerDone = false;

        displayTMP.text = "Press Z every time the colored circles reach the edge of the circumference to calibrate video lag.\nThis will take about one minute.";
        currentLagTMP.text = "Current video lag: " + ((int)(1000 * PlayerPrefs.GetFloat("video lag", 0.0f))).ToString() + " ms";

        backButton.SetActive(true);
        acceptButton.SetActive(false);
    }

    void Update()
    {
        if (isTimerDone == false)
        {
            if (videoCalibrationTimer < VIDEO_CALIBRATION_INTERVAL)
            {
                TimestampInput();
                videoCalibrationTimer += Time.deltaTime;
            }
            else
            {
                isTimerDone = true;
                DisplayVideoLag();
            }
        }
    }

    private void TimestampInput()
    {
        if (weaponGrowTimer >= WEAPON_GROW_INTERVAL)
        {
            weaponRed.transform.localScale = Vector3.zero;
            weaponGreen.transform.localScale = Vector3.zero;
            weaponBlue.transform.localScale = Vector3.zero;
            weaponYellow.transform.localScale = Vector3.zero;

            weaponGrowTimer -= WEAPON_GROW_INTERVAL;
        }
        else
        {
            weaponRed.transform.localScale = new Vector3(weaponGrowTimer / WEAPON_GROW_INTERVAL, weaponGrowTimer / WEAPON_GROW_INTERVAL, 0.0f);
            weaponGreen.transform.localScale = new Vector3(weaponGrowTimer / WEAPON_GROW_INTERVAL, weaponGrowTimer / WEAPON_GROW_INTERVAL, 0.0f);
            weaponBlue.transform.localScale = new Vector3(weaponGrowTimer / WEAPON_GROW_INTERVAL, weaponGrowTimer / WEAPON_GROW_INTERVAL, 0.0f);
            weaponYellow.transform.localScale = new Vector3(weaponGrowTimer / WEAPON_GROW_INTERVAL, weaponGrowTimer / WEAPON_GROW_INTERVAL, 0.0f);
        }

        if (Input.GetKeyDown(KeyCode.Z) == true)
        {
            timestamps.Add(videoCalibrationTimer);
        }

        weaponGrowTimer += Time.deltaTime;
    }

    private void DisplayVideoLag()
    {
        for (int i = 0; i < timestamps.Count; i++)
        {
            float videoLagValue = timestamps[i] - WEAPON_GROW_INTERVAL * (int)(timestamps[i] / WEAPON_GROW_INTERVAL);
            videoLagValue = Mathf.Min(videoLagValue, WEAPON_GROW_INTERVAL - videoLagValue);
            videoLagValuesPerTimestamp.Add(videoLagValue);
        }

        float videoLagTotal = 0.0f;
        for (int i = 0; i < videoLagValuesPerTimestamp.Count; i++)
        {
            videoLagTotal += videoLagValuesPerTimestamp[i];
        }
        if (videoLagValuesPerTimestamp.Count != 0)
        {
            PlayerPrefs.SetFloat("video lag", videoLagTotal / videoLagValuesPerTimestamp.Count);
        }
        else
        {
            PlayerPrefs.SetFloat("video lag", 0.0f);
        }

        int videoLagInMsRounded = Mathf.RoundToInt(PlayerPrefs.GetFloat("video lag", 0.0f) * 1000);

        displayTMP.text = "The video lag has been set to \n " + videoLagInMsRounded.ToString() + " milliseconds";
        currentLagTMP.text = "Current video lag: " + videoLagInMsRounded.ToString() + " ms";
        backButton.SetActive(false);
        acceptButton.SetActive(true);
    }
}
