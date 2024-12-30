using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using System;

[System.Serializable]
public class ItemData
{
    public string name;
    public string rarity;
    public string trait;
    public string item_category;
    public string price;
    public string item_subcategory;
    public int level;
}

[System.Serializable]
public class ItemArray
{
    public List<ItemData> items; 

    public void addItem(ItemData itemData)
    {
        this.items.Add(itemData);
    }
}

public class ItemsLoader : MonoBehaviour
{
    public static ItemsLoader Instance { get; private set; }
    public ItemArray ItemData { get; private set; }

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
        TextAsset jsonFile = Resources.Load<TextAsset>("items");
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
        ItemData = JsonUtility.FromJson<ItemArray>(json);
    }
}