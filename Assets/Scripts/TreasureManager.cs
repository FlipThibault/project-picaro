using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public static class TreasureManager
{

    //values based on https://2e.aonprd.com/Rules.aspx?ID=2738&Redirected=1
    // The values have been divided by 4 since the assumption is that the Archives of Nethys table is per 4 party members
    static Dictionary<int, Dictionary<ThreatLevelMultiplier, double>> ecounterTreasurePerLevel = new Dictionary<int, Dictionary<ThreatLevelMultiplier, double>> {
        { 
            1, new Dictionary<ThreatLevelMultiplier, double> {
                { ThreatLevelMultiplier.Low, 3.25 },
                { ThreatLevelMultiplier.Moderate, 4.5 },
                { ThreatLevelMultiplier.Severe, 6.5 },
                { ThreatLevelMultiplier.Extreme, 8.75 },
            } 
        },
        {
            2, new Dictionary<ThreatLevelMultiplier, double> {
                { ThreatLevelMultiplier.Low, 5.75 },
                { ThreatLevelMultiplier.Moderate, 7.5 },
                { ThreatLevelMultiplier.Severe, 11.25 },
                { ThreatLevelMultiplier.Extreme, 15 },
            }
        },
        {
            3, new Dictionary<ThreatLevelMultiplier, double> {
                { ThreatLevelMultiplier.Low, 9.5 },
                { ThreatLevelMultiplier.Moderate, 12.5 },
                { ThreatLevelMultiplier.Severe, 18.75 },
                { ThreatLevelMultiplier.Extreme, 25 },
            }
        },
        {
            4, new Dictionary<ThreatLevelMultiplier, double> {
                { ThreatLevelMultiplier.Low, 16.25 },
                { ThreatLevelMultiplier.Moderate, 21.25 },
                { ThreatLevelMultiplier.Severe, 32.5 },
                { ThreatLevelMultiplier.Extreme, 42.5 },
            }
        },
        {
            5, new Dictionary<ThreatLevelMultiplier, double> {
                { ThreatLevelMultiplier.Low, 25 },
                { ThreatLevelMultiplier.Moderate, 33.75 },
                { ThreatLevelMultiplier.Severe, 50 },
                { ThreatLevelMultiplier.Extreme, 67.5 },
            }
        },
        {
            6, new Dictionary<ThreatLevelMultiplier, double> {
                { ThreatLevelMultiplier.Low, 37.5 },
                { ThreatLevelMultiplier.Moderate, 50 },
                { ThreatLevelMultiplier.Severe, 75 },
                { ThreatLevelMultiplier.Extreme, 100 },
            }
        },
        {
            7, new Dictionary<ThreatLevelMultiplier, double> {
                { ThreatLevelMultiplier.Low, 55 },
                { ThreatLevelMultiplier.Moderate, 72.5 },
                { ThreatLevelMultiplier.Severe, 110 },
                { ThreatLevelMultiplier.Extreme, 145 },
            }
        },
        {
            8, new Dictionary<ThreatLevelMultiplier, double> {
                { ThreatLevelMultiplier.Low, 75 },
                { ThreatLevelMultiplier.Moderate, 100 },
                { ThreatLevelMultiplier.Severe, 150 },
                { ThreatLevelMultiplier.Extreme, 200 },
            }
        },
        {
            9, new Dictionary<ThreatLevelMultiplier, double> {
                { ThreatLevelMultiplier.Low, 107.5 },
                { ThreatLevelMultiplier.Moderate, 142.5 },
                { ThreatLevelMultiplier.Severe, 215 },
                { ThreatLevelMultiplier.Extreme, 285 },
            }
        },
        {
            10, new Dictionary<ThreatLevelMultiplier, double> {
                { ThreatLevelMultiplier.Low, 150 },
                { ThreatLevelMultiplier.Moderate, 200 },
                { ThreatLevelMultiplier.Severe, 300 },
                { ThreatLevelMultiplier.Extreme, 400 },
            }
        },
        {
            11, new Dictionary<ThreatLevelMultiplier, double> {
                { ThreatLevelMultiplier.Low, 216.25 },
                { ThreatLevelMultiplier.Moderate, 287.5 },
                { ThreatLevelMultiplier.Severe, 431.25 },
                { ThreatLevelMultiplier.Extreme, 575 },
            }
        },
        {
            12, new Dictionary<ThreatLevelMultiplier, double> {
                { ThreatLevelMultiplier.Low, 312.5 },
                { ThreatLevelMultiplier.Moderate, 412.5 },
                { ThreatLevelMultiplier.Severe, 618.75 },
                { ThreatLevelMultiplier.Extreme, 825 },
            }
        },
        {
            13, new Dictionary<ThreatLevelMultiplier, double> {
                { ThreatLevelMultiplier.Low, 468.75 },
                { ThreatLevelMultiplier.Moderate, 625 },
                { ThreatLevelMultiplier.Severe, 937.5 },
                { ThreatLevelMultiplier.Extreme, 1250 },
            }
        },
        {
            14, new Dictionary<ThreatLevelMultiplier, double> {
                { ThreatLevelMultiplier.Low, 687.5 },
                { ThreatLevelMultiplier.Moderate, 912.5 },
                { ThreatLevelMultiplier.Severe, 1375 },
                { ThreatLevelMultiplier.Extreme, 1825 },
            }
        },
        {
            15, new Dictionary<ThreatLevelMultiplier, double> {
                { ThreatLevelMultiplier.Low, 1025 },
                { ThreatLevelMultiplier.Moderate, 1362.5 },
                { ThreatLevelMultiplier.Severe, 2050 },
                { ThreatLevelMultiplier.Extreme, 2725 },
            }
        },
        {
            16, new Dictionary<ThreatLevelMultiplier, double> {
                { ThreatLevelMultiplier.Low, 1550 },
                { ThreatLevelMultiplier.Moderate, 2062.5 },
                { ThreatLevelMultiplier.Severe, 3100 },
                { ThreatLevelMultiplier.Extreme, 4125 },
            }
        },
        {
            17, new Dictionary<ThreatLevelMultiplier, double> {
                { ThreatLevelMultiplier.Low, 2440 },
                { ThreatLevelMultiplier.Moderate, 3200 },
                { ThreatLevelMultiplier.Severe, 4800 },
                { ThreatLevelMultiplier.Extreme, 6400 },
            }
        },
        {
            18, new Dictionary<ThreatLevelMultiplier, double> {
                { ThreatLevelMultiplier.Low, 3900 },
                { ThreatLevelMultiplier.Moderate, 5200 },
                { ThreatLevelMultiplier.Severe, 7800 },
                { ThreatLevelMultiplier.Extreme, 10400 },
            }
        },
        {
            19, new Dictionary<ThreatLevelMultiplier, double> {
                { ThreatLevelMultiplier.Low, 6650 },
                { ThreatLevelMultiplier.Moderate, 8875 },
                { ThreatLevelMultiplier.Severe, 13312.5 },
                { ThreatLevelMultiplier.Extreme, 17750 },
            }
        },
         {
            20, new Dictionary<ThreatLevelMultiplier, double> {
                { ThreatLevelMultiplier.Low, 9200 },
                { ThreatLevelMultiplier.Moderate, 12250 },
                { ThreatLevelMultiplier.Severe, 18375 },
                { ThreatLevelMultiplier.Extreme, 24500 },
            }
        },
    };
    public static double getTreasure(int partyLevel, int partySize, ThreatLevelMultiplier threatLevelModifier)
    {
        return ecounterTreasurePerLevel[partyLevel][threatLevelModifier] * partySize;
    }
}