using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEyeBehaviour : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0.0f, 100.0f) <= 0.01f)
        {
            animator.SetTrigger("startBlink");
        }
    }
}
