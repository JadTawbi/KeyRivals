using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShortcutManagement;
using UnityEngine;

public class BuilderBehaviour : MonoBehaviour
{
    public GameObject[] parts = new GameObject[3];
    private PartBehaviour.PartType[] parts_type = new PartBehaviour.PartType[3];
    private int part_count;
    private const int MAX_PARTS = 3;

    //1 2 3

    // (first*second + first*third + second*third) / (first squared + second squared + third squared) = unique number 
    /* 111 = a = 1+1+1 / 1+1+1 = 1
     * 112 = b = 1+2+2 / 1+1+4 = 5/6
     * 113 = c = 1+3+3 / 1+1+9 = 7/11
     * 121 = b = 2+1+2 / 1+4+1 = 5/6
     * 122 = d = 2+2+4 / 1+4+4 = 8/9
     * 123 = e = 2+3+6 / 1+4+9 = 11/14
     * 131 = c = 3+1+3 / 1+9+1 = 7/11
     * 132 = e = 3+2+6 / 1+9+4 = 11/14
     * 133 = f = 3+3+9 / 1+9+9 = 15/19
     * 211 = b = 2+2+1 / 4+1+1 = 5/6
     * 212 = d = 2+4+2 / 4+1+4 = 8/9
     * 213 = e = 2+6+3 / 4+1+9 = 11/14
     * 221 = d = 4+2+2 / 4+4+1 = 8/9
     * 222 = a = 4+4+4 / 4+4+4 = 1
     * 223 = g = 4+6+6 / 4+4+9 = 16/17
     * 231 = e = 6+2+3 / 4+9+1 = 11/14
     * 232 = g = 6+4+6 / 4+9+4 = 16/17
     * 233 = h = 6+6+9 / 4+9+9 = 21/22
     * 311 = c = 3+3+1 / 9+1+1 = 7/11
     * 312 = e = 3+6+2 / 9+1+4 = 11/14
     * 313 = f = 3+9+3 / 9+1+9 = 15/19
     * 321 = e = 6+3+2 / 9+4+1 = 11/14
     * 322 = g = 6+6+4 / 9+4+4 = 16/17
     * 323 = h = 6+9+6 / 9+4+9 = 21/22
     * 331 = f = 9+3+3 / 9+9+1 = 15/19
     * 332 = h = 9+6+6 / 9+9+4 = 21/22
     * 333 = a = 9+9+9 / 9+9+9 = 1
     * 
     * a=1, b=5/6, c=7/11, d=8/9, e=11/14, f=15/19, g=16/17, h=21/22
     */


    public enum PowerupType { A}

    void Start()
    {
        part_count = 0;
    }

    void Update()
    {
        
    }

    public void addPart(Sprite new_part_sprite, PartBehaviour.PartType new_part_type)
    {
        if (part_count < MAX_PARTS)
        {
            parts[part_count].GetComponent<SpriteRenderer>().sprite = new_part_sprite;
            parts_type[part_count] = new_part_type;
            parts[part_count].SetActive(true);
            part_count++;
        }

        if (part_count == 3)
        {
            //Build powerup
        }
    }
}
