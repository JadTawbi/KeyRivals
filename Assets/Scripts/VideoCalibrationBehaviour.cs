using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VideoCalibrationBehaviour : MonoBehaviour
{
    private float video_calibration_timer, weapon_grow_timer;
    private const float VIDEO_CALIBRATION_INTERVAL = 60.0f, WEAPON_GROW_INTERVAL = 1.0f;
    public GameObject weapon_1_red, weapon_2_green, weapon_3_blue, weapon_4_yellow;

    private bool timer_done;

    private List<float> timestamps;
    private List<float> video_lag_values_per_timestamp;
    private static float final_video_lag;

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
        video_calibration_timer = weapon_grow_timer = 0.0f;

        timestamps = new List<float>();
        video_lag_values_per_timestamp = new List<float>();

        timer_done = false;

        display_TMP.text = "Press Z every time the colored circles reach the edge of the circumference to calibrate video lag.\nThis will take about one minute.";

        back_button.SetActive(true);
        accept_button.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer_done == false)
        {
            if (video_calibration_timer < VIDEO_CALIBRATION_INTERVAL)
            {
                timestampInput();
                video_calibration_timer += Time.deltaTime;
            }
            else
            {
                timer_done = true;
                displayVideoLag();
            }
        }
    }

    private void timestampInput()
    {
        if (weapon_grow_timer >= WEAPON_GROW_INTERVAL)
        {
            weapon_1_red.transform.localScale = Vector3.zero;
            weapon_2_green.transform.localScale = Vector3.zero;
            weapon_3_blue.transform.localScale = Vector3.zero;
            weapon_4_yellow.transform.localScale = Vector3.zero;

            weapon_grow_timer -= WEAPON_GROW_INTERVAL;
        }
        else
        {
            weapon_1_red.transform.localScale = new Vector3(weapon_grow_timer / WEAPON_GROW_INTERVAL, weapon_grow_timer / WEAPON_GROW_INTERVAL, 0.0f);
            weapon_2_green.transform.localScale = new Vector3(weapon_grow_timer / WEAPON_GROW_INTERVAL, weapon_grow_timer / WEAPON_GROW_INTERVAL, 0.0f);
            weapon_3_blue.transform.localScale = new Vector3(weapon_grow_timer / WEAPON_GROW_INTERVAL, weapon_grow_timer / WEAPON_GROW_INTERVAL, 0.0f);
            weapon_4_yellow.transform.localScale = new Vector3(weapon_grow_timer / WEAPON_GROW_INTERVAL, weapon_grow_timer / WEAPON_GROW_INTERVAL, 0.0f);
        }

        if (Input.GetKeyDown(KeyCode.Z) == true)
        {
            timestamps.Add(video_calibration_timer);
        }

        weapon_grow_timer += Time.deltaTime;
    }

    private void displayVideoLag()
    {
        for (int i = 0; i < timestamps.Count; i++)
        {
            float video_lag_value = timestamps[i] - WEAPON_GROW_INTERVAL * (int)(timestamps[i] / WEAPON_GROW_INTERVAL);
            video_lag_value = Mathf.Min(video_lag_value, WEAPON_GROW_INTERVAL - video_lag_value);
            video_lag_values_per_timestamp.Add(video_lag_value);
        }

        float video_lag_total = 0.0f;
        for (int i = 0; i < video_lag_values_per_timestamp.Count; i++)
        {
            video_lag_total += video_lag_values_per_timestamp[i];
        }
        if (video_lag_values_per_timestamp.Count != 0)
        {
            final_video_lag = video_lag_total / video_lag_values_per_timestamp.Count;
        }
        else
        {
            final_video_lag = 0.0f;
        }

        MainMenuBehaviour.video_lag = final_video_lag;

        int video_lag_in_ms_rounded = Mathf.RoundToInt(final_video_lag * 1000);

        display_TMP.text = "The video lag has been set to \n " + video_lag_in_ms_rounded.ToString() + " milliseconds";
        back_button.SetActive(false);
        accept_button.SetActive(true);
    }
}
