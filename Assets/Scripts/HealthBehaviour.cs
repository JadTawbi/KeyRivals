using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBehaviour : MonoBehaviour
{
    public GameObject hp1, hp2, hp3;
    [System.NonSerialized]
    public int hit_points;

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
        if (hit_points < 3)
        {
            if (hp3.activeSelf == true)
            {
                hp3.SetActive(false);
            }
            if (hit_points < 2)
            {
                if (hp2.activeSelf == true)
                {
                    hp2.SetActive(false);
                }
                if (hit_points < 1)
                {
                    if (hp1.activeSelf == true)
                    {
                        hp1.SetActive(false);
                        player_behaviour.changeToPlayerState(PlayerBehaviour.PlayerState.Stunned);
                    }
                }
                else if (hp1.activeSelf == false)
                {
                    hp1.SetActive(true);
                }
            }
            else
            {
                if (hp2.activeSelf == false)
                {
                    hp2.SetActive(true);
                }
                if (hp1.activeSelf == false)
                {
                    hp1.SetActive(true);
                }
            }
        }
        else
        {
            if (hp3.activeSelf == false)
            {
                hp3.SetActive(true);
            }
            if (hp2.activeSelf == false)
            {
                hp2.SetActive(true);
            }
            if (hp1.activeSelf == false)
            {
                hp1.SetActive(true);
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
