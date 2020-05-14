using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroDelay : MonoBehaviour
{
    public GameObject weapon;

    public float seconds;
  

    void Start()
    {
        StartCoroutine("wait");
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(seconds);
        weapon.SetActive(true);
    }
}
