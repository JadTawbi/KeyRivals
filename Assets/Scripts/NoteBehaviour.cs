using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBehaviour : MonoBehaviour
{
    private Vector2 initial_position = new Vector2(-4.45f, 4.55f);
    private const float SPEED = 0.01f;
    private const float NOTE_HIT_THRESHOLD = 1.0f;
    private const float NOTE_MISSED_THRESHOLD = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move();
        checkHitMiss();
    }

    void move()
    {
        transform.position += -transform.up * SPEED;
    }

    void checkHitMiss()
    {
        if (transform.position.y <= NOTE_HIT_THRESHOLD)
        {
            if(Input.GetKeyDown(KeyCode.C))
            {
                Debug.Log("You hit the note");
                transform.position = initial_position;
                transform.position += transform.right * 1.75f;
            }
            if (transform.position.y <= NOTE_MISSED_THRESHOLD)
            {
                Debug.Log("You missed a note");
                transform.position = initial_position;
            }
        }
    }
}
