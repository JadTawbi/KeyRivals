using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SongSelectMenuBehaviour : MonoBehaviour
{
    public static WeaponSpawnerBehaviour.PlayableTrack track;
    public void play()
    {
        SceneManager.LoadScene("Game");
    }

    public void loadLucidDream()
    {
        track = WeaponSpawnerBehaviour.PlayableTrack.LucidDream;
        TutorialBehaviour.practice = false;
    }
    public void loadSchukran()
    {
        track = WeaponSpawnerBehaviour.PlayableTrack.Schukran;
        TutorialBehaviour.practice = false;
    }
    public void loadElTió()
    {
        track = WeaponSpawnerBehaviour.PlayableTrack.ElTio;
        TutorialBehaviour.practice = false;
    }
    public void loadRivals()
    {
        track = WeaponSpawnerBehaviour.PlayableTrack.Rivals;
        TutorialBehaviour.practice = false;
    }
    public void loadSEKBeat()
    {
        track = WeaponSpawnerBehaviour.PlayableTrack.SEKBeat;
        TutorialBehaviour.practice = false;
    }
    public void loadLagom()
    {
        track = WeaponSpawnerBehaviour.PlayableTrack.Lagom;
        TutorialBehaviour.practice = false;
    }
    public void loadDeeper()
    {
        track = WeaponSpawnerBehaviour.PlayableTrack.Deeper;
        TutorialBehaviour.practice = false;
    }
    public void loadPractice()
    {
        track = WeaponSpawnerBehaviour.PlayableTrack.Practice;
        TutorialBehaviour.practice = true;
    }
}
