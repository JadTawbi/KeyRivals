using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;
using System;

public class WinScreenBehaviour : MonoBehaviour
{
    [System.NonSerialized]
    public static int player1_score, player2_score;
    public TextMeshProUGUI player1_score_TMP, player2_score_TMP, winner_score_TMP, winner_TMP;
    public GameObject player1, player2, winner;

    private float t1, t2, player1_easing_time, player2_easing_time;
    private const float SCORE_TO_TIME_FACTOR = 100000.0f;
    private int player1_displayed_score, player2_displayed_score;

    List<int> player1_score_digits, player2_score_digits;
    int player1_lerp_start, player2_lerp_start;

    // Start is called before the first frame update
    void Start()
    {
        player1_score = 765169;
        player2_score = 431649;

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

        player1_easing_time = player1_score / SCORE_TO_TIME_FACTOR;
        player2_easing_time = player2_score / SCORE_TO_TIME_FACTOR;

        if(player1_score >= player2_score)
        {
            winner_TMP.text = "Player 1";
            winner_score_TMP.text = player1_score.ToString();
        }
        else
        {
            winner_TMP.text = "Player 2";
            winner_score_TMP.text = player2_score.ToString();
        }
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
            if (player1_score >= player2_score)
            {
                player1.SetActive(false);
            }
            else
            {
                player2.SetActive(false);
            }
        }
    }

    public void quitToMainMenu()
    {
        SceneManager.LoadScene("Menu");
        player1.SetActive(false);
        player2.SetActive(false);
        player1_score = player2_score = 0;
    }

    private void easeScore()
    {
        if (player1_score_digits.Count > 0)
        {
            float time_to_elapse = (player1_score_digits[0] * player1_score_digits.Count) / 20.0f;

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
            float time_to_elapse = (player2_score_digits[0] * player2_score_digits.Count) / 20.0f;

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
