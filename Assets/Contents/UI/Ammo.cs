using System;
using System.Collections;
using System.Collections.Generic;
using Contents.Controller.Player;
using Contents.Mechanic;
using UnityEngine;
using TMPro;

public class Ammo : MonoBehaviour
{
    public TextMeshProUGUI LeftAmmo;
    public TextMeshProUGUI MaxAmmo;
    public TextMeshProUGUI TotalAmmo;

    public Player player;
    private AMecha targetMecha;

    private void Awake()
    {
        targetMecha = player.target;
    }

    // Update is called once per frame
    void Update()
    {
        if (!targetMecha) return;
        
        LeftAmmo.text = targetMecha.Ammo.ToString();
        MaxAmmo.text = targetMecha.MaxAmmo.ToString();
        TotalAmmo.text = targetMecha.TotalAmmo.ToString();
    }
}
