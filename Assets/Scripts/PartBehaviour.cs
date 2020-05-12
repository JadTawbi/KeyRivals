using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PartBehaviour : MonoBehaviour
{
    private const float speed = 150.0f, floor_position = 815;

    public enum PartType { One = 1, Two = 2, Three = 3 };
    [System.NonSerialized]
    public PartType part_type;

    public Sprite type_one_sprite, type_two_sprite, type_three_sprite;
    public SpriteRenderer sprite_renderer;

    void Start()
    {

    }

    void Update()
    {
        movePart();
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
            case PartType.One:
                sprite_renderer.sprite = type_one_sprite;
                break;
            case PartType.Two:
                sprite_renderer.sprite = type_two_sprite;
                break;
            case PartType.Three:
                sprite_renderer.sprite = type_three_sprite;
                break;
        }

        part_type = new_type;
    }
}
