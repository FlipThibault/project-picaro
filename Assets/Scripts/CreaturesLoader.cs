using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;


[System.Serializable]
public enum Environment
{
    Graveyard,
    Celestial,
    Dungeon,
    Wilderness,
    Outerrealm,
    Darklands,
    Hell,
    Faewild,
    Water,
    Holy
}

[System.Serializable]
public class CreatureData
{
    public string name;
    public int level;
    public Environment[] environment;
}

[System.Serializable]
public class CreatureArray
{
    public List<CreatureData> creatures; // An array to hold Creature objects

    public void addCreature(CreatureData creatureData)
    {
        this.creatures.Add(creatureData);
    }
}
public class CreaturesLoader : MonoBehaviour
{
    public static CreaturesLoader Instance { get; private set; }
    public CreatureArray CreatureData { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Keep across scenes
        }
        else
        {
            Destroy(gameObject); // Ensure there's only one instance
        }
    }

    void Start()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("creatures");
        if (jsonFile != null)
        {
            ParseJson(jsonFile.text);
        }
        else
        {
            Debug.LogError("JSON file not found in Resources folder.");
        }
    }

    void ParseJson(string json)
    {
        CreatureData = JsonConvert.DeserializeObject<CreatureArray>(json);
    }
}