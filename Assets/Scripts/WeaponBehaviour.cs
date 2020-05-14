using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    private SpriteRenderer sprite_renderer;

    public enum Lane { First, Second, Third, Fourth };
    public Lane lane;

    public enum AttackColour { Red, Green, Blue, Yellow, Bad};
    public AttackColour attack_colour;
    private Color red, green, blue, yellow, bad;
    private KeyCode input_key;
    private KeyCode red_key, green_key, blue_key, yellow_key;

    public enum Side { Player1 = -1, Player2 = 1};
    public Side side;

    private GameObject player;
    private PlayerBehaviour player_behaviour;
    private float stay_charged_interval, stay_charged_timer;
    [System.NonSerialized]
    public float grow_timer, grow_interval;

    private HealthBehaviour health_behaviour;
    private ScoreBehaviour score_behaviour;

    public enum WeaponState { GrowingUnderThreshold, GrowingOverThreshold, Charging, Shooting };
    [System.NonSerialized]
    public WeaponState weapon_state;

    private const float HIT_SCORE = 100.0f;
    private const float HIT_LOWER_THRESHOLD = 0.65f;

    public GameObject part;
    private const int PART_DROP_CHANCE = 100;
    private Transform parts_parent;

    // Start is called before the first frame update
    void Start()
    {
        stay_charged_interval = grow_interval/4;
        stay_charged_timer = 0.0f;

        red = new Color(1.0f, 0.153f, 0.0f);
        green = new Color(0.0f, 1.0f, 0.431f);
        blue = new Color(0.212f, 0.929f, 0.871f);
        yellow = new Color(0.969f, 1.0f, 0.0f);
        bad = new Color(0.60f, 0, 0.80f);

        initializeProperties();

        if (attack_colour == AttackColour.Bad)
        {
            grow_timer = GetComponentInParent<WeaponSpawnerBehaviour>().spawn_offset;
            weapon_state = WeaponState.Charging;
        }
        else
        {
            grow_timer = 0.0f;
            weapon_state = WeaponState.GrowingOverThreshold;
        }

        parts_parent = GameObject.FindWithTag("Parts Container").transform;
    }

    void initializeProperties()
    {
        switch (side)
        {
            case Side.Player1:
                player = GameObject.FindWithTag("Player1");
                health_behaviour = GameObject.Find("Health Player1").GetComponent<HealthBehaviour>();
                score_behaviour = GameObject.Find("Score Player1").GetComponent<ScoreBehaviour>();
                red_key = KeyCode.Z;
                green_key = KeyCode.X;
                blue_key = KeyCode.C;
                yellow_key = KeyCode.V;
                break;

            case Side.Player2:
                player = GameObject.FindWithTag("Player2");
                health_behaviour = GameObject.Find("Health Player2").GetComponent<HealthBehaviour>();
                score_behaviour = GameObject.Find("Score Player2").GetComponent<ScoreBehaviour>();
                red_key = KeyCode.H;
                green_key = KeyCode.J;
                blue_key = KeyCode.K;
                yellow_key = KeyCode.L;
                break;
        }

        player_behaviour = player.GetComponent<PlayerBehaviour>();
        sprite_renderer = GetComponent<SpriteRenderer>();

        transform.position = new Vector3((int)side * 150.5f, 225.0f - 150.0f * (int)lane, 0.0f);
        /* To calculate x position: side enum is set to either -1 or 1 and then used in the calculation
         * To calculate y position: lane enum is cast into an int and is used to calculate how far down from the first lane its position is going to be.*/

        switch (attack_colour)
        {
            case AttackColour.Red:
                sprite_renderer.color = red;
                input_key = red_key;
                break;

            case AttackColour.Green:
                sprite_renderer.color = green;
                input_key = green_key;
                break;

            case AttackColour.Blue:
                sprite_renderer.color = blue;
                input_key = blue_key;
                break;

            case AttackColour.Yellow:
                sprite_renderer.color = yellow;
                input_key = yellow_key;
                break;
            case AttackColour.Bad:
                sprite_renderer.color = bad;
                input_key = KeyCode.None;
                break;
        }
        gameObject.name = "Weapon_" + player.tag + "_" + lane.ToString();
        gameObject.tag = "Weapon_" + player.tag + "_" + lane.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        checkWeaponState();

        if (player_behaviour.player_state == PlayerBehaviour.PlayerState.Alive)
        {
            checkHitMiss();
        }
        growFilling();
    }

    void checkWeaponState()
    {
        if (grow_timer < grow_interval * HIT_LOWER_THRESHOLD)
        {
            if (weapon_state != WeaponState.GrowingUnderThreshold)
            {
                weapon_state = WeaponState.GrowingUnderThreshold;
            }
        }
        else if (grow_timer < grow_interval)
        {
            if (weapon_state != WeaponState.GrowingOverThreshold)
            {
                weapon_state = WeaponState.GrowingOverThreshold;
            }
        }
        else if (stay_charged_timer < stay_charged_interval)
        {
            if (weapon_state != WeaponState.Charging)
            {
                weapon_state = WeaponState.Charging;
            }
        }
        else
        {
            if (weapon_state != WeaponState.Shooting)
            {
                weapon_state = WeaponState.Shooting;
            }
        }
    }

    void growFilling()
    {
        switch (weapon_state)
        {
            case WeaponState.GrowingUnderThreshold:
                grow_timer += Time.deltaTime;
                transform.localScale = new Vector3(grow_timer / grow_interval, grow_timer / grow_interval, 1.0f);
                if (stay_charged_timer != 0.0f)
                {
                    stay_charged_timer = 0.0f;
                }
                break;
            case WeaponState.GrowingOverThreshold:
                grow_timer += Time.deltaTime;
                transform.localScale = new Vector3(grow_timer / grow_interval, grow_timer / grow_interval, 1.0f);
                if (stay_charged_timer != 0.0f)
                {
                    stay_charged_timer = 0.0f;
                }
                break;
            case WeaponState.Charging:
                stay_charged_timer += Time.deltaTime;
                if (transform.localScale.x != 1.0f || transform.localScale.y != 1.0f)
                {
                    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                }
                float red_component, green_component, blue_component;
                switch (attack_colour)
                {
                    case AttackColour.Red:
                        red_component = 1.0f - (1.0f - 0.6f) * (stay_charged_timer / stay_charged_interval);
                        green_component = 0.153f - (0.153f) * (stay_charged_timer / stay_charged_interval);
                        blue_component = 0.8f * (stay_charged_timer / stay_charged_interval);
                        break;
                    case AttackColour.Green:
                        red_component = (0.6f) * (stay_charged_timer / stay_charged_interval);
                        green_component = 1.0f - (stay_charged_timer / stay_charged_interval);
                        blue_component = 0.431f + (0.8f - 0.431f) * (stay_charged_timer / stay_charged_interval);
                        break;
                    case AttackColour.Blue:
                        red_component = 0.212f + (0.6f - 0.212f) * (stay_charged_timer / stay_charged_interval);
                        green_component = 0.929f - (0.929f) * (stay_charged_timer / stay_charged_interval);
                        blue_component = 0.871f - (0.871f - 0.8f) * (stay_charged_timer / stay_charged_interval);
                        break;
                    case AttackColour.Yellow:
                        red_component = 0.969f - (0.969f - 0.6f) * (stay_charged_timer / stay_charged_interval);
                        green_component = 1.0f - (stay_charged_timer / stay_charged_interval);
                        blue_component = 0.8f * (stay_charged_timer / stay_charged_interval);
                        break;
                    case AttackColour.Bad:
                        red_component = 0.6f;
                        green_component = 0.0f;
                        blue_component = 0.8f;
                        break;
                    default:
                        red_component = 0.0f;
                        green_component = 0.0f;
                        blue_component = 0.0f;
                        break;
                }
                sprite_renderer.color = new Color(red_component, green_component, blue_component);
                break;
            case WeaponState.Shooting:
                //Debug.Log(attack_colour + " weapon shot " + side);
                health_behaviour.loseHealth();
                score_behaviour.resetStreak();
                Destroy(gameObject);
                break;
        }
    }

    void checkHitMiss()
    {
        if (weapon_state != WeaponState.GrowingUnderThreshold)
        {
            if (lane == player_behaviour.lane)
            {
                GameObject[] weapons_in_lane = GameObject.FindGameObjectsWithTag(gameObject.tag);
                bool biggest_in_lane = true;
                if (weapons_in_lane.Length > 1)
                {
                    for (int i = 0; i < weapons_in_lane.Length; i++)
                    {
                        if (gameObject.GetInstanceID() != weapons_in_lane[i].GetInstanceID())
                        {
                            WeaponBehaviour other_weapon_behaviour = weapons_in_lane[i].GetComponent<WeaponBehaviour>();
                            if (grow_timer + stay_charged_timer < other_weapon_behaviour.grow_timer + other_weapon_behaviour.stay_charged_timer)
                            {
                                biggest_in_lane = false;
                            }
                        }
                    }
                }

                if (biggest_in_lane == true)
                {
                    if (Input.GetKeyDown(input_key))
                    {
                        Debug.Log(side + " disabled a " + attack_colour + " weapon!!");
                        switch (weapon_state)
                        {
                            case WeaponState.GrowingOverThreshold:
                                score_behaviour.addScore((int)(HIT_SCORE * grow_timer / grow_interval));
                                break;
                            case WeaponState.Charging:
                                score_behaviour.addScore((int)(HIT_SCORE - (HIT_SCORE / 2) * (stay_charged_timer / stay_charged_interval)));
                                break;
                        }
                        if (Random.Range(0.0f, 100.0f) <= PART_DROP_CHANCE)
                        {
                            Vector3 new_part_position = transform.position + new Vector3(104.0f * (int)side, 0.0f, 0.0f);
                            Quaternion new_part_rotation = Quaternion.Euler(0.0f, 0.0f, 90.0f * (int)side);
                            PartBehaviour new_part_behaviour = Instantiate(part, new_part_position, new_part_rotation, parts_parent).GetComponent<PartBehaviour>();
                            new_part_behaviour.changeType((PartBehaviour.PartType)Random.Range(0, 3));
                        }

                        Destroy(gameObject);

                    }
                    else if (Input.GetKeyDown(red_key) || Input.GetKeyDown(green_key) || Input.GetKeyDown(blue_key) || Input.GetKeyDown(yellow_key))
                    {
                        grow_timer = grow_interval;
                        sprite_renderer.color = bad;
                        attack_colour = AttackColour.Bad;
                        input_key = KeyCode.None;
                        Debug.Log(side + " shot a weapon with the wrong attack colour");
                    }
                }
            }
        }
    }
}
