using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShortcutManagement;
using UnityEngine;

public class BuilderBehaviour : MonoBehaviour
{
    private const int MAX_PARTS = 3;
    public GameObject[] parts = new GameObject[MAX_PARTS];
    private SpriteRenderer[] parts_sprite_renderer = new SpriteRenderer[MAX_PARTS];
    private PartBehaviour.PartType[] parts_type;
    public int part_count;

    public enum PowerUpType { Character = 0, One = 12, Two = 21, Three = 102, Four = 120, Five = 111, Six = 201, Seven = 210 };
    private PowerUpType power_up_type;
    private int[] part_type_amounts;

    public enum BuilderState { Collecting, PowerupReady };
    private BuilderState builder_state;

    void Start()
    {
        for (int i = 0; i < MAX_PARTS; i++)
        {
            parts_sprite_renderer[i] = parts[i].GetComponent<SpriteRenderer>();
        }
        initializeProperties();
        //To-do: Set PowerUPType.Character to a character specific script;
    }

    private void initializeProperties()
    {
        parts_type = new PartBehaviour.PartType[3];
        part_count = 0;
        part_type_amounts = new int[3] { 0, 0, 0 };
        builder_state = BuilderState.Collecting;
        power_up_type = PowerUpType.Character;
        for (int i = 0; i < MAX_PARTS; i++)
        {
            parts_sprite_renderer[i].sprite = null;
        }
    }

    void Update()
    {
        
    }

    public void addPart(Sprite new_part_sprite, PartBehaviour.PartType new_part_type)
    {
        if (builder_state == BuilderState.Collecting)
        {
            if (part_count < MAX_PARTS)
            {
                parts_sprite_renderer[part_count].sprite = new_part_sprite;
                parts_type[part_count] = new_part_type;
                part_type_amounts[(int)new_part_type]++;
                part_count++;
            }

            if (part_count == MAX_PARTS)
            {
                bool character_specific_powerup = false;
                for (int i = 0; i < MAX_PARTS; i++)
                {
                    if (part_type_amounts[i] == MAX_PARTS)
                    {
                        character_specific_powerup = true;
                    }
                }
                if (character_specific_powerup == true)
                {
                    power_up_type = PowerUpType.Character;
                }
                else
                {
                    int power_up_code = 0;
                    for (int i = 0; i < MAX_PARTS; i++)
                    {
                        power_up_code += part_type_amounts[i] * (int)(Mathf.Pow(10, (MAX_PARTS - 1) - i));
                    }
                    power_up_type = (PowerUpType)power_up_code;
                }

                Debug.Log(gameObject.name + " built a powerup of type " + power_up_type.ToString());

                builder_state = BuilderState.PowerupReady;

                //set active powerup
            }
        }
    }

    public void usePowerup()
    {
        //use powerup

        initializeProperties();
    }
}
