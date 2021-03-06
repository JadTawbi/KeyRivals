﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public WeaponBehaviour.Lane lane;
    private WeaponBehaviour.Side side;

    private WeaponBehaviour.AttackColour attack_colour;

    public static readonly KeyCode
        red_key_player1 = KeyCode.Z, 
        red_key_player2 = KeyCode.H,
        green_key_player1 = KeyCode.X, 
        green_key_player2 = KeyCode.J,
        blue_key_player1 = KeyCode.C, 
        blue_key_player2 = KeyCode.K,
        yellow_key_player1 = KeyCode.V, 
        yellow_key_player2 = KeyCode.L;
    public KeyCode move_up, move_down;
    private KeyCode red_key, green_key, blue_key, yellow_key;
    private Vector3 move_distance;

    public GameObject weapon_prefab;
    public Transform weapon_spawner;

    public enum PlayerState { Alive, Stunned };
    [System.NonSerialized]
    public PlayerState player_state;

    bool recover_red_pressed, recover_green_pressed, recover_blue_pressed, recover_yellow_pressed;
    public GameObject stun_overlay;
    private SpriteRenderer stun_overlay_sprite_renderer;
    public Sprite stun_overlay_sprite;
    public GameObject health;
    private HealthBehaviour health_behaviour;

    private float stun_timer;
    private const float STUN_INTERVAL = 5.0f;

    public GameObject score;
    private ScoreBehaviour score_behaviour;

    public GameObject powerup_builder;
    private BuilderBehaviour builder_behaviour;

    public enum PlayerCharacter { BassFisher, BigLightBeam, CrownJules, HotDogg, Jojitsu, LadyGooGooGaGa, Powerdog };
    private PlayerCharacter player_character;
    [System.Serializable]
    public class SpriteCollection
    {
        public List<Sprite> sprites;
    }
    public List<SpriteCollection> sprite_collections;

    private SpriteRenderer sprite_renderer;

    public GameObject[] beams = new GameObject[4];

    public GameObject stun_recovery;
    private Animator stun_recovery_animator;

    [System.NonSerialized]
    public bool portals_activated;

    public static readonly KeyCode 
        power_up_key_player1 = KeyCode.Space,
        power_up_key_player2 = KeyCode.Return;
    private KeyCode power_up_key;

    [System.NonSerialized]
    public bool shooting_locked, powerdog_powerup_active;

    [System.NonSerialized]
    public WeaponBehaviour.AttackColour powerdog_colour;
    [System.NonSerialized]
    public KeyCode key_to_mash;

    [System.NonSerialized]
    public bool movement_locked, movement_inverted;

    void Start()
    {
        stun_timer = 0.0f;

        move_distance = new Vector3(0.0f, 150.0f, 0.0f);

        sprite_renderer = gameObject.GetComponent<SpriteRenderer>();

        if (gameObject.CompareTag("Player1"))
        {
            loadCharacter(CharacterSelectMenuBehaviour.player1_character);
            side = WeaponBehaviour.Side.Player1;
            red_key = red_key_player1;
            green_key = green_key_player1;
            blue_key = blue_key_player1;
            yellow_key = yellow_key_player1;
            power_up_key = power_up_key_player1;
        }
        else if (gameObject.CompareTag("Player2"))
        {
            loadCharacter(CharacterSelectMenuBehaviour.player2_character);
            side = WeaponBehaviour.Side.Player2;
            red_key = red_key_player2;
            green_key = green_key_player2;
            blue_key = blue_key_player2;
            yellow_key = yellow_key_player2;
            power_up_key = power_up_key_player2;
        }

        recover_red_pressed = recover_green_pressed = recover_blue_pressed = recover_yellow_pressed = false;

        health_behaviour = health.GetComponent<HealthBehaviour>();
        score_behaviour = score.GetComponent<ScoreBehaviour>();
        builder_behaviour = powerup_builder.GetComponent<BuilderBehaviour>();

        stun_overlay_sprite_renderer = stun_overlay.GetComponent<SpriteRenderer>();
        stun_overlay_sprite_renderer.sprite = null;

        stun_recovery_animator = stun_recovery.GetComponent<Animator>();

        portals_activated = false;
        shooting_locked = false;
        powerdog_powerup_active = false;
        powerdog_colour = WeaponBehaviour.AttackColour.Bad;
        key_to_mash = KeyCode.None;
        movement_locked = false;
        movement_inverted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameBehaviour.paused == false)
        {
            switch (player_state)
            {
                case PlayerState.Alive:
                    if (movement_locked == false)
                    {
                        movePlayer();
                    }
                    checkPowerUpInput();
                    if (shooting_locked == false)
                    {
                        checkBeamInput();
                    }
                    if (powerdog_powerup_active == true)
                    {
                        checkPowerDogInput();
                    }
                    break;

                case PlayerState.Stunned:
                    checkStunInput();
                    break;
            }
        }
    }

    void movePlayer()
    {
        if ((Input.GetKeyDown(move_up) && movement_inverted == false) || (Input.GetKeyDown(move_down) && movement_inverted == true))
        {
            if(lane != WeaponBehaviour.Lane.First)
            { 
                transform.position += move_distance;
                lane--;
                sprite_renderer.sprite = sprite_collections[(int)player_character].sprites[(int)lane];
                //Debug.Log(gameObject.name + " has moved to the " + lane + " lane");
            }
            else if (portals_activated == true)
            {
                transform.position -= move_distance * 3;
                lane += 3;
                sprite_renderer.sprite = sprite_collections[(int)player_character].sprites[(int)lane];
                //Debug.Log(gameObject.name + " has moved to the " + lane + " lane");
            }
        }
        if ((Input.GetKeyDown(move_down) && movement_inverted == false) || (Input.GetKeyDown(move_up) && movement_inverted == true))
        {
            if (lane != WeaponBehaviour.Lane.Fourth)
            {
                transform.position -= move_distance;
                lane++;
                sprite_renderer.sprite = sprite_collections[(int)player_character].sprites[(int)lane];
                //Debug.Log(gameObject.name + " has moved to the " + lane + " lane");
            }
            else if (portals_activated == true)
            {
                transform.position += move_distance * 3;
                lane -= 3;
                sprite_renderer.sprite = sprite_collections[(int)player_character].sprites[(int)lane];
                //Debug.Log(gameObject.name + " has moved to the " + lane + " lane");
            }
        }
    }

    void shootBeam()
    {
        switch (attack_colour)
        {
            case WeaponBehaviour.AttackColour.Red:
                beams[(int)lane].GetComponent<SpriteRenderer>().color = WeaponBehaviour.red;
                break;
            case WeaponBehaviour.AttackColour.Green:
                beams[(int)lane].GetComponent<SpriteRenderer>().color = WeaponBehaviour.green;
                break;
            case WeaponBehaviour.AttackColour.Blue:
                beams[(int)lane].GetComponent<SpriteRenderer>().color = WeaponBehaviour.blue;
                break;
            case WeaponBehaviour.AttackColour.Yellow:
                beams[(int)lane].GetComponent<SpriteRenderer>().color = WeaponBehaviour.yellow;
                break;
        }


        GameObject[] weapons_in_lane = GameObject.FindGameObjectsWithTag("Weapon_" + gameObject.tag + "_" + lane.ToString());
        //Debug.Log("There is " + weapons_in_lane.Length + " weapons in the " + lane.ToString() + " lane.");
        if (weapons_in_lane.Length == 0)
        {
            beams[(int)lane].GetComponent<Animator>().SetTrigger("playerShoot");
            WeaponBehaviour new_weapon_behaviour = Instantiate(weapon_prefab, weapon_spawner).GetComponent<WeaponBehaviour>();
            new_weapon_behaviour.grow_interval = weapon_spawner.GetComponent<WeaponSpawnerBehaviour>().spawn_offset;
            new_weapon_behaviour.lane = lane;
            new_weapon_behaviour.side = side;
            new_weapon_behaviour.attack_colour = WeaponBehaviour.AttackColour.Bad;

            Debug.Log(gameObject.name + " charges up an inactive weapon in " + lane + " lane :(");
        }
        else if (weapons_in_lane.Length == 1)
        {
            if (weapons_in_lane[0].GetComponent<WeaponBehaviour>().weapon_state == WeaponBehaviour.WeaponState.GrowingUnderThreshold)
            {
                score_behaviour.resetStreak();
            }
            else
            {
                beams[(int)lane].GetComponent<Animator>().SetTrigger("playerShoot");
            }
        }
        else
        {
            beams[(int)lane].GetComponent<Animator>().SetTrigger("playerShoot");
        }
    }

    void checkBeamInput()
    {
        if (Input.GetKeyDown(red_key))
        {
            attack_colour = WeaponBehaviour.AttackColour.Red;
            shootBeam();
        }
        else if (Input.GetKeyDown(green_key))
        {
            attack_colour = WeaponBehaviour.AttackColour.Green;
            shootBeam();
        }
        else if (Input.GetKeyDown(blue_key))
        {
            attack_colour = WeaponBehaviour.AttackColour.Blue;
            shootBeam();
        }
        else if (Input.GetKeyDown(yellow_key))
        {
            attack_colour = WeaponBehaviour.AttackColour.Yellow;
            shootBeam();
        }
    }
    void checkStunInput()
    {
        if (recover_red_pressed == false && Input.GetKeyDown(red_key))
        {
            recover_red_pressed = true;
        }
        else if (recover_green_pressed == false && Input.GetKeyDown(green_key))
        {
            recover_green_pressed = true;
        }
        else if (recover_blue_pressed == false && Input.GetKeyDown(blue_key))
        {
            recover_blue_pressed = true;
        }
        else if (recover_yellow_pressed == false && Input.GetKeyDown(yellow_key))
        {
            recover_yellow_pressed = true;
        }

        if ((recover_red_pressed && recover_green_pressed && recover_blue_pressed && recover_yellow_pressed) || (stun_timer >= STUN_INTERVAL))
        {
            stun_timer = 0.0f;
            changeToPlayerState(PlayerState.Alive);
        }

        stun_timer += Time.deltaTime;
    }

    private void checkPowerUpInput()
    {
        if(Input.GetKeyDown(power_up_key))
        {
            builder_behaviour.usePowerup();
        }
    }

    private void checkPowerDogInput()
    {
        switch(powerdog_colour)
        {
            case WeaponBehaviour.AttackColour.Red:
                key_to_mash = red_key;
                break;
            case WeaponBehaviour.AttackColour.Green:
                key_to_mash = green_key;
                break;
            case WeaponBehaviour.AttackColour.Blue:
                key_to_mash = blue_key;
                break;
            case WeaponBehaviour.AttackColour.Yellow:
                key_to_mash = yellow_key;
                break;
            default:
                key_to_mash = KeyCode.None;
                break;
        }

        if (Input.GetKeyDown(key_to_mash))
        {
            score_behaviour.addScore((int)WeaponBehaviour.HIT_SCORE /*multiply by a value between 0.0f and 1.0f to balance*/ );
        }
        else if (Input.GetKeyDown(red_key) || Input.GetKeyDown(green_key) || Input.GetKeyDown(blue_key) || Input.GetKeyDown(yellow_key))
        {
            score_behaviour.resetStreak();
            //health_behaviour.loseHealth(); do we want this?
        }
    }

    public void changeToPlayerState(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.Alive:
                player_state = PlayerState.Alive;
                gameObject.GetComponent<Animator>().SetBool("stunActive", false);
                stun_recovery_animator.SetBool("stunned", false);
                stun_overlay_sprite_renderer.sprite = null;
                health_behaviour.resetHealth();
                Debug.Log(gameObject.name + " is now alive");
                break;
            case PlayerState.Stunned:
                player_state = PlayerState.Stunned;
                gameObject.GetComponent<Animator>().SetBool("stunActive", true);
                stun_recovery_animator.SetBool("stunned", true);
                stun_overlay_sprite_renderer.sprite = stun_overlay_sprite;
                recover_red_pressed = recover_green_pressed = recover_blue_pressed = recover_yellow_pressed = false;
                Debug.Log(gameObject.name + " is now stunned");
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Part") == true)
        {
            GameObject collision_game_object = collision.gameObject;
            builder_behaviour.addPart(collision_game_object.GetComponent<SpriteRenderer>().sprite, collision_game_object.GetComponent<PartBehaviour>().part_type);
            Destroy(collision.gameObject);
            Debug.Log(side + (" has collected a " + collision.name));
        }
    }

    private void loadCharacter(PlayerCharacter player_character_to_load)
    {
        sprite_renderer.sprite = sprite_collections[(int)player_character_to_load].sprites[0];
        player_character = player_character_to_load;
    }

    public PlayerCharacter getPlayerCharacter()
    {
        return player_character;
    }
}