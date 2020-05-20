using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
    private const int TUTORIAL_LENGTH = 3;
    public Sprite[] tutorial_sprites = new Sprite[TUTORIAL_LENGTH];
    public GameObject tutorial_display;
    private Image tutorial_image;
    private int current_sprite;

    private void Start()
    {
        tutorial_image = tutorial_display.GetComponent<Image>();
        current_sprite = 0;
    }

    public void loadNextTutorial()
    {
        current_sprite++;
        if (current_sprite > TUTORIAL_LENGTH - 1)
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
            current_sprite = TUTORIAL_LENGTH - 1;
        }
        tutorial_image.sprite = tutorial_sprites[current_sprite];
    }
}
