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
    private TextMeshPro multiplier_TMPro, score_TMPro;

    //score easing
    public EasingFunctions easing;
    private float easing_timer, easing_timer_max;
    private int amount_to_add, amount_before_easing;
    private List<int> added_amounts;

    void Start()
    {
        score_amount = 0;
        multiplier_amount = 1;
        multiplier_TMPro = multiplier.GetComponent<TextMeshPro>();
        score_TMPro = score.GetComponent<TextMeshPro>();


        //easing_timer = amount_to_add = amount_before_easing = added_amounts[0] = 0;
        //easing_timer_max = 100.0f;
    }

    void Update()
    {
        displayText();
    }

    private void displayText()
    {
        //score_amount = (int)(easing.easeNone(easing_timer, (float) amount_before_easing, (float) added_amounts[0], easing_timer_max));

        //if (easing_timer >= easing_timer_max)
        //{
        //    added_amounts.Remove(added_amounts[0]);
        //    easing_timer = 0;
        //    amount_before_easing = score_amount;
        //}
        //else
        //{
        //    easing_timer++;
        //    if (easing_timer >= easing_timer_max)
        //    {
        //        easing_timer = easing_timer_max;
        //    }
        //}

        multiplier_TMPro.text = multiplier_amount.ToString();
        score_TMPro.text = score_amount.ToString();
    }

    public void addScore(int score_to_add)
    {
        //amount_to_add = score_to_add * multiplier_amount;
        //added_amounts.Add(amount_to_add);

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
