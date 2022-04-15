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
        LeftAmmo.text = target.data.Ammo.ToString();
        MaxAmmo.text = target.data.MaxAmmo.ToString();
        TotalAmmo.text = target.data.TotalAmmo.ToString();
    }
}
