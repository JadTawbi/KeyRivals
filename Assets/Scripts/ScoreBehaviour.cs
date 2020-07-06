using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBehaviour : MonoBehaviour
{
    public GameObject multiplier, score;
    private int multiplier_amount, streak_counter;
    [System.NonSerialized]
    public int score_amount;
    private int score_displayed, score_before_adding;
    private float t;
    private const float EASING_TIME = 0.2f;
    private TextMeshPro multiplier_TMPro, score_TMPro;

    [System.NonSerialized]
    public bool multiplier_locked;

    void Start()
    {
        score_amount = 0;
        multiplier_amount = 1;
        multiplier_TMPro = multiplier.GetComponent<TextMeshPro>();
        score_TMPro = score.GetComponent<TextMeshPro>();

        score_displayed = score_before_adding = 0;
        t = 0.0f;
        multiplier_locked = false;
    }

    void Update()
    {
        easeScore();
        displayText();
    }

    private void displayText()
    {
        multiplier_TMPro.text = multiplier_amount.ToString();
        score_TMPro.text = score_displayed.ToString();
    }

    public void addScore(int score_to_add)
    {
        score_before_adding = score_amount;
        t = 0;
        score_amount += score_to_add * multiplier_amount;
        streak_counter++;
        increaseMultiplier();
    }

    public void easeScore()
    {
        t = Mathf.MoveTowards(t, 1.0f, Time.deltaTime / EASING_TIME);
        score_displayed = (int)Mathf.Lerp(score_before_adding, score_amount, t);
    }

    private void increaseMultiplier()
    {
        if (streak_counter > 0 && streak_counter % 5 == 0)
        {
            multiplier_amount++;
        }
    }

    public void multiplyMultiplierBy(float x)
    {
        multiplier_amount = Mathf.CeilToInt(x*multiplier_amount);
    }

    public void increaseMultiplierBy(int x)
    {
        multiplier_amount += x;
    }

    public void resetStreak()
    {
        streak_counter = 0;
        if(multiplier_locked == false)
        {
            multiplier_amount = 1;
        }
    }
}
