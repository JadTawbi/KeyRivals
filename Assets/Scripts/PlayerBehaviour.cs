using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public enum Lane { First, Second, Third, Fourth };
    public Lane lane;
    public KeyCode move_up, move_down;
    private Vector3 move_distance;

    void Start()
    {
        move_distance = new Vector3(0.0f, 3.0f, 0.0f);

    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();

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
}