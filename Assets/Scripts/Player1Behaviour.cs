using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Behaviour : MonoBehaviour
{
    public enum Lane { First, Second, Third, Fourth };
    public Lane lane;
    private Vector3 move_distance;

    void Start()
    {
        move_distance = new Vector3(1.75f, 0.0f, 0.0f);
        lane = Lane.Second;

    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();

    }

    void movePlayer()
    {
        if (Input.GetKeyDown(KeyCode.A) && lane != Lane.First)
        {
            transform.position -= move_distance;
            lane--;
            Debug.Log(gameObject.name + " has moved to the " + lane + " lane");
        }
        if (Input.GetKeyDown(KeyCode.D) && lane != Lane.Fourth)
        {
            transform.position += move_distance;
            lane++;
            Debug.Log(gameObject.name + " has moved to the " + lane + " lane");
        }
    }
}