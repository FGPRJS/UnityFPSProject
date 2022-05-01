using System;
using System.Collections;
using System.Collections.Generic;
using Contents.Controller.Player;
using Contents.Mechanic;
using JetBrains.Annotations;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

namespace Contents.UI.Inventory
{
    public class Inventory : MonoBehaviour
    {
        public enum WindowStatus
        {
            Open,
            Close
        }

        public WindowStatus windowStatus;
        public Button windowTriggerButton;
        public string windowOpenAnimationName, windowCloseAnimationName;
        public Animator windowAnimator;
        public InventoryContent inventoryContent;

        public Player player;
        private AMecha target;
        public InventoryItem instance;
        private List<InventoryItem> inventoryItems;
        public InventoryItemInfo itemInfo;

        
        private long inventorySize;
        
        private void Awake()
        {
            windowStatus = WindowStatus.Close;
            inventoryItems = new List<InventoryItem>();
        }

        private void Start()
        {
            Initialize();
            
            StartCoroutine(LoadInventoryItems());
            windowTriggerButton.onClick.AddListener(InventoryTrigger);
        }

        public void Initialize()
        {
            target = player.target;
            inventorySize = target.inventorySize;
            target.inventoryEvent.AddListener(InventoryEvent);
        }

        private void InventoryTrigger()
        {
            switch (windowStatus)
            {
                case WindowStatus.Close:
                    windowStatus = WindowStatus.Open;
                    windowAnimator.Play(windowOpenAnimationName);
                    break;
                
                case WindowStatus.Open:
                    windowStatus = WindowStatus.Close;
                    windowAnimator.Play(windowCloseAnimationName);
                    inventoryContent.CloseItemDetail();
                    break;
            }
        }

        private IEnumerator LoadInventoryItems()
        {
            while (inventoryItems.Count < inventorySize)
            {
                var newInventoryItem = CreateInventoryItem();
                inventoryItems.Add(newInventoryItem);
            }
            
            yield return null;
        }

        private InventoryItem CreateInventoryItem()
        {
            var item = Instantiate(instance, inventoryContent.itemGrid.transform, false);

            //Add Clicked Event for each item
            item.ClickedEvent.AddListener(itemInfo.ChangeItemInfo);
            item.ClickedEvent.AddListener(ItemClickedEvent);
            
            return item;
        }

        private void ItemClickedEvent(ItemClickedEventArgs arg0)
        {
            inventoryContent.OpenItemDetail();
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

                //if Coroutine is too slow to make instance
                if (inventoryItems[i] == null)
                {
                    inventoryItems[i] = CreateInventoryItem();
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
