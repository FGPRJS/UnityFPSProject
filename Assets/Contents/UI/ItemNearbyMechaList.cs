using System.Collections;
using Contents.Controller.Player;
using Contents.Mechanic;
using UnityEngine;

namespace Contents.UI
{
    public class ItemNearbyMechaList : MonoBehaviour
    {
        public Player player;
        private AMecha targetMecha;

        public ItemNearbyMecha instance;
        private ItemNearbyMecha[] nearbyItems;
        private int itemCountLimit = 5;

        void Awake()
        {
            targetMecha = player.target;

            nearbyItems = new ItemNearbyMecha[itemCountLimit];
        }

        void Start()
        {
            StartCoroutine(LoadItemInfo());
        }

        IEnumerator LoadItemInfo()
        {
            for (int i = 0; i < nearbyItems.Length; i++)
            {
                nearbyItems[i] = Instantiate(instance, transform, false);
                nearbyItems[i].gameObject.SetActive(false);   
            }
        
            yield return null;
        }
    
        private void Update()
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
                    nearbyItems[itemInfoIndex].gameObject.SetActive(false);
                    continue;
                }

                var item = itemNearbyMechaInfo[infoIndex];
            
                nearbyItems[itemInfoIndex].gameObject.SetActive(true);
                nearbyItems[itemInfoIndex].Target = item;

                infoIndex++;
            }

        
            #endregion
        }
    }
}
