using System;
using Contents.Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Contents.UI
{
    public class ItemNearbyMecha : MonoBehaviour
    {
        [SerializeField]
        private AItem target;
        public TextMeshProUGUI itemNameGUI;
        private string itemName;

        private void Start()
        {
            itemName = target.itemName;
        }

        public AItem Target
        {
            get => target;
            set
            {
                target = value;
                ItemName = target.itemName;
            }
        }
        
        public string ItemName
        {
            get => itemName;
            set
            {
                itemName = value;
                itemNameGUI.text = itemName;
            }
        }
        
    }
}
