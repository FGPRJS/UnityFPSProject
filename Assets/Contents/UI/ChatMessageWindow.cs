using System.Collections.Generic;
using Contents.Controller;
using Contents.Controller.Player;
using UnityEngine;

namespace Contents.UI
{
    public class ChatMessageWindow : MonoBehaviour
    {
        public Player player;
        public GameObject content;

        public ChatMessage instance;
        private Queue<ChatMessage> usingMessageQueue;
        public long maxMessageCount;
        
        private void Awake()
        {
            usingMessageQueue = new Queue<ChatMessage>();
        }
        
        private void Start()
        {
            player.playerChatMessageUpdatedEvent.AddListener(PlayerMessageUpdated);
        }

        private void PlayerMessageUpdated(PlayerEventArgs arg)
        {
            AddMessage(arg.message);
        }

        public void AddMessage(string message)
        {
            var newChatMessage = Instantiate(instance, content.transform, false);
            usingMessageQueue.Enqueue(newChatMessage);

            while (usingMessageQueue.Count > maxMessageCount)
            {
                var removeTarget = usingMessageQueue.Dequeue();
                Destroy(removeTarget.gameObject);
            }
        }
    }
}
