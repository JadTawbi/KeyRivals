using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    private Vector3 scale_increase;
    private SpriteRenderer sprite_renderer;
    public enum Lane { First, Second, Third, Fourth };
    public Lane lane;
    public enum Type { Red, Green, Blue, Yellow };
    public Type type;
    private KeyCode key;

    public enum Side { Player1 = -1, Player2 = 1};
    public Side side;

    private GameObject player;
    private PlayerBehaviour player_behaviour;

    // Start is called before the first frame update
    void Start()
    {
        switch(side)
        {
            case Side.Player1:
                player = GameObject.FindWithTag("Player 1");
                break;

            case Side.Player2:
                player = GameObject.FindWithTag("Player 2");
                break;
        }

        player_behaviour = player.GetComponent<PlayerBehaviour>();
        scale_increase = new Vector3(0.001f, 0.001f, 0.0f);
        sprite_renderer = GetComponent<SpriteRenderer>();

        transform.position = new Vector3((int)side * 150.0f, 225.0f - 150.0f * (int)lane, 0.0f);
        /* To calculate x position: side enum is set to either -1 or 1 and then used in the calculation
         * To calculate y position: lane enum is cast into an int and is used to calculate how far down from the first lane its position is going to be.*/

        switch(type)
        {
            case Type.Red:
                sprite_renderer.color = new Color(1, 0, 0);
                switch(side)
                {
                    case Side.Player1:
                        key = KeyCode.Z;
                        break;

                    case Side.Player2:
                        key = KeyCode.H;
                        break;
                }
                break;

            case Type.Green:
                sprite_renderer.color = new Color(0, 1, 0);
                switch (side)
                {
                    case Side.Player1:
                        key = KeyCode.X;
                        break;

                    case Side.Player2:
                        key = KeyCode.J;
                        break;
                }
                break;

            case Type.Blue:
                sprite_renderer.color = new Color(0, 0, 1);
                switch (side)
                {
                    case Side.Player1:
                        key = KeyCode.C;
                        break;

                    case Side.Player2:
                        key = KeyCode.K;
                        break;
                }
                break;

            case Type.Yellow:
                sprite_renderer.color = new Color(1, 1, 0);
                switch (side)
                {
                    case Side.Player1:
                        key = KeyCode.V;
                        break;

                    case Side.Player2:
                        key = KeyCode.L;
                        break;
                }
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        growFilling();
        checkHitMiss();
    }

    void growFilling()
    {
        if(transform.localScale.x < 1.0f || transform.localScale.y < 1.0f)
        {
           transform.localScale += scale_increase;
        }
        if(transform.localScale.x > 1.0f || transform.localScale.y > 1.0f)
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
    }

    void checkHitMiss()
    {
        if(Input.GetKeyDown(key))
        {
            if((int)lane == (int)player_behaviour.lane)
            {
                transform.localScale = new Vector3(0.0f, 0.0f, 1.0f);
                Debug.Log(side+" disabled a " + type + " weapon!!");
            }
            else
            {
                Debug.Log("Wrong lane idiot");
            }
        }
        //continue here (else if)
        if(transform.localScale.x >= 1.0f || transform.localScale.y >= 1.0f)
        {
            transform.localScale = new Vector3(0.0f, 0.0f, 1.0f);
            Debug.Log(type + " weapon shot " + side);
        }
    }
}
