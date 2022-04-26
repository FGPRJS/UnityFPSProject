using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicMaterialManager
{
    public enum MaterialType
    {
        Null,
        Metal
    }

    public static MaterialType ToMaterialType(string name)
    {
        try
        {
            var type = (MaterialType) Enum.Parse(typeof(MaterialType), name);
            return type;
        }
        catch (ArgumentException ex)
        {
            return MaterialType.Null;
        }
    }
}
