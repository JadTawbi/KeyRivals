using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animController : MonoBehaviour
{
    public Animator anim; 
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("z") || Input.GetKeyDown("x") || Input.GetKeyDown("c") || Input.GetKeyDown("v"))
        {
            //anim.Play("player1_shoot");
            anim.SetTrigger("keyPress");

        }
        
    }
}
