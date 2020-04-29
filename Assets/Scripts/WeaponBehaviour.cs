﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    private SpriteRenderer sprite_renderer;

    public enum Lane { First, Second, Third, Fourth };
    public Lane lane;

    public enum AttackColour { Red, Green, Blue, Yellow, Bad};
    public AttackColour attack_colour;
    private Color red, green, blue, yellow, bad;
    private KeyCode input_key;
    private KeyCode red_key, green_key, blue_key, yellow_key;

    public enum Side { Player1 = -1, Player2 = 1};
    public Side side;

    private GameObject player;
    private PlayerBehaviour player_behaviour;
    private float grow_timer, stay_charged_interval, stay_charged_timer;
    public float grow_interval;

    // Start is called before the first frame update
    void Start()
    {
        stay_charged_interval = grow_interval/2;
        stay_charged_timer = 0.0f;

        red = new Color(1, 0, 0);
        green = new Color(0, 1, 0);
        blue = new Color(0, 0, 1);
        yellow = new Color(1, 1, 0);
        bad = new Color(0.60f, 0, 0.80f);

        initializeCharacteristics();
    }

    void initializeCharacteristics()
    {
        switch (side)
        {
            case Side.Player1:
                player = GameObject.FindWithTag("Player1");
                red_key = KeyCode.Z;
                green_key = KeyCode.X;
                blue_key = KeyCode.C;
                yellow_key = KeyCode.V;
                break;

            case Side.Player2:
                player = GameObject.FindWithTag("Player2");
                red_key = KeyCode.H;
                green_key = KeyCode.J;
                blue_key = KeyCode.K;
                yellow_key = KeyCode.L;
                break;
        }

        player_behaviour = player.GetComponent<PlayerBehaviour>();
        sprite_renderer = GetComponent<SpriteRenderer>();

        transform.position = new Vector3((int)side * 150.0f, 225.0f - 150.0f * (int)lane, 0.0f);
        /* To calculate x position: side enum is set to either -1 or 1 and then used in the calculation
         * To calculate y position: lane enum is cast into an int and is used to calculate how far down from the first lane its position is going to be.*/

        switch (attack_colour)
        {
            case AttackColour.Red:
                sprite_renderer.color = red;
                input_key = red_key;
                break;

            case AttackColour.Green:
                sprite_renderer.color = green;
                input_key = green_key;
                break;

            case AttackColour.Blue:
                sprite_renderer.color = blue;
                input_key = blue_key;
                break;

            case AttackColour.Yellow:
                sprite_renderer.color = yellow;
                input_key = yellow_key;
                break;
        }
        gameObject.name = "Weapon_" + player.tag + "_" + lane.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        checkHitMiss();
        growFilling();
    }

    void growFilling()
    {
        if (grow_timer < grow_interval) //Weapon is growing
        {
            grow_timer += Time.deltaTime;
            transform.localScale = new Vector3(grow_timer / grow_interval, grow_timer / grow_interval, 1.0f);
            if ( stay_charged_timer != 0.0f)
            {
                stay_charged_timer = 0.0f;
            }
        }
        else if (stay_charged_timer < stay_charged_interval) //Weapon has grown and is charging
        {
            stay_charged_timer += Time.deltaTime;
            if(transform.localScale.x != 1.0f || transform.localScale.y != 1.0f)
            {
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
        }
        else //Weapon has fired
        {
            Debug.Log(attack_colour + " weapon shot " + side);
            Destroy(gameObject);
        }
    }

    void checkHitMiss()
    {
        if (Input.GetKeyDown(input_key))
        {
            if(lane == player_behaviour.lane)
            {
                Debug.Log(side+" disabled a " + attack_colour + " weapon!!");
                Destroy(gameObject);
            }
        }
        else if (Input.GetKeyDown(red_key) || Input.GetKeyDown(green_key) || Input.GetKeyDown(blue_key) || Input.GetKeyDown(yellow_key))
        {
            if (lane == player_behaviour.lane)
            {
                grow_timer = grow_interval;
                sprite_renderer.color = bad;
                Debug.Log(side + " might be colorblind (sorry if you actually are). Wrong color! ");
            }
        }
    }
}
