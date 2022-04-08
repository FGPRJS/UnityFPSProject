using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class ButtonWrapper : MonoBehaviour
{
    private Button baseButton;
    public UnityEvent<ButtonEventType, ButtonEventArgs> ButtonAdditionalEvent;

    public void SubscribeButtonEvent()
    {
        ButtonAdditionalEvent.AddListener(this.AdditionalEvent);
    }
    ~ButtonWrapper()
    {
        ButtonAdditionalEvent.RemoveAllListeners();
    }

    void Start()
    {
        this.baseButton = this.transform.GetComponent<Button>();
        this.baseButton.onClick.AddListener(this.ButtonAction);
        this.AdditionalStartAction();
    }

    protected virtual void ButtonAction()
    {
        Debug.Log("Button Clicked");
    }

    protected virtual void AdditionalEvent(ButtonEventType type, ButtonEventArgs arg)
    {
        Debug.Log("Button Additional Event Activated");
    }

    protected virtual void AdditionalStartAction()
    {

    }
}
