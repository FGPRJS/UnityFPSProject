using System.Collections;
using Contents.Controller.Player;
using Contents.Mechanic;
using UnityEngine;

namespace Contents.UI
{
    public class ItemNearbyMechaInfo : MonoBehaviour
    {
        public Player player;
        private AMecha targetMecha;

        public InventoryItem instance;
        private InventoryItem[] inventoryItems;
        private int itemCountLimit = 5;

        void Awake()
        {
            targetMecha = player.target;

            inventoryItems = new InventoryItem[itemCountLimit];
        }

        void Start()
        {
            StartCoroutine(LoadItemInfo());
        }

        IEnumerator LoadItemInfo()
        {
            for (int i = 0; i < inventoryItems.Length; i++)
            {
                inventoryItems[i] = Instantiate(instance, transform, false);
                inventoryItems[i].gameObject.SetActive(false);   
            }
        
            yield return null;
        }
    
        private void FixedUpdate()
        {
            #region ItemView
            //Append Item Info
            var nearbyMechaData = targetMecha.itemNearbyMechaInfo;
            var itemNearbyMechaInfo = nearbyMechaData.ToArray();

            var infoIndex = 0;
        
            for (var itemInfoIndex = 0; itemInfoIndex < itemCountLimit; itemInfoIndex++)
            {
                if (infoIndex >= itemNearbyMechaInfo.Length)
                {
                    inventoryItems[itemInfoIndex].gameObject.SetActive(false);
                    continue;
                }

                var item = itemNearbyMechaInfo[infoIndex];
            
                inventoryItems[itemInfoIndex].gameObject.SetActive(true);
                inventoryItems[itemInfoIndex].target = item;

                infoIndex++;
            }

        
            #endregion
        }
    }
}
