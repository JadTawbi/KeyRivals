using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartBehaviour : MonoBehaviour
{
    private const float speed = 150.0f, floor_position = 815;

    void Start()
    {
        
    }

    void Update()
    {
        movePart();
    }
    private void movePart()
    {
        transform.position += -transform.up * speed * Time.deltaTime;
        if(Mathf.Abs(transform.position.x)>= floor_position)
        {
            Destroy(gameObject);
        }
    }
}
