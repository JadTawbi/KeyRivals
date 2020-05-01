using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBehaviour : MonoBehaviour
{
    public GameObject hp1, hp2, hp3;
    [System.NonSerialized]
    public int hit_points;
    // Start is called before the first frame update
    void Start()
    {
        hit_points = 3;
    }

    // Update is called once per frame
    void Update()
    {
        checkHitPoints();
    }

    private void checkHitPoints()
    {
        if (hit_points < 3)
        {
            if (hp3.activeSelf == true)
            {
                hp3.SetActive(false);
            }
            if (hit_points < 2)
            {
                if (hp2.activeSelf == true)
                {
                    hp2.SetActive(false);
                }
                if (hit_points < 1)
                {
                    if (hp1.activeSelf == true)
                    {
                        hp1.SetActive(false);
                    }
                }
            }
        }
    }

    public void loseHealth()
    {
        hit_points--;

        if (hit_points < 0)
        {
            hit_points = 0;
        }
        else if (hit_points > 3)
        {
            hit_points = 3;
        }
    }
}
