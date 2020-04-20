using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBehaviour : MonoBehaviour
{
    public enum Lane { First, Second, Third, Fourth };
    public Lane lane;
    public KeyCode key;
    private Vector2 initial_position = new Vector2(-724.0f/54.0f, 47.0f);
    private const float SPEED = 5.0f;
    private const float NOTE_HIT_THRESHOLD = -71.0f/54.0f;
    private const float NOTE_MISSED_THRESHOLD = -161.0f/54.0f;
    private float note_hit_offset;
    private float sprite_half_height;
    private float note_top_side, note_bottom_side;
    public GameObject player;
    private PlayerBehaviour player_behaviour;
    // Start is called before the first frame update
    void Start()
    {
        player_behaviour = player.GetComponent<PlayerBehaviour>();
        sprite_half_height = GetComponent<SpriteRenderer>().bounds.extents.y;
        note_top_side = note_bottom_side = note_hit_offset = 0.0f;
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
        note_bottom_side = transform.position.y - sprite_half_height;
        if ( note_bottom_side <= NOTE_HIT_THRESHOLD)
        {
            if(Input.GetKeyDown(key))
            {
                if((int)lane == (int)player_behaviour.lane)
                {
                    Debug.Log("You hit the note");
                    note_hit_offset = Mathf.Abs(note_bottom_side - NOTE_MISSED_THRESHOLD);
                    transform.position = initial_position;
                    transform.position += new Vector3(3.0f * (int)lane, note_hit_offset, 0.0f);
                }
                else
                {
                    Debug.Log("Wrong lane idiot");
                }
            } 
            if (note_bottom_side <= NOTE_MISSED_THRESHOLD)
            {
                Debug.Log("You missed a note");
                transform.position = initial_position;
                transform.position += new Vector3(3.0f * (int)lane, 0.0f, 0.0f);
            }
        }
    }
}
