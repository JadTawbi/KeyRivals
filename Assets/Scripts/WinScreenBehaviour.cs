﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;
using System;
using UnityEditor;

public class WinScreenBehaviour : MonoBehaviour
{
    [System.NonSerialized]
    public static int player1_score, player2_score;
    public TextMeshProUGUI player1_score_TMP, player2_score_TMP, wins_TMP, winner_TMP, song_TMP;
    public GameObject player1, player2, winner;

    private float t1, t2;
    private int player1_displayed_score, player2_displayed_score;

    List<int> player1_score_digits, player2_score_digits;
    int player1_lerp_start, player2_lerp_start;

    public AudioSource audio_source;

    // Start is called before the first frame update
    void Start()
    {
        switch (SongSelectMenuBehaviour.track)
        {
            case WeaponSpawnerBehaviour.PlayableTrack.LucidDream:
                song_TMP.text = "01 - Lucid Dream";
                break;
            case WeaponSpawnerBehaviour.PlayableTrack.Schukran:
                song_TMP.text = "02 - Schukran";
                break;
            case WeaponSpawnerBehaviour.PlayableTrack.ElTio:
                song_TMP.text = "03 - El Tio";
                break;
            case WeaponSpawnerBehaviour.PlayableTrack.Rivals:
                song_TMP.text = "04 - Rivals";
                break;
            case WeaponSpawnerBehaviour.PlayableTrack.SEKBeat:
                song_TMP.text = "05 - SEKBeat";
                break;
            case WeaponSpawnerBehaviour.PlayableTrack.Lagom:
                song_TMP.text = "06 - Lagom";
                break;
            case WeaponSpawnerBehaviour.PlayableTrack.Deeper:
                song_TMP.text = "07 - Deeper";
                break;
            case WeaponSpawnerBehaviour.PlayableTrack.Practice:
                song_TMP.text = "00 - Practice";
                break;
        }
        player1_score_digits = new List<int>();
        player2_score_digits = new List<int>();

        int score = player1_score;
        while (score > 0)
        {
            player1_score_digits.Add(score % 10);
            score /= 10;
        }
        player1_score_digits.Reverse();

        score = player2_score;
        while (score > 0)
        {
            player2_score_digits.Add(score % 10);
            score /= 10;
        }
        player2_score_digits.Reverse();

        player1_lerp_start = player2_lerp_start = 0;

        t1 = t2 = 0.0f;
        player1_displayed_score = player2_displayed_score = 0;

        if(player1_score > player2_score)
        {
            winner_TMP.text = "Player 1";
        }
        else if (player1_score < player2_score)
        {
            winner_TMP.text = "Player 2";
        }
        else
        {
            winner_TMP.text = "Both players";
            wins_TMP.text = "tied!";
        }

        audio_source.volume = PlayerPrefs.GetFloat("volume", OptionsMenuBehaviour.DEFAULT_VOLUME);
        audio_source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        easeScore();

        player1_score_TMP.text = player1_displayed_score.ToString();
        player2_score_TMP.text = player2_displayed_score.ToString();
        if (t1 == 1.0f && t2 == 1.0f)
        {
            winner.SetActive(true);
        }
    }

    public void quitToMainMenu()
    {
        SceneManager.LoadScene("Menu");
        player1_score = player2_score = 0;
    }

    private void easeScore()
    {
        if (player1_score_digits.Count > 0)
        {
            float time_to_elapse = (player1_score_digits[0] * player1_score_digits.Count) / 5.0f;

            t1 = Mathf.MoveTowards(t1, 1.0f, Time.deltaTime / time_to_elapse);
            player1_displayed_score = (int)Mathf.Lerp(player1_lerp_start, player1_score, t1);

            int order = (int)Mathf.Pow(10, player1_score_digits.Count - 1);

            if (player1_displayed_score >= (player1_lerp_start + (player1_score_digits[0] * order)))
            {
                player1_lerp_start += player1_score_digits[0] * order;
                t1 = 0;
                player1_score_digits.RemoveAt(0);
            }
        }
        else
        {
            t1 = 1.0f;
        }

        if (player2_score_digits.Count > 0)
        {
            float time_to_elapse = (player2_score_digits[0] * player2_score_digits.Count) / 5.0f;

            t2 = Mathf.MoveTowards(t2, 1.0f, Time.deltaTime / time_to_elapse);
            player2_displayed_score = (int)Mathf.Lerp(player2_lerp_start, player2_score, t2);

            int order = (int)Mathf.Pow(10, player2_score_digits.Count - 1);

            if (player2_displayed_score >= (player2_lerp_start + (player2_score_digits[0] * order)))
            {
                player2_lerp_start += player2_score_digits[0] * order;
                t2 = 0;
                player2_score_digits.RemoveAt(0);
            }
        }
        else
        {
            t2 = 1.0f;
        }
    }
}
