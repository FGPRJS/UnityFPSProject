using System;
using Contents.Controller.Player;
using UnityEngine;

namespace Contents.UI
{
    public class UIManager : MonoBehaviour
    {
        public Player target;
        
        public HealthBar hpBar;
        public Ammo ammo;
        public CastingGuage castingGuage;
        public ChatMessageWindow chatWindow;
        public Inventory.Inventory inventory;

        public SkillIcon advancedSkill;
        
        
        private void Awake()
        {
            SetPlayerForUI(target);
        }

        public void SetPlayerForUI(Player player)
        {
            target = player;
            
            hpBar.player = target;
            ammo.player = target;
            castingGuage.player = target;
            chatWindow.player = target;
            inventory.player = target;

            advancedSkill.player = target;
            advancedSkill.targetSkill = target.target.skillAdvanced;
            
            
        }
    }
}
