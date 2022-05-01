using System;
using Contents.Item;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Contents.UI
{
    public class InventoryItem : MonoBehaviour, 
        IPointerClickHandler
    {
        public Image image;
        public AItem target;
        

        public UnityEvent<ItemClickedEventArgs> ClickedEvent;

        private void Awake()
        {
            ClickedEvent = new UnityEvent<ItemClickedEventArgs>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!target) return;

            var arg = new ItemClickedEventArgs();
            arg.sender = this;
            ClickedEvent.Invoke(arg);
        }
    }
}
