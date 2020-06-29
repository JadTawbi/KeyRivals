using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderBehaviour : MonoBehaviour
{
    private const int MAX_PARTS = 3;
    public GameObject[] parts = new GameObject[MAX_PARTS];
    private SpriteRenderer[] parts_sprite_renderer = new SpriteRenderer[MAX_PARTS];
    private PartBehaviour.PartType[] parts_type;
    public int part_count;

    public enum PowerUpType { None = -1, Character = 0, One = 12, Two = 21, Three = 102, Four = 120, Five = 111, Six = 201, Seven = 210 };
    private PowerUpType power_up_type;
    private int[] part_type_amounts;

    public enum BuilderState { Collecting, PowerupReady };
    private BuilderState builder_state;

    private float power_up_timer, power_up_timer_interval;
    private bool timer_running;

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
        power_up_type = PowerUpType.None;
        for (int i = 0; i < MAX_PARTS; i++)
        {
            parts_sprite_renderer[i].sprite = null;
        }
        power_up_timer = power_up_timer_interval = 0.0f;
        timer_running = false;
    }

    void Update()
    {
        runTimer();
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
                    //power_up_type = PowerUpType.One;
                }

                Debug.Log(gameObject.name + " built a powerup of type " + power_up_type.ToString());

                builder_state = BuilderState.PowerupReady;
            }
        }
    }

    public void usePowerup()
    {
        if (builder_state == BuilderState.PowerupReady)
        {
            switch (power_up_type)
            {
                case PowerUpType.None:
                    break;
                case PowerUpType.Character:
                    break;
                case PowerUpType.One:
                    PlayerBehaviour.movement_locked = false;
                    startTimer(5.0f);
                    break;
                case PowerUpType.Two:
                    break;
                case PowerUpType.Three:
                    break;
                case PowerUpType.Four:
                    break;
                case PowerUpType.Five:
                    break;
                case PowerUpType.Six:
                    break;
                case PowerUpType.Seven:
                    break;
                default:
                    break;
            }
        }
        else
        {
            Debug.Log("Power Up not ready");
        }
    }
    private void endPowerUpEffect()
    {
        switch (power_up_type)
        {
            case PowerUpType.None:
                break;
            case PowerUpType.Character:
                break;
            case PowerUpType.One:
                PlayerBehaviour.movement_locked = true;
                break;
            case PowerUpType.Two:
                break;
            case PowerUpType.Three:
                break;
            case PowerUpType.Four:
                break;
            case PowerUpType.Five:
                break;
            case PowerUpType.Six:
                break;
            case PowerUpType.Seven:
                break;
            default:
                break;
        }
        initializeProperties();
    }
    private void startTimer(float timer_duration)
    {
        timer_running = true;
        power_up_timer = 0;
        power_up_timer_interval = timer_duration;
    }
    private void runTimer()
    {
        if (timer_running == true)
        {
            if (power_up_timer < power_up_timer_interval)
            {
                power_up_timer += Time.deltaTime;
            }
            else
            {
                timer_running = false;
                endPowerUpEffect();
            }
        }
    }
}
