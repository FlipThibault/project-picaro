using System.Linq;
using UnityEngine;
using Newtonsoft.Json;
using Assets.Scripts;
using System.Collections.Generic;
using System;

namespace Map
{
    public class MapManager : MonoBehaviour
    {
        public MapConfig config;
        public MapView view;

        public static Dictionary<int, Map> mapsPerLevel { get; set; }

        public static int currentLevel { get; set; }

        private void Start()
        {
            if (PlayerPrefs.HasKey("Game"))
            {
                string gameJson = PlayerPrefs.GetString("Game");
                Game game = JsonConvert.DeserializeObject<Game>(gameJson);
                GameManager.currentGame = game;
                currentLevel = game.partyLevel;
            }
            if (PlayerPrefs.HasKey("MapsPerLevel"))
            {
                string mapsPerLevelJson = PlayerPrefs.GetString("MapsPerLevel");
                mapsPerLevel = JsonConvert.DeserializeObject<Dictionary<int, Map>>(mapsPerLevelJson);

                view.ShowMap(getCurrentMap());
            }
            else
            {
                GenerateNewMap();
            }
        }

        public void IncrementLevel() {
            if(currentLevel == GameManager.currentGame.partyLevel) {
                int newLevel = GameManager.currentGame.partyLevel + 1;
                GameManager.currentGame.partyLevel = newLevel;
                currentLevel = newLevel;
            } else {
                currentLevel += 1;
            }
            UpdateMap(false);
            SaveGame();
        }

        public void GoBackLevel() {
            currentLevel -= 1;
            view.ShowMap(mapsPerLevel[currentLevel]);
        }

        public void GenerateNewMap() {
            UpdateMap(true);
        }
        public void UpdateMap(bool shouldRegenerateMapForCurrentLevel)
        {
            if(mapsPerLevel == null) {
                mapsPerLevel = new Dictionary<int, Map>();
            }
            if(!mapsPerLevel.ContainsKey(currentLevel)) {
                Map map = MapGenerator.GetMap(config);
                mapsPerLevel.Add(currentLevel, map);
                view.ShowMap(map);
            } else {
                if(shouldRegenerateMapForCurrentLevel && !mapsPerLevel[currentLevel].isInProgress) {
                    Map map = MapGenerator.GetMap(config);
                    mapsPerLevel[currentLevel] = map;
                }
                view.ShowMap(mapsPerLevel[currentLevel]);
            }
        }

        public static Map getCurrentMap() {
            try {
                return mapsPerLevel[currentLevel];
            } catch(Exception e) {
                return null;
            }
        }

        public void SaveGame()
        {
            if (getCurrentMap() == null) return;

            string mapsPerLevelJson = JsonConvert.SerializeObject(mapsPerLevel, Formatting.Indented,
                new JsonSerializerSettings {ReferenceLoopHandling = ReferenceLoopHandling.Ignore});
            PlayerPrefs.SetString("MapsPerLevel", mapsPerLevelJson);
            Debug.Log("SAVING MAPS: " + mapsPerLevelJson.ToString());

            string gameJson = JsonConvert.SerializeObject(GameManager.currentGame, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            PlayerPrefs.SetString("Game", gameJson);
            Debug.Log("SAVING GAME: " + gameJson.ToString());

            PlayerPrefs.SetInt("PotionLikelihood", PotionGenerator.getCurrentPotionLikelihoodPercentage());
            Debug.Log("SAVING potionlikelihood: " + PotionGenerator.getCurrentPotionLikelihoodPercentage().ToString());

            string mysteryRoomTypeLikelihoodJson = JsonConvert.SerializeObject(MysteryGenerator.getCurrentMysteryRoomTypeLikelihoodPercentage(), Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            PlayerPrefs.SetString("MysteryLikelihood", mysteryRoomTypeLikelihoodJson);
            Debug.Log("SAVING MysteryLikelihood: " + mysteryRoomTypeLikelihoodJson.ToString());

            string filepath = Application.persistentDataPath + "/" + GameManager.currentGame.gameName + ".json";
            Debug.Log("GAME SAVING AT LOCATION: " + filepath);
            SaveData saveData = new SaveData(GameManager.currentGame, mapsPerLevel, PotionGenerator.getCurrentPotionLikelihoodPercentage(), MysteryGenerator.getCurrentMysteryRoomTypeLikelihoodPercentage());
            string saveDataJson = JsonConvert.SerializeObject(saveData, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

            System.IO.File.WriteAllText(filepath, saveDataJson);
            Debug.Log("GAME SAVED!");

        }

        private void OnApplicationQuit()
        {
            SaveGame();
        }
    }
}
