using System.Linq;
using UnityEngine;
using Newtonsoft.Json;
using Assets.Scripts;

namespace Map
{
    public class MapManager : MonoBehaviour
    {
        public MapConfig config;
        public MapView view;

        public Map CurrentMap { get; private set; }

        private void Start()
        {
            if (PlayerPrefs.HasKey("Game"))
            {
                string gameJson = PlayerPrefs.GetString("Game");
                Game game = JsonConvert.DeserializeObject<Game>(gameJson);
                GameManager.currentGame = game;
            }
            if (PlayerPrefs.HasKey("Map"))
            {
                string mapJson = PlayerPrefs.GetString("Map");
                Map map = JsonConvert.DeserializeObject<Map>(mapJson);
                // using this instead of .Contains()
                if (map.path.Any(p => p.Equals(map.GetBossNode().point)))
                {
                    // payer has already reached the boss, generate a new map
                    // GenerateNewMap();
                }
                else
                {
                    CurrentMap = map;
                    // player has not reached the boss yet, load the current map
                    view.ShowMap(map);
                }
            }
            else
            {
                GenerateNewMap();
            }
        }

        public void GenerateNewMap()
        {
            Map map = MapGenerator.GetMap(config);
            CurrentMap = map;
            Debug.Log(map.ToJson());
            view.ShowMap(map);
        }

        public void SaveGame()
        {
            if (CurrentMap == null) return;

            string mapJson = JsonConvert.SerializeObject(CurrentMap, Formatting.Indented,
                new JsonSerializerSettings {ReferenceLoopHandling = ReferenceLoopHandling.Ignore});
            PlayerPrefs.SetString("Map", mapJson);
            Debug.Log("SAVING MAP: " + mapJson.ToString());

            string gameJson = JsonConvert.SerializeObject(GameManager.currentGame, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            PlayerPrefs.SetString("Game", gameJson);
            Debug.Log("SAVING GAME: " + gameJson.ToString());

            PlayerPrefs.SetInt("PotionLikelihood", PotionGenerator.getCurrentPotionLikelihoodPercentage());
            Debug.Log("SAVING potionlikelihood: " + PotionGenerator.getCurrentPotionLikelihoodPercentage().ToString());

            string mysteryRoomTypeLikelihoodJson = JsonConvert.SerializeObject(MysteryGenerator.getCurrentMysteryRoomTypeLikelihoodPercentage(), Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            PlayerPrefs.SetString("MysteryLikelihood", mysteryRoomTypeLikelihoodJson);
            Debug.Log("SAVING MysteryLikelihood: " + mysteryRoomTypeLikelihoodJson.ToString());

            PlayerPrefs.Save();

            string filepath = Application.persistentDataPath + "/" + GameManager.currentGame.gameName + ".json";
            Debug.Log("GAME SAVING AT LOCATION: " + filepath);
            SaveData saveData = new SaveData(GameManager.currentGame, CurrentMap, PotionGenerator.getCurrentPotionLikelihoodPercentage(), MysteryGenerator.getCurrentMysteryRoomTypeLikelihoodPercentage());
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
