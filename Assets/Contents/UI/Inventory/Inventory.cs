using System;
using System.Collections;
using System.Collections.Generic;
using Contents.Controller.Player;
using Contents.Mechanic;
using JetBrains.Annotations;
using UnityEngine;

namespace Contents.UI.Inventory
{
    public class Inventory : MonoBehaviour
    {
        public Player player;
        private AMecha target;
        public InventoryItem instance;
        private List<InventoryItem> inventoryItems;
        public GameObject inventoryContent;
        private long inventorySize;
        
        private void Awake()
        {
            target = player.target;
            inventoryItems = new List<InventoryItem>();
            inventorySize = target.inventorySize;
        }

        private void Start()
        {
            StartCoroutine(LoadInventoryItems());
            target.inventoryEvent.AddListener(InventoryEvent);
        }

        private IEnumerator LoadInventoryItems()
        {
            while (inventoryItems.Count < inventorySize)
            {
                var newInventoryItem = Instantiate(instance, inventoryContent.transform, false);
                inventoryItems.Add(newInventoryItem);
            }
            
            yield return null;
        }

        private void InventoryEvent(InventoryEventArgs arg0)
        {
            Refresh();
        }

        public void Refresh()
        {
            for (int i = 0; i < inventorySize; i++)
            {
                var item = target.inventory[i];

                //if Coroutine too slow to make instance
                if (inventoryItems[i] == null)
                {
                    inventoryItems[i] = Instantiate(instance, inventoryContent.transform, false);
                }
                
                if (item == null)
                {
                    inventoryItems[i].image.sprite = null;
                    inventoryItems[i].target = null;
                }
                else
                {
                    inventoryItems[i].image.sprite = item.image.sprite;
                    inventoryItems[i].target = item;
                }
            }
        }
    }
}
