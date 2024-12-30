using System.Collections;
using Map;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {

        public static Game currentGame = null;
        //public Map CurrentMap { get; private set; }

        // Use this for initialization
        void Start()
        {
            // if (currentGame == null)
            // {
            //     currentGame = new Game();
            // }
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void QuitGame()
        {
            Debug.Log("Quit Game");
            Application.Quit();
        }
    }
}