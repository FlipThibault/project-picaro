using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using System;

[System.Serializable]
public class PotionData
{
    public string name;
    public string rarity;
    public string price;
    public int level;
}

[System.Serializable]
public class PotionArray
{
    public List<PotionData> potions;

    public void addItem(PotionData potionData)
    {
        this.potions.Add(potionData);
    }
}

public class PotionsLoader : MonoBehaviour
{
    public static PotionsLoader Instance { get; private set; }
    public PotionArray PotionData { get; private set; }

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
        TextAsset jsonFile = Resources.Load<TextAsset>("potions");
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
        PotionData = JsonUtility.FromJson<PotionArray>(json);
    }
}