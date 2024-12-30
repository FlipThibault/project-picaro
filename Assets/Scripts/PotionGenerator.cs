using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;

public static class PotionGenerator
{
    static int CommonPotionRarity = 70;
    static int UnCommonPotionRarity = 20;
    static int RarePotionRarity = 10;

    //generate potions
    static List<PotionData> allPotions = PotionsLoader.Instance.PotionData.potions;

    static int currentPotionLikelihoodPercentage = 40;
    static System.Random random = new System.Random();

    public static void setCurrentPotionLikelihoodPercentage(int potionLikelihoodPercentage)
    {
        currentPotionLikelihoodPercentage = potionLikelihoodPercentage;
    }

    public static int getCurrentPotionLikelihoodPercentage()
    {
        return currentPotionLikelihoodPercentage;
    }

    static void increaseLikelihood()
    {
        currentPotionLikelihoodPercentage = System.Math.Min(100, currentPotionLikelihoodPercentage + 10);
    }

    static void decreaseLikelihood() {
        currentPotionLikelihoodPercentage = System.Math.Max(0, currentPotionLikelihoodPercentage - 10);
    }

    public static void resetLikelihood()
    {
        currentPotionLikelihoodPercentage = 40;
    }

    static List<PotionData> generatePotionsDrop(int maxPotion, int partyLevel)
    {
        int numPotionsToGenerate = random.Next(1, maxPotion + 1);
        List<PotionData> potionsToReturn = new List<PotionData>();
        List<PotionData> elibilePotions = allPotions.FindAll(potion => potion.level <= partyLevel + 2);

        for (int i = 0; i < numPotionsToGenerate; i++)
        {
            int potionLikelihood = random.Next(1, 101);

            if (potionLikelihood <= RarePotionRarity)
            {
                List<PotionData> rarePotions = elibilePotions.FindAll(potion => potion.rarity == "Rare");

                if(rarePotions.Count == 0)
                {
                    rarePotions = elibilePotions.FindAll(potion => potion.rarity == "Uncommon");
                }
                if (rarePotions.Count == 0)
                {
                    rarePotions = elibilePotions.FindAll(potion => potion.rarity == "Common");
                }
                int rarePotionIndex = random.Next(0, rarePotions.Count);

                potionsToReturn.Add(rarePotions[rarePotionIndex]);
            }
            else if (potionLikelihood <= UnCommonPotionRarity) {
                List<PotionData> uncommonPotions = elibilePotions.FindAll(potion => potion.rarity == "Uncommon");
                if (uncommonPotions.Count == 0)
                {
                    uncommonPotions = elibilePotions.FindAll(potion => potion.rarity == "Common");
                }
                int uncommonPotionIndex = random.Next(0, uncommonPotions.Count);

                potionsToReturn.Add(uncommonPotions[uncommonPotionIndex]);
            }
            else
            {
                List<PotionData> commonPotions = elibilePotions.FindAll(potion => potion.rarity == "Common");
                int commonPotionIndex = random.Next(0, commonPotions.Count);

                potionsToReturn.Add(commonPotions[commonPotionIndex]);
            }
        }

        return potionsToReturn;
    }

    public static List<PotionData> generatePotionsForEncounter(int partySize, int partyLevel)
    {
        int maxPotions = partySize / 2;

        int potionDropPercentage = random.Next(1, 101);
        bool hasPotion = potionDropPercentage <= currentPotionLikelihoodPercentage;

        if(hasPotion)
        {
            Debug.Log("POTION FOUND - decreasing likelihood ");
            decreaseLikelihood();
            return generatePotionsDrop(maxPotions, partyLevel);
        } else
        {
            Debug.Log("POTION NOT FOUND - increasing likelihood ");
            increaseLikelihood();
            return new List<PotionData>();
        }
    }
}