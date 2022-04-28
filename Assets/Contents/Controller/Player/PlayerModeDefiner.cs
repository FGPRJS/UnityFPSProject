using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerModeDefiner
{
    private bool isActive;
    public UnityEvent ActiveStatusChange;

    public PlayerModeDefiner()
    {
        ActiveStatusChange = new UnityEvent();
    }
    
    public bool IsActive
    {
        get => isActive;
        set
        {
            isActive = value;
            ActiveStatusChange.Invoke();
        }
    }
}
