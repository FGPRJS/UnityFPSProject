using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConfigManager
{
    private static PlayerConfigManager instance;

    public static PlayerConfigManager Instance 
    {
        get
        {
            if(instance == null)
            {
                instance = new PlayerConfigManager();
            }

            return instance;
        }
    }



    public float sensibility = 2.0f;
}
