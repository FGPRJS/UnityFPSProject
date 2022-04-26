using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class ButtonWrapper : MonoBehaviour
{
    private Button baseButton;

    protected virtual void Start()
    {
        baseButton = GetComponent<Button>();
        baseButton.onClick.AddListener(ButtonAction);
    }


    protected virtual void ButtonAction()
    {

    }

    protected virtual void OnDestroy()
    {
        baseButton.onClick.RemoveAllListeners();
    }
}
