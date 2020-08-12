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

    public PlayerBehaviour player_behaviour;
    private PlayerBehaviour.PlayerCharacter player_character;
    public ScoreBehaviour score_behaviour_own, score_behaviour_rival;
    public HealthBehaviour health_behaviour;
    public WeaponBehaviour.Side side;
    public WeaponSpawnerBehaviour weapon_spawner_behaviour;
    public GameObject poker_chip;
    private GameObject new_poker_chip;

    private bool reset_builder_properties;
    void Start()
    {
        for (int i = 0; i < MAX_PARTS; i++)
        {
            parts_sprite_renderer[i] = parts[i].GetComponent<SpriteRenderer>();
        }
        initializeProperties();
        reset_builder_properties = true;
    }

    public void initializeProperties()
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
        player_character = player_behaviour.getPlayerCharacter();
        new_poker_chip = null;
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
                    //power_up_type = (PowerUpType)power_up_code;
                    power_up_type = PowerUpType.Character; //Uncomment to make all powerups be the same type
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
                    switch (player_character)
                    {
                        case PlayerBehaviour.PlayerCharacter.BassFisher:
                            reset_builder_properties = false;
                            BuilderBehaviour rival_builder_behaviour = GameObject.FindGameObjectWithTag("Builder_"+ ((WeaponBehaviour.Side)(-(int)(side))).ToString()).GetComponent<BuilderBehaviour>();
                            power_up_type = rival_builder_behaviour.getPowerUpType();
                            if (power_up_type == PowerUpType.None)
                            {
                                reset_builder_properties = true;
                            }
                            else if (power_up_type == PowerUpType.Character)
                            {
                                player_character = rival_builder_behaviour.getPlayerCharacter();
                            }
                            rival_builder_behaviour.initializeProperties();
                            startTimer(0.0f);
                            break;
                        case PlayerBehaviour.PlayerCharacter.BigLightBeam:
                            break;
                        case PlayerBehaviour.PlayerCharacter.CrownJules:
                            break;
                        case PlayerBehaviour.PlayerCharacter.HotDogg:
                            break;
                        case PlayerBehaviour.PlayerCharacter.Jojitsu:
                            break;
                        case PlayerBehaviour.PlayerCharacter.LadyGooGooGaGa:
                            weapon_spawner_behaviour.poker_chip_active = true;
                            new_poker_chip = Instantiate(poker_chip, transform);
                            PokerChipBehaviour new_poker_chip_behaviour = new_poker_chip.GetComponent<PokerChipBehaviour>();
                            new_poker_chip_behaviour.side = (WeaponBehaviour.Side)(-(int)side);
                            new_poker_chip_behaviour.lane = (WeaponBehaviour.Lane)(Random.Range(0, 4));
                            startTimer(8.0f);
                            break;
                        case PlayerBehaviour.PlayerCharacter.Powerdog:
                            break;
                    }
                    break;
                case PowerUpType.One:
                    player_behaviour.movement_locked = false;
                    startTimer(16.0f);
                    break;
                case PowerUpType.Two:
                    score_behaviour_own.multiplyMultiplierBy(2.0f);
                    startTimer(0.0f);
                    break;
                case PowerUpType.Three:
                    score_behaviour_rival.multiplyMultiplierBy(0.5f);
                    startTimer(0.0f);
                    break;
                case PowerUpType.Four:
                    switch (side)
                    {
                        case WeaponBehaviour.Side.Player1:
                            WeaponSpawnerBehaviour.color_locked_player1 = true;
                            break;
                        case WeaponBehaviour.Side.Player2:
                            WeaponSpawnerBehaviour.color_locked_player2 = true;
                            break;
                    }
                    startTimer(8.0f);
                    break;
                case PowerUpType.Five:
                    health_behaviour.health_locked = true;
                    score_behaviour_own.multiplier_locked = true;
                    startTimer(8.0f);
                    break;
                case PowerUpType.Six:
                    health_behaviour.resetHealth();
                    score_behaviour_own.increaseMultiplierBy(3);
                    startTimer(0.0f);
                    break;
                case PowerUpType.Seven:
                    switch (side)
                    {
                        case WeaponBehaviour.Side.Player1:
                            WeaponSpawnerBehaviour.color_random_player2 = true;
                            break;
                        case WeaponBehaviour.Side.Player2:
                            WeaponSpawnerBehaviour.color_random_player1 = true;
                            break;
                    }
                    startTimer(8.0f);
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
                switch (player_character)
                {
                    case PlayerBehaviour.PlayerCharacter.BassFisher:
                        break;
                    case PlayerBehaviour.PlayerCharacter.BigLightBeam:
                        break;
                    case PlayerBehaviour.PlayerCharacter.CrownJules:
                        break;
                    case PlayerBehaviour.PlayerCharacter.HotDogg:
                        break;
                    case PlayerBehaviour.PlayerCharacter.Jojitsu:
                        break;
                    case PlayerBehaviour.PlayerCharacter.LadyGooGooGaGa:
                        weapon_spawner_behaviour.poker_chip_active = false;
                        Destroy(new_poker_chip);
                        break;
                    case PlayerBehaviour.PlayerCharacter.Powerdog:
                        break;
                }
                break;
            case PowerUpType.One:
                player_behaviour.movement_locked = true;
                break;
            case PowerUpType.Two:
                break;
            case PowerUpType.Three:
                break;
            case PowerUpType.Four:
                switch (side)
                {
                    case WeaponBehaviour.Side.Player1:
                        WeaponSpawnerBehaviour.color_locked_player1 = false;
                        break;
                    case WeaponBehaviour.Side.Player2:
                        WeaponSpawnerBehaviour.color_locked_player2 = false;
                        break;
                }
                break;
            case PowerUpType.Five:
                health_behaviour.health_locked = false;
                score_behaviour_own.multiplier_locked = false;
                break;
            case PowerUpType.Six:
                break;
            case PowerUpType.Seven:
                switch (side)
                {
                    case WeaponBehaviour.Side.Player1:
                        WeaponSpawnerBehaviour.color_random_player2 = false;
                        break;
                    case WeaponBehaviour.Side.Player2:
                        WeaponSpawnerBehaviour.color_random_player1 = false;
                        break;
                }
                break;
        }
        if (reset_builder_properties)
        {
            initializeProperties();
        }
        reset_builder_properties = true;
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

    public PowerUpType getPowerUpType()
    {
        return power_up_type;
    }
    public PlayerBehaviour.PlayerCharacter getPlayerCharacter()
    {
        return player_character;
    }
}
