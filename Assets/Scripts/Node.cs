using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;

namespace Map
{
    public class Node
    {
        public readonly Vector2Int point;
        public readonly List<Vector2Int> incoming = new List<Vector2Int>();
        public readonly List<Vector2Int> outgoing = new List<Vector2Int>();
        [JsonConverter(typeof(StringEnumConverter))]
        public readonly NodeType nodeType;
        public readonly string blueprintName;
        [JsonConverter(typeof(StringEnumConverter))]
        public MysteryRoomType? mysteryNodeType;
        public Vector2 position;
        public List<CreatureData> creatures;
        public List<ItemData> items;
        public List<PotionData> potions;
        public BoonData boon;
        public double treasure;

        public Node(NodeType nodeType, string blueprintName, Vector2Int point)
        {
            this.nodeType = nodeType;
            this.blueprintName = blueprintName;
            this.point = point;
            this.creatures = null;
            this.items = null;
            this.potions = null;
            this.boon = null;
            this.treasure = 0;
        }

        public void setCreatures(List<CreatureData> creatures)
        {
            this.creatures = creatures;
        }

        public void setTreasure(double treasure)
        {
            this.treasure = treasure;
        }
        public void setItems(List<ItemData> items)
        {
            this.items = items;
        }
        public void setPotions(List<PotionData> potions)
        {
            this.potions = potions;
        }
        public void setBoon(BoonData boon)
        {
            this.boon = boon;
        }

        public void setMysteryNodeType(MysteryRoomType mysteryNodeType)
        {
            this.mysteryNodeType = mysteryNodeType;
        }
        public void AddIncoming(Vector2Int p)
        {
            if (incoming.Any(element => element.Equals(p)))
                return;

            incoming.Add(p);
        }

        public void AddOutgoing(Vector2Int p)
        {
            if (outgoing.Any(element => element.Equals(p)))
                return;

            outgoing.Add(p);
        }

        public void RemoveIncoming(Vector2Int p)
        {
            incoming.RemoveAll(element => element.Equals(p));
        }

        public void RemoveOutgoing(Vector2Int p)
        {
            outgoing.RemoveAll(element => element.Equals(p));
        }

        public bool HasNoConnections()
        {
            return incoming.Count == 0 && outgoing.Count == 0;
        }
    }
}