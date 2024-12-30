using System;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        // if(sceneName == "Map") {
        //     //set game in player prefs
        //     // PlayerPrefs.
        //     // set map in player prefs
        //     GameManager.currentGame = new Game(2, 5, Environment.Darklands, "potatoSalad");
        // }
        SceneManager.LoadScene(sceneName);
    }
}