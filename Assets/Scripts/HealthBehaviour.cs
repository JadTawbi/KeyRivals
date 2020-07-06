using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBehaviour : MonoBehaviour
{
    private const int MAX_HP = 3;
    public GameObject[] hp_objects = new GameObject[MAX_HP];
    private SpriteRenderer[] hp_objects_sprite_renderer = new SpriteRenderer[MAX_HP];
    public Sprite[] hp_sprites = new Sprite[MAX_HP];
    [System.NonSerialized]
    public int hit_points;

    public GameObject player;
    private PlayerBehaviour player_behaviour;

    private float invincibility_timer;
    private const float INVINCIBILITY_INTERVAL = 2.0f;
    private bool invincibility_active;

    [System.NonSerialized]
    public bool health_locked;

    // Start is called before the first frame update
    void Start()
    {
        hit_points = MAX_HP;
        player_behaviour = player.GetComponent<PlayerBehaviour>();
        for (int i = 0; i < MAX_HP; i++)
        {
            hp_objects_sprite_renderer[i] = hp_objects[i].GetComponent<SpriteRenderer>();
        }
        invincibility_timer = 0.0f;
        invincibility_active = false;
        health_locked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameBehaviour.paused == false)
        {
            checkHitPoints();
            checkInvincibility();
        }
    }

    private void checkHitPoints()
    {
        for (int i = 0; i < MAX_HP; i++)
        {
            if (i < hit_points)
            {
                if (hp_objects_sprite_renderer[i].sprite == null)
                {
                    hp_objects_sprite_renderer[i].sprite = hp_sprites[i];
                }
            }
            else
            {
                if (hp_objects_sprite_renderer[i].sprite != null)
                {
                    hp_objects_sprite_renderer[i].sprite = null;

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
        if(health_locked == false)
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
    }

    public void resetHealth()
    {
        if(hit_points < MAX_HP)
        { 
            hit_points = MAX_HP;
            activateInvincibility();
        }
    }

    void checkInvincibility()
    {
        if (invincibility_active == true)
        {
            if (invincibility_timer >= INVINCIBILITY_INTERVAL)
            {
                invincibility_active = false;
                player.GetComponent<Animator>().SetBool("invincibilityActive", false);
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
        player.GetComponent<Animator>().SetBool("invincibilityActive", true);
    }
}
