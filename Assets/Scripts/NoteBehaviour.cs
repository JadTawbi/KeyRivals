using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBehaviour : MonoBehaviour
{
    public enum Lane { First, Second, Third, Fourth };
    public Lane lane;
    public KeyCode key;
    private Vector2 initial_position = new Vector2(-6.2f, 13.55f);
    private const float SPEED = 3.0f;
    private const float NOTE_HIT_THRESHOLD = 2.0f;
    private const float NOTE_MISSED_THRESHOLD = 0.0f;
    private float sprite_half_height;
    private float note_top_side, note_bottom_side;
    public GameObject player;
    private Player1Behaviour player_behaviour;
    // Start is called before the first frame update
    void Start()
    {
        player_behaviour = player.GetComponent<Player1Behaviour>();
        sprite_half_height = GetComponent<SpriteRenderer>().bounds.extents.y;
        note_top_side = note_bottom_side = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        move();
        checkHitMiss();
    }

    void move()
    {
        transform.position += -transform.up * SPEED * Time.deltaTime;
    }

    void checkHitMiss()
    {
        note_top_side = transform.position.y + sprite_half_height;      
        if ( note_top_side <= NOTE_HIT_THRESHOLD)
        {
            if(Input.GetKeyDown(key))
            {
                if((int)lane == (int)player_behaviour.lane)
                {
                    Debug.Log("You hit the note");
                    transform.position = initial_position;
                    transform.position += new Vector3(1.75f * (int)lane, 0.0f, 0.0f);
                }
                else
                {
                    Debug.Log("Wrong lane idiot");
                }
            } 
            note_bottom_side = transform.position.y - sprite_half_height;
            if (note_bottom_side <= NOTE_MISSED_THRESHOLD)
            {
                Debug.Log("You missed a note");
                transform.position = initial_position;
                transform.position += new Vector3(1.75f * (int)lane, 0.0f, 0.0f);
            }
        }
    }
}
