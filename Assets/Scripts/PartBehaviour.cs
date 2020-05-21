using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PartBehaviour : MonoBehaviour
{
    private const float speed = 150.0f, floor_position = 825;

    public enum PartType { Blue, Red, Yellow };
    [System.NonSerialized]
    public PartType part_type;

    public Sprite type_one_sprite, type_two_sprite, type_three_sprite;
    public SpriteRenderer sprite_renderer;

    void Start()
    {
        gameObject.name = part_type.ToString() + " Part";
    }

    void Update()
    {
        if (GameBehaviour.paused == false)
        {
            movePart();
        }
    }
    private void movePart()
    {
        transform.position += -transform.up * speed * Time.deltaTime;
        if(Mathf.Abs(transform.position.x)>= floor_position)
        {
            Destroy(gameObject);
        }
    }

    public void changeType(PartType new_type)
    {
        switch(new_type)
        {
            case PartType.Blue:
                sprite_renderer.sprite = type_one_sprite;
                break;
            case PartType.Red:
                sprite_renderer.sprite = type_two_sprite;
                break;
            case PartType.Yellow:
                sprite_renderer.sprite = type_three_sprite;
                break;
        }

        part_type = new_type;
    }
}
