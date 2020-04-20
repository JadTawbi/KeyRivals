using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    private Vector3 scale_increase;
    public Transform filling_transform;
    // Start is called before the first frame update
    void Start()
    {
        scale_increase = new Vector3(0.001f, 0.001f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        growFilling();
    }

    void growFilling()
    {
        if(filling_transform.localScale.x < 1.0f || filling_transform.localScale.y < 1.0f)
        {
           filling_transform.localScale += scale_increase;
        }
        if(filling_transform.localScale.x > 1.0f || filling_transform.localScale.y > 1.0f)
        {
            filling_transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
    }
}
