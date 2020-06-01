using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialBehaviour : MonoBehaviour
{
    public Sprite[] tutorial_sprites;
    public GameObject tutorial_display;
    private Image tutorial_image;
    private int current_sprite;
    public static bool practice = false;

    private void Start()
    {
        tutorial_image = tutorial_display.GetComponent<Image>();
        current_sprite = 0;
    }

    public void loadNextTutorial()
    {
        current_sprite++;
        if (current_sprite > tutorial_sprites.Count() - 1)
        {
            current_sprite = 0;
        }
        tutorial_image.sprite = tutorial_sprites[current_sprite];
    }

    public void loadPreviousTutorial()
    {
        current_sprite--;
        if (current_sprite < 0)
        {
            current_sprite = tutorial_sprites.Count() - 1;
        }
        tutorial_image.sprite = tutorial_sprites[current_sprite];
    }
}
