using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using DG.Tweening;
using Newtonsoft.Json;
using UnityEngine;

namespace Map
{
    public class MapPlayerTracker : MonoBehaviour
    {
        public bool lockAfterSelecting = false;
        public float enterNodeDelay = 1f;
        public MapManager mapManager;
        public MapView view;

        public static MapPlayerTracker Instance;

        public bool Locked { get; set; }

        private void Awake()
        {
            Instance = this;
        }

        public void SelectNode(MapNode mapNode)
        {
            if (Locked) return;

            // Debug.Log("Selected node: " + mapNode.Node.point);

            if (MapManager.getCurrentMap().path.Count == 0)
            {
                // player has not selected the node yet, he can select any of the nodes with y = 0
                if (mapNode.Node.point.y == 0)
                    SendPlayerToNode(mapNode);
                else
                    PlayWarningThatNodeCannotBeAccessed(mapNode);
            }
            else
            {
                Vector2Int currentPoint = MapManager.getCurrentMap().path[MapManager.getCurrentMap().path.Count - 1];
                Node currentNode = MapManager.getCurrentMap().GetNode(currentPoint);

                if (currentNode != null && currentNode.outgoing.Any(point => point.Equals(mapNode.Node.point)))
                    SendPlayerToNode(mapNode);
                else
                    PlayWarningThatNodeCannotBeAccessed(mapNode);
            }
        }

        private void SendPlayerToNode(MapNode mapNode)
        {
            Locked = lockAfterSelecting;
            MapManager.getCurrentMap().path.Add(mapNode.Node.point);
            mapManager.SaveGame();
            view.SetAttainableNodes();
            view.SetLineColors();
            mapNode.ShowSwirlAnimation();

            DOTween.Sequence().AppendInterval(enterNodeDelay).OnComplete(() => EnterNode(mapNode));
        }

        private static void logData(Node mapNode)
        {
            foreach (var creature in mapNode.creatures)
            {
                Debug.Log("Creature: " + creature.name + " level: " + creature.level);
                Debug.Log("Creature environments: ");
                foreach (var env in creature.environment)
                {
                    Debug.Log(env);
                }
            }
            foreach (var item in mapNode.items)
            {
                Debug.Log("Item: " + item.name + " level: " + item.level + " price: " + item.price);
            }
            foreach (var potion in mapNode.potions)
            {
                Debug.Log("Potion: " + potion.name + " level: " + potion.level + " price: " + potion.price + " rarity: " + potion.rarity);
            }
            if (mapNode.boon != null)
            {
                Debug.Log("BOON: " + mapNode.boon.name + " description: " + mapNode.boon.description + " rarity: " + mapNode.boon.rarity);
            }
            else
            {
                Debug.Log("NO BOON");
            }
            if (mapNode.treasure != 0)
            {
                Debug.Log("TREASURE: " + mapNode.treasure + "gp");
            }
        }

        private static void EnterNode(MapNode mapNode)
        {

            MapManager.getCurrentMap().isInProgress = true;

            if(mapNode.Node.nodeType == NodeType.Boss) {
                MapManager.getCurrentMap().isComplete = true;
            }
            
            Debug.Log("----------------------------------------------------------");

            Debug.Log("Entering node: " + mapNode.Node.blueprintName + " of type: " + mapNode.Node.nodeType);

            if (mapNode.Node.nodeType == NodeType.Mystery)
            {

                MysteryRoomType computedRoomType = MysteryGenerator.computeMysteryRoomType();
                mapNode.Node.setMysteryNodeType(computedRoomType);

                switch (computedRoomType)
                {
                    case MysteryRoomType.MinorEnemy:
                    case MysteryRoomType.Treasure:
                        Debug.Log("MYSTERY ROOM TYPE IS: " + computedRoomType.ToString());
                        MapView.setupNodeByType(mapNode.Node);
                        break;
                    case MysteryRoomType.Store:
                        Debug.Log("MYSTERY ROOM TYPE IS: STORE");
                        break;
                    case MysteryRoomType.DemonDoor:
                        Debug.Log("MYSTERY ROOM TYPE IS: DEMON DOOR");
                        break;
                    default:
                        Debug.Log("MYSTERY ROOM TYPE IS:EVENT");
                        break;
                }
            }
            else
            {
                // we have access to blueprint name here as well
                if (mapNode.Node.nodeType == NodeType.MinorEnemy || mapNode.Node.nodeType == NodeType.EliteEnemy || mapNode.Node.nodeType == NodeType.Boss)
                {
                    List<PotionData> potionsToAdd = EncounterBuilder.computePotionsForEncounter(GameManager.currentGame.partySize, GameManager.currentGame.partyLevel, mapNode.Node);
                    mapNode.Node.setPotions(potionsToAdd);
                }

                logData(mapNode.Node);
            }
        }

        private void PlayWarningThatNodeCannotBeAccessed(MapNode mapNode)
        {
               if(mapNode.Node.nodeType == NodeType.Boss) {
                MapManager.getCurrentMap().isComplete = true;
            }
            Debug.Log("Selected node cannot be accessed");
            logData(mapNode.Node);
        }
    }
}