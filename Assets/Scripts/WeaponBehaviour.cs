using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// take care of Delta time on scale increase
public class WeaponBehaviour : MonoBehaviour
{
    private SpriteRenderer sprite_renderer;
    public enum Lane { First, Second, Third, Fourth };
    public Lane lane;
    public enum WeaponType { Red, Green, Blue, Yellow, Bad};
    public WeaponType weapon_type;
    private Color red, green, blue, yellow, bad;
    private KeyCode input_key;
    private KeyCode red_key, green_key, blue_key, yellow_key;

    public enum Side { Player1 = -1, Player2 = 1};
    public Side side;

    private GameObject player;
    private PlayerBehaviour player_behaviour;
    private float grow_interval, grow_timer_offset, stay_charged_interval, stay_charged_timer, stay_charged_offset;
    public float grow_timer;

    //For testing
    public int vertical_half;
    private GameObject score;
    private Score score_script;
    private bool weapon_got_disabled = false;
    private bool weapon_retaliated = false;

    // Start is called before the first frame update
    void Start()
    {
        grow_interval = 1.0f;
        /*grow_timer =*/ grow_timer_offset = 0.0f;
        stay_charged_interval = grow_interval/2;
        stay_charged_timer = 0.0f;

        red = new Color(1, 0, 0);
        green = new Color(0, 1, 0);
        blue = new Color(0, 0, 1);
        yellow = new Color(1, 1, 0);
        bad = new Color(0.60f, 0, 0.80f);

        initializeCharacteristics();

        /* FOR TESTING */
        score = GameObject.FindGameObjectWithTag("Score");
        score_script = score.GetComponent<Score>();
        /* END */
    }

    void initializeCharacteristics()
    {
        switch (side)
        {
            case Side.Player1:
                player = GameObject.FindWithTag("Player1");
                red_key = KeyCode.Z;
                green_key = KeyCode.X;
                blue_key = KeyCode.C;
                yellow_key = KeyCode.V;
                break;

            case Side.Player2:
                player = GameObject.FindWithTag("Player2");
                red_key = KeyCode.H;
                green_key = KeyCode.J;
                blue_key = KeyCode.K;
                yellow_key = KeyCode.L;
                break;
        }

        player_behaviour = player.GetComponent<PlayerBehaviour>();
        sprite_renderer = GetComponent<SpriteRenderer>();

        transform.position = new Vector3((int)side * 150.0f, 225.0f - 150.0f * (int)lane, 0.0f);
        /* To calculate x position: side enum is set to either -1 or 1 and then used in the calculation
         * To calculate y position: lane enum is cast into an int and is used to calculate how far down from the first lane its position is going to be.*/

        switch (weapon_type)
        {
            case WeaponType.Red:
                sprite_renderer.color = red;
                input_key = red_key;
                break;

            case WeaponType.Green:
                sprite_renderer.color = green;
                input_key = green_key;
                break;

            case WeaponType.Blue:
                sprite_renderer.color = blue;
                input_key = blue_key;
                break;

            case WeaponType.Yellow:
                sprite_renderer.color = yellow;
                input_key = yellow_key;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        checkHitMiss();
        growFilling();
    }

    void randomizeCharacteristics()
    {
        lane = (Lane)(Random.Range(0, 2) + vertical_half * 2);
        weapon_type = (WeaponType)Random.Range(0, 4);
        gameObject.name = "Weapon_" + player.tag + "_" + lane.ToString();
        initializeCharacteristics();
    }

    void growFilling()
    {
        if (grow_timer < grow_interval) //Weapon is growing
        {
            grow_timer += Time.deltaTime;
            transform.localScale = new Vector3(Mathf.Max(grow_timer, 0.0f) / grow_interval, Mathf.Max(grow_timer, 0.0f) / grow_interval, 1.0f);
            if ( stay_charged_timer != 0.0f)
            {
                stay_charged_timer = 0.0f;
            }
            if ( grow_timer_offset != 0.0f)
            {
                grow_timer_offset = 0.0f;
            }
        }
        else if (stay_charged_timer< stay_charged_interval) //Weapon has grown and is charging
        {
            stay_charged_timer += Time.deltaTime;
            if(transform.localScale.x != 1.0f || transform.localScale.y != 1.0f)
            {
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
        }
        else //Weapon has fired
        {
            transform.localScale = new Vector3(0.0f, 0.0f, 1.0f);
            Debug.Log(weapon_type + " weapon shot " + side);

            grow_timer = 0.0f - grow_timer_offset;
            weapon_retaliated = false;

            /* FOR TESTING*/
            if (weapon_got_disabled == false)
            {
                switch (player.tag)
                {
                    case "Player1":
                        score_script.player1_misses++;
                        break;
                    case "Player2":
                        score_script.player2_misses++;
                        break;
                }
            }
            weapon_got_disabled = false;
            randomizeCharacteristics();
            /* END */
        }
    }

    void checkHitMiss()
    {
        if (Input.GetKeyDown(input_key) && weapon_retaliated == false && grow_timer >= 0.0f) //To-Do: Lower threshold and consequences
        {
            if((int)lane == (int)player_behaviour.lane)
            {
                grow_timer_offset = grow_interval - Mathf.Max(grow_timer, 0.0f);
                stay_charged_offset = stay_charged_interval - stay_charged_timer; //Just for testing
                grow_timer = 0 - grow_timer_offset - stay_charged_offset;   //stay_charged_offset is just for testing
                Debug.Log(side+" disabled a " + weapon_type + " weapon!!");

                /* FOR TESTING*/
                switch (player.tag)
                {
                    case "Player1":
                        score_script.player1_hits++;
                        break;
                    case "Player2":
                        score_script.player2_hits++;
                        break;
                }                    
                weapon_got_disabled = true;
                /* END */

                randomizeCharacteristics();
            }
        }
        else if ((Input.GetKeyDown(red_key) || Input.GetKeyDown(green_key) || Input.GetKeyDown(blue_key) || Input.GetKeyDown(yellow_key)) && weapon_retaliated == false && grow_timer >= 0.0f)
        {
            if ((int)lane == (int)player_behaviour.lane)
            {
                grow_timer_offset = grow_interval - Mathf.Max(grow_timer, 0.0f);
                sprite_renderer.color = bad;
                grow_timer = grow_interval + stay_charged_interval; //stay_charged_interval is just for testing
                Debug.Log(side + " might be colorblind (sorry if you actually are). Wrong color! ");
                weapon_retaliated = true;
            }
        }
        else if ((Input.GetKeyDown(red_key) || Input.GetKeyDown(green_key) || Input.GetKeyDown(blue_key) || Input.GetKeyDown(yellow_key)) && weapon_retaliated == false)
        {
            switch (player.tag)
            {
                case "Player1":
                    score_script.player1_misses++;
                    break;
                case "Player2":
                    score_script.player2_misses++;
                    break;
            }
        }
    }
}
