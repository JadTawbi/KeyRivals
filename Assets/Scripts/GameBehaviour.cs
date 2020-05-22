using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBehaviour : MonoBehaviour
{
    public static bool paused = false;

    public GameObject weapon_spawner;
    private AudioSource weapon_spawner_audio_source;

    public GameObject[] game_objects_with_animation;
    private Animator[] animators;

    public GameObject pause_canvas, pause_menu, options_menu;

    private void Start()
    {
        weapon_spawner_audio_source = weapon_spawner.GetComponent<AudioSource>();

        animators = new Animator[game_objects_with_animation.Length];
        int i = 0;
        foreach(GameObject game_object in game_objects_with_animation)
        {
            animators[i] = game_object.GetComponent<Animator>();
            i++;
        }
    }

    private void Update()
    {
        checkInput();
    }

    private void checkInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            togglePause();
        }
    }
    public void togglePause()
    {
        if(paused == false)
        {
            weapon_spawner_audio_source.Pause();
            foreach (Animator animator in animators)
            {
                animator.speed = 0;
            }
            pause_canvas.SetActive(true);
            pause_menu.SetActive(true);
            options_menu.SetActive(false);
            paused = true;
        }
        else
        {
            weapon_spawner_audio_source.UnPause();
            foreach (Animator animator in animators)
            {
                animator.speed = 1;
            }
            pause_canvas.SetActive(false);
            paused = false;
        }
    }

    public void quitToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
