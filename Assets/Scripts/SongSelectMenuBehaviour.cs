using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SongSelectMenuBehaviour : MonoBehaviour
{
    public static WeaponSpawnerBehaviour.Track track;
    public void play()
    {
        SceneManager.LoadScene("Game");
    }

    public void loadLucidDream()
    {
        track = WeaponSpawnerBehaviour.Track.LucidDream;
        TutorialBehaviour.practice = false;
    }
    public void loadSchukran()
    {
        track = WeaponSpawnerBehaviour.Track.Schukran;
        TutorialBehaviour.practice = false;
    }
    public void loadElTió()
    {
        track = WeaponSpawnerBehaviour.Track.ElTió;
        TutorialBehaviour.practice = false;
    }
    public void loadRivals()
    {
        track = WeaponSpawnerBehaviour.Track.Rivals;
        TutorialBehaviour.practice = false;
    }
    public void loadSEKBeat()
    {
        track = WeaponSpawnerBehaviour.Track.SEKBeat;
        TutorialBehaviour.practice = false;
    }
    public void loadLagom()
    {
        track = WeaponSpawnerBehaviour.Track.Lagom;
        TutorialBehaviour.practice = false;
    }
    public void loadDeeper()
    {
        track = WeaponSpawnerBehaviour.Track.Deeper;
        TutorialBehaviour.practice = false;
    }
    public void loadPractice()
    {
        track = WeaponSpawnerBehaviour.Track.Practice;
        TutorialBehaviour.practice = true;
    }
}
