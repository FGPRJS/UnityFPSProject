using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModeManager
{
    private List<PlayerModeDefiner> uiModeDefiner;
    private List<PlayerModeDefiner> playModeDefiner;

    public PlayerModeManager()
    {
        uiModeDefiner = new List<PlayerModeDefiner>();
        playModeDefiner = new List<PlayerModeDefiner>();
    }
    
    public enum PlayerMode
    {
        Play,
        UI
    }
    
    private PlayerMode mode;
    public PlayerMode Mode
    {
        get => mode;
        set
        {
            bool result = false;
            
            //if other definer is all off, able to change
            switch (value)
            {
                case PlayerMode.Play:
                    foreach (var item in uiModeDefiner)
                    {
                        //if single definer is ON, not able to change
                        result |= item.IsActive;
                        if (result) return;
                    }
                    break;
                
                case PlayerMode.UI:
                    foreach (var item in playModeDefiner)
                    {
                        //if single definer is ON, not able to change
                        result |= item.IsActive;
                        if (result) return;
                    }
                    break;
            }
            
            mode = value;
        }
    }

    
    
    public void AddUIModeDefiner(PlayerModeDefiner definer)
    {
        uiModeDefiner.Add(definer);
        definer.ActiveStatusChange.AddListener(UIModeDefinerActivationChanged);
    }
    
    public void AddPlayModeDefiner(PlayerModeDefiner definer)
    {
        playModeDefiner.Add(definer);
        definer.ActiveStatusChange.AddListener(PlayModeDefinerActivationChanged);
    }
    
    private void UIModeDefinerActivationChanged()
    {
        Mode = PlayerMode.UI;
    }

    private void PlayModeDefinerActivationChanged()
    {
        Mode = PlayerMode.Play;
    }
}
