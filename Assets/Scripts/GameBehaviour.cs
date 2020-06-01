using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBehaviour : MonoBehaviour
{
    public static bool paused = false;

    public GameObject weapon_spawner;
    private WeaponSpawnerBehaviour weapon_spawner_behaviour;

    public GameObject[] game_objects_with_animation;
    private Animator[] animators;
    private float[] animation_speeds;

    public GameObject pause_canvas, pause_menu, options_menu;
    private OptionsMenuBehaviour options_menu_behaviour;

    private float clapping_timer;
    public const float CLAPPING_INTERVAL = 4.0f;
    private bool clapping_done;

    public AudioSource audio_source;
    private AudioClip clapping_audio_clip;

    private void Start()
    {
        weapon_spawner_behaviour = weapon_spawner.GetComponent<WeaponSpawnerBehaviour>();
        options_menu_behaviour = options_menu.GetComponent<OptionsMenuBehaviour>();

        audio_source.volume = PlayerPrefs.GetFloat("volume", OptionsMenuBehaviour.DEFAULT_VOLUME);

        animators = new Animator[game_objects_with_animation.Length];
        int i = 0;
        foreach(GameObject game_object in game_objects_with_animation)
        {
            animators[i] = game_object.GetComponent<Animator>();
            i++;
        }

        clapping_timer = 0.0f;
        clapping_audio_clip = Resources.Load("Sounds/IntroSoundEffect_01") as AudioClip;
        clapping_done = false;

        audio_source.clip = clapping_audio_clip;
        audio_source.Play();

        animation_speeds = new float[game_objects_with_animation.Count()];

        i = 0;
        foreach(Animator animator in animators)
        {
            animation_speeds[i] = animator.speed;
            i++;
        }
    }

    private void Update()
    {
        checkPauseInput();

        if (paused == false)
        {
            waitForClapping();
        }
    }

    private void checkPauseInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            togglePause();
        }
    }

    private void waitForClapping()
    {
        if (clapping_done == false && clapping_timer >= CLAPPING_INTERVAL)
        {
            weapon_spawner_behaviour.startLagTimers();
            clapping_done = true;
        }
        else
        {
            clapping_timer += Time.deltaTime;
        }
    }

    public void togglePause()
    {
        if(paused == false)
        {
            options_menu_behaviour.pauseSound();
            foreach (Animator animator in animators)
            {
                animator.speed = 0;
            }
            pause_canvas.SetActive(true);

            //This is to ensure that no matter the menu state on the last pause, the menu always opens on the main pause menu
            pause_menu.SetActive(true); 
            options_menu.SetActive(false);

            paused = true;
        }
        else
        {
            options_menu_behaviour.unPauseSound();
            int i = 0;
            foreach (Animator animator in animators)
            {
                animator.speed = animation_speeds[i];
                i++;
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
