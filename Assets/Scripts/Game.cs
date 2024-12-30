using System.Collections;
using Map;
using UnityEngine;

namespace Map {
    [System.Serializable]
    public class Game
    {
        public int partySize { get; set; }
        public int partyLevel  { get; set; }
        public Environment environment { get; set; }
        public string gameName { get; set; }

        // Default constructor
        public Game()
        {
            // You can initialize the default values here if needed
            partySize = 5;
            partyLevel = 1;
            environment = Environment.Outerrealm;
            gameName = "default";
        }

        // Constructor with parameters to initialize all fields
        public Game(int partySize, int partyLevel, Environment environment, string gameName)
        {
            this.partySize = partySize;
            this.partyLevel = partyLevel;
            this.environment = environment;
            this.gameName = gameName;
        }
    }
}
