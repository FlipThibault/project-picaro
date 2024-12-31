using System.Collections;
using Map;
using Newtonsoft.Json;
using UnityEngine;

namespace Map {
    [System.Serializable]
    public class Game
    {
        public int partySize { get; set; }
        public int partyLevel  { get; set; }
        public Environment environment { get; set; }
        public string gameName { get; set; }

        public Game()
        {
            partySize = 4;
            partyLevel = 1;
            environment = Environment.Outerrealm;
            gameName = "default";
        }

        public Game(int partySize, int partyLevel, Environment environment, string gameName)
        {
            this.partySize = partySize;
            this.partyLevel = partyLevel;
            this.environment = environment;
            this.gameName = gameName;
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }
    }
}
