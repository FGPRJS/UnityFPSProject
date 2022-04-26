using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUtility
{
    public static void HideObject(GameObject obj)
    {
        obj.transform.localScale = Vector3.zero;
    }

    public static void ShowObject(GameObject obj)
    {
        obj.transform.localScale = Vector3.one;
    }
}
