using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public enum Lane { First, Second, Third, Fourth };
    public Lane lane;

    private enum BeamType { Red, Green, Blue, Yellow };
    private BeamType beam_type;

    public KeyCode move_up, move_down;
    private KeyCode red_key, green_key, blue_key, yellow_key;
    private Vector3 move_distance;
    private GameObject[] active_weapons;

    void Start()
    {
        move_distance = new Vector3(0.0f, 150.0f, 0.0f);

        if (gameObject.CompareTag("Player1"))
        {
            red_key = KeyCode.Z;
            green_key = KeyCode.X;
            blue_key = KeyCode.C;
            yellow_key = KeyCode.V;
        }
        else if (gameObject.CompareTag("Player2"))
        {
            red_key = KeyCode.H;
            green_key = KeyCode.J;
            blue_key = KeyCode.K;
            yellow_key = KeyCode.L;
        }
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
        if (Input.GetKeyDown(red_key))
        {
            beam_type = BeamType.Red;
            shootBeam();
        }
        else if (Input.GetKeyDown(green_key))
        {
            beam_type = BeamType.Green;
            shootBeam();
        }
        else if (Input.GetKeyDown(blue_key))
        {
            beam_type = BeamType.Blue;
            shootBeam();
        }
        else if (Input.GetKeyDown(yellow_key))
        {
            beam_type = BeamType.Yellow;
            shootBeam();
        }
    }

    void movePlayer()
    {
        if (Input.GetKeyDown(move_up) && lane != Lane.First)
        {
            transform.position += move_distance;
            lane--;
            Debug.Log(gameObject.name + " has moved to the " + lane + " lane");
        }
        if (Input.GetKeyDown(move_down) && lane != Lane.Fourth)
        {
            transform.position -= move_distance;
            lane++;
            Debug.Log(gameObject.name + " has moved to the " + lane + " lane");
        }
    }

    void shootBeam()
    {
        switch (beam_type)
        {
            // change the color of the beam here
            default:
                break;
        }

        active_weapons = GameObject.FindGameObjectsWithTag("Weapon");
        bool weapon_is_occupied = false;
        for (int i = 0; i < active_weapons.Length; i++)
        {
            if(("Weapon_"+ gameObject.name +"_"+lane.ToString()) == active_weapons[i].name)
            {
                weapon_is_occupied = true;
                Debug.Log("weapon active in " + lane + " lane");
                break;
            }
        }
        if (weapon_is_occupied == false)
        {
            Debug.Log(gameObject.name + " takes damage because there was no weapon in the " + lane + " lane :("); //spawn a charged weapon in that lane
        }
    }
}