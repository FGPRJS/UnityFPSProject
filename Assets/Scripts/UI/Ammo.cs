using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ammo : MonoBehaviour
{
    public TextMeshProUGUI LeftAmmo;
    public TextMeshProUGUI MaxAmmo;
    public TextMeshProUGUI TotalAmmo;
    public MechaBase target;

    
    // Update is called once per frame
    void Update()
    {
        LeftAmmo.text = target.Ammo.ToString();
        MaxAmmo.text = target.MaxAmmo.ToString();
        TotalAmmo.text = target.TotalAmmo.ToString();
    }
}
