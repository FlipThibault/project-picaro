using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using Map;
using UnityEngine;

public enum ThreatLevelMultiplier : int
{
    Trivial = 10,
    Low = 15,
    Moderate = 20,
    Severe = 30,
    Extreme = 40,
}

public class EncounterData
{
    public int partyLevelAdjustment;
    public int xp;

    public EncounterData(int partyLevelAdjustment, int xp)
    {
        this.partyLevelAdjustment = partyLevelAdjustment;
        this.xp = xp;
    }
}

public static class EncounterBuilder
{
    public static List<EncounterData> encounterData = new List<EncounterData>
    {
        new EncounterData(-4, 10),
        new EncounterData(-3, 15),
        new EncounterData(-2, 20),
        new EncounterData(-1, 30),
        new EncounterData(0, 40),
        new EncounterData(+1, 60),
        new EncounterData(+2, 80),
        new EncounterData(+3, 120),
        new EncounterData(+4, 160)
    };

    static ThreatLevelMultiplier getThreatLevel(Node node)
    {
        ThreatLevelMultiplier threatLevelMultiplier = ThreatLevelMultiplier.Moderate;

        if (node.nodeType == NodeType.EliteEnemy)
        {
            threatLevelMultiplier = ThreatLevelMultiplier.Severe;
        }

        return threatLevelMultiplier;
    }
    static int computeEncounterXpBudget(int partySize, Node node)
    {
        return partySize * (int)getThreatLevel(node);
    }

    static int computeXpForCreatureLevel(int creatureLevel, int partyLevel)
    {
        int difference = creatureLevel - partyLevel;
        EncounterData encounterInfo = encounterData.FirstOrDefault(encounterInfo => encounterInfo.partyLevelAdjustment == difference);
        return encounterInfo?.xp ?? 0;
    }

    public static List<CreatureData> computeCreaturesForEncounter(int partySize, int partyLevel, Node node)
    {
        List<CreatureData> allCreatures = CreaturesLoader.Instance.CreatureData.creatures;
        int encounterBudget = computeEncounterXpBudget(partySize, node);

        int remainingBudget = encounterBudget;
        List<CreatureData> encounterCreatures = new List<CreatureData>();
        System.Random random = new System.Random();

        while (remainingBudget > 0)
        {
            List<CreatureData> eligibleEncounterCreatures = allCreatures.FindAll(creature =>
                creature.level >= partyLevel - 2 &&
                creature.level <= partyLevel + 2 &&
                creature.environment.Contains(GameManager.currentGame.GetEnvironment()) && 
                computeXpForCreatureLevel(creature.level, partyLevel) <= remainingBudget);

            if (eligibleEncounterCreatures.Count == 0)
            {
                //Debug.LogWarning("No eligible creatures found for the encounter.");
                break;
            }

            int index = random.Next(eligibleEncounterCreatures.Count);
            CreatureData creatureToAdd = eligibleEncounterCreatures[index];

            encounterCreatures.Add(creatureToAdd);
            remainingBudget -= computeXpForCreatureLevel(creatureToAdd.level, partyLevel);
        }

        return encounterCreatures;
    }

    public static List<ItemData> computeItemsForChest(int partySize, int partyLevel)
    {
        List<ItemData> allItems = ItemsLoader.Instance.ItemData.items;
        List<ItemData> encounterItems = new List<ItemData>();
        int maxItemLevel = partyLevel + 2;
        int minItems = 1;
        int maxItems = Math.Max(minItems, partySize - 1);
        System.Random random = new System.Random();
        int numberOfItems = random.Next(minItems, maxItems + 1);

        List<ItemData> eligibleChestItems = allItems.FindAll(item => item.level <= maxItemLevel);

        if (eligibleChestItems.Count == 0)
        {
            Debug.LogWarning("No eligible items found for the chest.");
            return encounterItems;
        }

        for (int i = 0; i < numberOfItems; i++)
        {
            int index = random.Next(eligibleChestItems.Count);
            encounterItems.Add(eligibleChestItems[index]);
        }

        return encounterItems;
    }

    public static List<PotionData> computePotionsForEncounter(int partySize, int partyLevel, Node node)
    {
        return PotionGenerator.generatePotionsForEncounter(partySize, partyLevel);
    }

    public static BoonData computeBoonForEncounter(int partySize, int partyLevel, Node node)
    {
        int CommonBoonRarity = 50;
        int UncommonBoonRarity = 30;
        int RareBoonRarity = 15;
        int LegendaryBoonRarity = 5;

        System.Random random = new System.Random();
        List<BoonData> allBoons = BoonsLoader.Instance.BoonData.boons;

        int boonLikelihood = random.Next(1, 101);

        List<BoonData> selectedRarityBoons;
        if (boonLikelihood <= LegendaryBoonRarity)
        {
            selectedRarityBoons = allBoons.FindAll(boon => boon.rarity == "Legendary");
        }
        else if (boonLikelihood <= RareBoonRarity)
        {
            selectedRarityBoons = allBoons.FindAll(boon => boon.rarity == "Rare");
        }
        else if (boonLikelihood <= UncommonBoonRarity)
        {
            selectedRarityBoons = allBoons.FindAll(boon => boon.rarity == "Uncommon");
        }
        else
        {
            selectedRarityBoons = allBoons.FindAll(boon => boon.rarity == "Common");
        }

        if (selectedRarityBoons.Count == 0)
        {
            //Debug.LogWarning("No boons available for the selected rarity.");
            return null;
        }

        int boonIndex = random.Next(selectedRarityBoons.Count);
        return selectedRarityBoons[boonIndex];
    }

    public static double getTreasureForEncounter(int partySize, int partyLevel, Node node)
    {
        return TreasureManager.getTreasure(partyLevel, partySize, getThreatLevel(node));
    }

}
