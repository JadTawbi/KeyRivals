using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WinScreenBehaviour : MonoBehaviour
{
    [System.NonSerialized]
    public static int player1_score, player2_score;
    public TextMeshProUGUI player1_score_TMP, player2_score_TMP, winner_score_TMP, winner_TMP;
    public GameObject player1, player2;

    // Start is called before the first frame update
    void Start()
    {
        player1_score_TMP.text = player1_score.ToString();
        player2_score_TMP.text = player2_score.ToString();
        if(player1_score >= player2_score)
        {
            winner_score_TMP.text = player1_score.ToString();
            winner_TMP.text = "Player 1";
            player2.SetActive(true);
            player1.SetActive(false);
        }
        else
        {
            winner_score_TMP.text = player2_score.ToString();
            winner_TMP.text = "Player 2";
            player1.SetActive(true);
            player2.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void quitToMainMenu()
    {
        SceneManager.LoadScene("Menu");
        player1.SetActive(false);
        player2.SetActive(false);
        player1_score = player2_score = 0;
    }
}
