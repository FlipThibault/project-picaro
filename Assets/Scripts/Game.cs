using System.Collections;
using UnityEngine;

[System.Serializable]
public class Game
{
    public int partySize = 5;
    public int partyLevel = 1;
    public Environment environment = Environment.Outerrealm;
    public string gameName = "default";

    // Default constructor
    public Game()
    {
        // You can initialize the default values here if needed
    }

    // Constructor with parameters to initialize all fields
    public Game(int partySize, int partyLevel, Environment environment, string gameName)
    {
        this.partySize = partySize;
        this.partyLevel = partyLevel;
        this.environment = environment;
        this.gameName = gameName;
    }

    // Getters and Setters
    public int GetPartySize()
    {
        return partySize;
    }

    public void SetPartySize(int value)
    {
        partySize = value;
    }

    public int GetPartyLevel()
    {
        return partyLevel;
    }

    public void SetPartyLevel(int value)
    {
        partyLevel = value;
    }

    public Environment GetEnvironment()
    {
        return environment;
    }

    public void SetEnvironment(Environment value)
    {
        environment = value;
    }

    public string GetGameName()
    {
        return gameName;
    }

    public void SetGameName(string value)
    {
        gameName = value;
    }
}