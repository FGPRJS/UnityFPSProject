using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatMessage : MonoBehaviour
{
    public TextMeshProUGUI textGUI;

    public void ChangeText(string text)
    {
        textGUI.text = text;
    }
}
