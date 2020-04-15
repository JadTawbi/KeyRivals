using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    [System.NonSerialized]
    public int notes_hit, notes_missed;


    void Start()
    {
        notes_hit = notes_missed = 0;
    }


    void Update()
    {
        
    }
}
