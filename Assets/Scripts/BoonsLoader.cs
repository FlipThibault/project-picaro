using System.Linq;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class BoonData
{
    public string name;
    public string description;
    public string rarity;
}

[System.Serializable]
public class BoonArray
{
    public List<BoonData> boons; // An array to hold Boon objects

    public void addBoon(BoonData BoonData)
    {
        this.boons.Add(BoonData);
    }
}
public class BoonsLoader : MonoBehaviour
{
    public static BoonsLoader Instance { get; private set; }
    public BoonArray BoonData { get; private set; }

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
        TextAsset jsonFile = Resources.Load<TextAsset>("boons");
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
        BoonData = JsonUtility.FromJson<BoonArray>(json);
    }
}