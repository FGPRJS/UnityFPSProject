using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Contents.UI
{
    public class InventoryItemInfo : MonoBehaviour
    {
        public Image image;
        public TextMeshProUGUI itemNameText;
        public TextMeshProUGUI itemExplanationText;
        public Button UseButton;
        public Button DropButton;

        private bool isOpen = false;

        public void ChangeItemInfo(ItemClickedEventArgs arg0)
        {
            var target = arg0.sender;
            var itemData = target.target;
        
            image.sprite = target.image.sprite;
            itemNameText.text = itemData.itemName;
            itemExplanationText.text = itemData.itemExplanation;
        }
    }
}
