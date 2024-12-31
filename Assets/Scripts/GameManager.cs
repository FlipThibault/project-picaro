using System.Collections;
using Map;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static Game currentGame { get; set; }
        public void QuitGame()
        {
            Debug.Log("Quit Game");
            Application.Quit();
        }
    }
}