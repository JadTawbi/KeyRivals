using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public int player1_hits, player1_misses, player2_hits, player2_misses;
    public GameObject player1_hit_count, player1_miss_count, player2_hit_count, player2_miss_count;
    private TextMeshPro player1_hit_count_TMPro, player1_miss_count_TMPro, player2_hit_count_TMPro, player2_miss_count_TMPro;


    void Start()
    {
        player1_hits = player1_misses = player2_hits = player2_misses = 0;

        player1_hit_count_TMPro = player1_hit_count.GetComponent<TextMeshPro>();
        player1_miss_count_TMPro = player1_miss_count.GetComponent<TextMeshPro>();
        player2_hit_count_TMPro = player2_hit_count.GetComponent<TextMeshPro>();
        player2_miss_count_TMPro = player2_miss_count.GetComponent<TextMeshPro>();
    }


    void Update()
    {
        player1_hit_count_TMPro.text = player1_hits.ToString();
        player1_miss_count_TMPro.text = player1_misses.ToString();
        player2_hit_count_TMPro.text = player2_hits.ToString();
        player2_miss_count_TMPro.text = player2_misses.ToString();
    }
}
