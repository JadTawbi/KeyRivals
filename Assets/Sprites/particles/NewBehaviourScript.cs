using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public ParticleSystem P1;
    public ParticleSystem P2;
    public ParticleSystem P3;


    void ActivateFirst()
    {
        P1.Play();
    }

    void ActivateSecond()
    {
        P2.Play();
    }

    void ActivateThird()
    {
        P3.Play();
    }

    void DeactivateBoth()
    {
        P1.Stop();
        P2.Stop();
        P3.Stop();
    }
}
