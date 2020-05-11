﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public WeaponBehaviour.Lane lane;
    private WeaponBehaviour.Side side;

    private WeaponBehaviour.AttackColour attack_colour;

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
    public GameObject health;
    private HealthBehaviour health_behaviour;

    private float stun_timer;
    private const float stun_interval = 5.0f;

    public GameObject score;
    private ScoreBehaviour score_behaviour;

    void Start()
    {
        stun_timer = 0.0f;

        move_distance = new Vector3(0.0f, 150.0f, 0.0f);

        if (gameObject.CompareTag("Player1"))
        {
            side = WeaponBehaviour.Side.Player1;
            red_key = KeyCode.Z;
            green_key = KeyCode.X;
            blue_key = KeyCode.C;
            yellow_key = KeyCode.V;
        }
        else if (gameObject.CompareTag("Player2"))
        {
            side = WeaponBehaviour.Side.Player2;
            red_key = KeyCode.H;
            green_key = KeyCode.J;
            blue_key = KeyCode.K;
            yellow_key = KeyCode.L;
        }

        recover_red_pressed = recover_green_pressed = recover_blue_pressed = recover_yellow_pressed = false;

        stun_overlay.SetActive(false);

        health_behaviour = health.GetComponent<HealthBehaviour>();
        score_behaviour = score.GetComponent<ScoreBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (player_state)
        {
            case PlayerState.Alive:
                movePlayer();
                checkBeamInput();
                break;

            case PlayerState.Stunned:
                checkStunInput();
                break;
        }
    }

    void movePlayer()
    {
        if (Input.GetKeyDown(move_up) && lane != WeaponBehaviour.Lane.First)
        {
            transform.position += move_distance;
            lane--;
            //Debug.Log(gameObject.name + " has moved to the " + lane + " lane");
        }
        if (Input.GetKeyDown(move_down) && lane != WeaponBehaviour.Lane.Fourth)
        {
            transform.position -= move_distance;
            lane++;
            //Debug.Log(gameObject.name + " has moved to the " + lane + " lane");
        }
    }

    void shootBeam()
    {
        switch (attack_colour)
        {
            // change the color of the beam here
            default:
                break;
        }

        GameObject[] weapons_in_lane = GameObject.FindGameObjectsWithTag("Weapon_" + gameObject.tag + "_" + lane.ToString());
        //Debug.Log("There is " + weapons_in_lane.Length + " weapons in the " + lane.ToString() + " lane.");
        if (weapons_in_lane.Length == 0)
        {
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

        if((recover_red_pressed && recover_green_pressed && recover_blue_pressed && recover_yellow_pressed) || (stun_timer >= stun_interval))
        {
            stun_timer = 0.0f;
            changeToPlayerState(PlayerState.Alive);
        }

        stun_timer += Time.deltaTime;
    }

    public void changeToPlayerState(PlayerState state)
    {
        switch(state)
        {
            case PlayerState.Alive:
                player_state = PlayerState.Alive;
                stun_overlay.SetActive(false);
                health_behaviour.resetHealth();
                Debug.Log(gameObject.name + " is now alive");
                break;
            case PlayerState.Stunned:
                player_state = PlayerState.Stunned;
                stun_overlay.SetActive(true);
                recover_red_pressed = recover_green_pressed = recover_blue_pressed = recover_yellow_pressed = false;
                Debug.Log(gameObject.name + " is now stunned");
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Part") == true)
        {
            // add part to slot
            Destroy(collision.gameObject);
            Debug.Log(side + (" has collected a power up part"));
        }
    }
}