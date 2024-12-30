using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Map;
public static class MysteryGenerator
{

    static Dictionary<MysteryRoomType, int> currentRoomTypeLikelihoodPercentages = new Dictionary<MysteryRoomType, int>();
    static Dictionary<MysteryRoomType, int> basePercentages = new Dictionary<MysteryRoomType, int>();

    static int BASE_MINOR_ENEMY_PERCENTAGE = 10;
    static int BASE_DEMON_DOOR_PERCENTAGE = 5;
    static int BASE_STORE_PERCENTAGE = 3;
    static int BASE_TREASURE_PERCENTAGE = 2;

    static System.Random random = new System.Random();
    public static void setCurrentMysteryRoomTypeLikelihoodPercentage(Dictionary<MysteryRoomType, int> likelihood)
    {
        currentRoomTypeLikelihoodPercentages = likelihood;
    }

    public static Dictionary<MysteryRoomType, int> getCurrentMysteryRoomTypeLikelihoodPercentage()
    {
        return currentRoomTypeLikelihoodPercentages;
    }

    static MysteryGenerator() {
        resetLikelihood();
    }

    static void increaseOtheRoomLikelihoods(MysteryRoomType generatedType)
    {
        var keys = new List<MysteryRoomType>(currentRoomTypeLikelihoodPercentages.Keys);

        foreach (var key in keys)
        {
            if(key != generatedType)
            {
                currentRoomTypeLikelihoodPercentages[key] = currentRoomTypeLikelihoodPercentages[key] + basePercentages[key];
            }
        }

    }

    public static void resetLikelihood()
    {
        basePercentages.Clear();
        currentRoomTypeLikelihoodPercentages.Clear();

        basePercentages.Add(MysteryRoomType.MinorEnemy, BASE_MINOR_ENEMY_PERCENTAGE);
        basePercentages.Add(MysteryRoomType.DemonDoor, BASE_DEMON_DOOR_PERCENTAGE);
        basePercentages.Add(MysteryRoomType.Store, BASE_STORE_PERCENTAGE);
        basePercentages.Add(MysteryRoomType.Treasure, BASE_TREASURE_PERCENTAGE);

        currentRoomTypeLikelihoodPercentages.Add(MysteryRoomType.MinorEnemy, basePercentages[MysteryRoomType.MinorEnemy]);
        currentRoomTypeLikelihoodPercentages.Add(MysteryRoomType.DemonDoor, basePercentages[MysteryRoomType.DemonDoor]);
        currentRoomTypeLikelihoodPercentages.Add(MysteryRoomType.Store, basePercentages[MysteryRoomType.Store]);
        currentRoomTypeLikelihoodPercentages.Add(MysteryRoomType.Treasure, basePercentages[MysteryRoomType.Treasure]);
    }

    public static MysteryRoomType computeMysteryRoomType()
    {
        int percentage = random.Next(1, 101);

        Debug.Log("MYSTERY PERCENTAGE: " + percentage);

        int currentOffset = 0;
        foreach (var kvp in currentRoomTypeLikelihoodPercentages.OrderBy(kvp => kvp.Value))
        {
            if (percentage >= currentOffset && percentage < currentRoomTypeLikelihoodPercentages[kvp.Key] + currentOffset)
            {
                increaseOtheRoomLikelihoods(kvp.Key);
                return kvp.Key;
            } else
            {
                currentOffset += currentRoomTypeLikelihoodPercentages[kvp.Key];
            }
        }

        return MysteryRoomType.Event;
    }
}