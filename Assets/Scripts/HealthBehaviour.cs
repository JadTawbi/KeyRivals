using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBehaviour : MonoBehaviour
{
    public GameObject[] hp_objects = new GameObject[3];
    [System.NonSerialized]
    public int hit_points;
    private const int MAX_HP = 3;

    public GameObject player;
    private PlayerBehaviour player_behaviour;

    private float invincibility_timer;
    private const float invincibility_interval = 1.5f;
    private bool invincibility_active;
    
    // Start is called before the first frame update
    void Start()
    {
        hit_points = 3;
        player_behaviour = player.GetComponent<PlayerBehaviour>();
        invincibility_timer = 0.0f;
        invincibility_active = false;
    }

    // Update is called once per frame
    void Update()
    {
        checkHitPoints();
        checkInvincibility();
    }

    private void checkHitPoints()
    {
        for (int i = 0; i < MAX_HP; i++)
        {
            if (i < hit_points)
            {
                if (hp_objects[i].activeSelf != true)
                {
                    hp_objects[i].SetActive(true);
                }
            }
            else
            {
                if (hp_objects[i].activeSelf != false)
                {
                    hp_objects[i].SetActive(false);
                    if (i == 0)
                    {
                        player_behaviour.changeToPlayerState(PlayerBehaviour.PlayerState.Stunned);
                    }
                } 
            }
        }
    }

    public void loseHealth()
    {
        if (invincibility_active == false)
        {
            hit_points--;

            if (hit_points < 0)
            {
                hit_points = 0;
            }
            else if (hit_points > 3)
            {
                hit_points = 3;
            }
            activateInvincibility();
        }
    }

    public void resetHealth()
    {
        hit_points = 3;
        activateInvincibility();
    }

    void checkInvincibility()
    {
        if (invincibility_active == true)
        {
            if (invincibility_timer >= invincibility_interval)
            {
                invincibility_active = false;
                invincibility_timer = 0.0f;
            }
            else
            {
                invincibility_timer += Time.deltaTime;
            }
        }
    }

    void activateInvincibility()
    {
        invincibility_active = true;
    }
}
