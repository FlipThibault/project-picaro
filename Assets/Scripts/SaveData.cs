using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    [System.Serializable]
    public class SaveData
    {

        public Game game { get; private set; }
        public Dictionary<int, Map> mapsPerLevel { get; private set; }
        public int potionLikelihood { get; private set; }
        public Dictionary<MysteryRoomType, int> mysteryRoomTypeLikelihoods { get; private set; }

        public SaveData(Game game, Dictionary<int, Map> mapsPerLevel, int potionLikelihood, Dictionary<MysteryRoomType, int> mysteryRoomTypeLikelihoods)
        {
            this.game = game;
            this.mapsPerLevel = mapsPerLevel;
            this.potionLikelihood = potionLikelihood;
            this.mysteryRoomTypeLikelihoods = mysteryRoomTypeLikelihoods;
        }
    }
}