using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokerChipBehaviour : MonoBehaviour
{
    public WeaponBehaviour.Lane lane;
    public WeaponBehaviour.Side side;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "PokerChip_" + side.ToString() + "_" + lane.ToString();
        transform.position = new Vector3((int)side * 150.5f, 225.0f - 150.0f * (int)lane, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
