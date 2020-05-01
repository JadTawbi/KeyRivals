using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBehaviour : MonoBehaviour
{
    public GameObject multiplier, score;
    private int multiplier_amount, score_amount, streak_counter;
    private TextMeshPro multiplier_TMPro, score_TMPro;

    void Start()
    {
        score_amount = 0;
        multiplier_amount = 1;
        multiplier_TMPro = multiplier.GetComponent<TextMeshPro>();
        score_TMPro = score.GetComponent<TextMeshPro>();
    }

    void Update()
    {
        displayText();
    }

    private void displayText()
    {
        multiplier_TMPro.text = multiplier_amount.ToString();
        score_TMPro.text = score_amount.ToString();
    }

    public void addScore(int score_to_add)
    {
        score_amount += score_to_add * multiplier_amount;
        streak_counter++;
        increaseMultiplier();
    }

    private void increaseMultiplier()
    {
        if (streak_counter > 0 && streak_counter % 5 == 0)
        {
            multiplier_amount++;
        }
    }

    public void resetStreak()
    {
        streak_counter = 0;
        multiplier_amount = 1;
    }
}
