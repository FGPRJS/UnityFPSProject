using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalConfigs
{
    private static GlobalConfigs instance;
    public static GlobalConfigs Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new GlobalConfigs();
            }

            return instance;
        }
    }



    public float Gravity = 9.81f;
}
