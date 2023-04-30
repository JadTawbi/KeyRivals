using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CalibrationBehaviour : MonoBehaviour
{
	[SerializeField] private CalibrationType calibrationType;
	private enum CalibrationType
	{
		Audio,
		Video,
	}

	[SerializeField, Tooltip("Leave empty for Video Calibration")] private AudioSource audioSource;
	[SerializeField, Tooltip("Leave empty for Audio calibration")] private GameObject[] weapons;

	private float calibrationTimer, intervalTimer;
	private List<float> timestamps;

	[SerializeField, Tooltip("How long should the calibration last")]				private float duration;
	[SerializeField, Tooltip("How often should feedback occur during calibration")] private float interval;
	[SerializeField] private TextMeshProUGUI displayTMP, currentLagTMP;
	[SerializeField] private GameObject backButton, acceptButton;

	private string lagName;
	private string audioLagInstructions = "Press Z every time you hear the metronome to calibrate audio lag.\nThis will take about one minute.";
	private string videoLagInstructions = "Press Z every time the colored circles reach the edge of the circumference to calibrate video lag.\nThis will take about one minute.";

	private bool isDone;

	private void OnEnable()
	{
		Initialize();
	}

	private void Initialize()
	{
		switch (calibrationType)
		{
			case CalibrationType.Audio:
				lagName = "audio lag";
				displayTMP.text = audioLagInstructions;
				break;
			case CalibrationType.Video:
				lagName = "video lag";
				displayTMP.text = videoLagInstructions;
				break;
		}

		isDone = false;
		calibrationTimer = intervalTimer = 0.0f;
		timestamps = new List<float>();
		currentLagTMP.text = "Current " + lagName + ": " + ((int)(1000 * PlayerPrefs.GetFloat(lagName, 0.0f))).ToString() + " ms";

		backButton.SetActive(true);
		acceptButton.SetActive(false);
	}

	private void Update()
	{
		Calibrate();
	}

	private void Calibrate()
	{
		if (calibrationTimer < duration)
		{
			Feedback();
			Timestamp();
			calibrationTimer += Time.deltaTime;
		}
		else if (isDone == false)
		{
			CalculateAndDisplayLag();
			isDone = true;
		}
	}

	private void Feedback()
	{
		if (intervalTimer >= interval)
		{
			if (calibrationType == CalibrationType.Audio) { audioSource.Play(); }
			if (calibrationType == CalibrationType.Video) { ScaleWeapons(Vector3.zero); }
			intervalTimer -= interval;
		}
		else
		{
			if (calibrationType == CalibrationType.Video)
			{
				var newScale = new Vector3(intervalTimer / interval, intervalTimer / interval, 0.0f);
				ScaleWeapons(newScale);
			}
		}

		intervalTimer += Time.deltaTime;
	}

	private void ScaleWeapons(Vector3 newScale)
	{
		foreach (var weapon in weapons)
		{
			weapon.transform.localScale = newScale;
		}
	}

	private void Timestamp()
	{
		if (Input.GetKeyDown(KeyCode.Z) == true)
		{
			timestamps.Add(calibrationTimer);
		}
	}

	private void CalculateAndDisplayLag()
	{
		float lagTotalSum = 0.0f;
		for (int i = 0; i < timestamps.Count; i++)
		{
			//Floor the timestamp to a multiple of the interval
			float flooredValue = interval * Mathf.FloorToInt(timestamps[i] / interval);

			//Subtract the floored timestamp to obtain the value of the deviation from the interval
			float videoLagValue = timestamps[i] - flooredValue;

			//From the two possible deviations from an exact interval value, store the smallest of them
			lagTotalSum += Mathf.Min(videoLagValue, interval - videoLagValue);
		}

		if (lagTotalSum == 0 || timestamps.Count == 0)
		{
			PlayerPrefs.SetFloat(lagName, 0.0f);
		}
		else
		{
			//Calculate the average video lag and store it
			float lag = lagTotalSum / timestamps.Count;
			PlayerPrefs.SetFloat(lagName, lag);
		}

		int lagInMsRounded = Mathf.RoundToInt(PlayerPrefs.GetFloat(lagName, 0.0f) * 1000);

		displayTMP.text = "The " + lagName + " has been set to \n " + lagInMsRounded.ToString() + " milliseconds";
		currentLagTMP.text = "Current " + lagName + ": " + lagInMsRounded.ToString() + " ms";

		backButton.SetActive(false);
		acceptButton.SetActive(true);
	}
}
