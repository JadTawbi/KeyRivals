using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectMenuBehaviour : MonoBehaviour
{
    public static PlayerBehaviour.PlayerCharacter player1_character, player2_character;

    public static void randomizeCharacters()
    {
        player1_character = (PlayerBehaviour.PlayerCharacter)Random.Range(0, 7);
        player2_character = (PlayerBehaviour.PlayerCharacter)Random.Range(0, 7);
    }
    public void loadBassfisherPlayer1()
    {
        player1_character = PlayerBehaviour.PlayerCharacter.BassFisher; 
    }    
    public void loadBassfisherPlayer2()
    {
        player2_character = PlayerBehaviour.PlayerCharacter.BassFisher;
    }
    public void loadBigLightBeamPlayer1()
    {
        player1_character = PlayerBehaviour.PlayerCharacter.BigLightBeam;
    }
    public void loadBigLightBeamPlayer2()
    {
        player2_character = PlayerBehaviour.PlayerCharacter.BigLightBeam;
    }
    public void loadCrownJulesPlayer1()
    {
        player1_character = PlayerBehaviour.PlayerCharacter.CrownJules;
    }
    public void loadCrownJulesPlayer2()
    {
        player2_character = PlayerBehaviour.PlayerCharacter.CrownJules;
    }
    public void loadHotDoggPlayer1()
    {
        player1_character = PlayerBehaviour.PlayerCharacter.HotDogg;
    }
    public void loadHotDoggPlayer2()
    {
        player2_character = PlayerBehaviour.PlayerCharacter.HotDogg;
    }
    public void loadJojitsuPlayer1()
    {
        player1_character = PlayerBehaviour.PlayerCharacter.Jojitsu;
    }
    public void loadJojitsuPlayer2()
    {
        player2_character = PlayerBehaviour.PlayerCharacter.Jojitsu;
    }
    public void loadLadyGooGooGaGaPlayer1()
    {
        player1_character = PlayerBehaviour.PlayerCharacter.LadyGooGooGaGa;
    }
    public void loadLadyGooGooGaGaPlayer2()
    {
        player2_character = PlayerBehaviour.PlayerCharacter.LadyGooGooGaGa;
    }
    public void loadPowerdogPlayer1()
    {
        player1_character = PlayerBehaviour.PlayerCharacter.Powerdog;
    }
    public void loadPowerdogPlayer2()
    {
        player2_character = PlayerBehaviour.PlayerCharacter.Powerdog;
    }

}
