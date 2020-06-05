using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectMenuBehaviour : MonoBehaviour
{
    public static PlayerBehaviour.PlayerCharacter player1_character, player2_character;
    public Button[] player1_buttons, player2_buttons;

    private void OnEnable()
    {
        updateButtonColourPlayer1();
        updateButtonColourPlayer2();
    }

    public static void randomizeCharacters()
    {
        player1_character = (PlayerBehaviour.PlayerCharacter)Random.Range(0, 7);
        player2_character = (PlayerBehaviour.PlayerCharacter)Random.Range(0, 7);
    }
    public void randomizePlayer1Character()
    {
        player1_character = (PlayerBehaviour.PlayerCharacter)Random.Range(0, 7);
    }
    public void randomizePlayer2Character()
    {
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

    public void updateButtonColourPlayer1()
    {
        ColorBlock color_block;
        for (int i = 0; i < player1_buttons.Count(); i++)
        {
            color_block = player1_buttons[i].colors;

            if (i == (int)player1_character)
            {
                color_block.normalColor = WeaponBehaviour.blue;
            }
            else
            {
                color_block.normalColor = Color.white;
            }

            player1_buttons[i].colors = color_block;
        }
    }

    public void updateButtonColourPlayer2()
    {
        ColorBlock color_block;
        for (int i = 0; i < player2_buttons.Count(); i++)
        {
            color_block = player2_buttons[i].colors;

            if (i == (int)player2_character)
            {
                color_block.normalColor = WeaponBehaviour.red;
            }
            else
            {
                color_block.normalColor = Color.white;
            }

            player2_buttons[i].colors = color_block;
        }
    }
}
