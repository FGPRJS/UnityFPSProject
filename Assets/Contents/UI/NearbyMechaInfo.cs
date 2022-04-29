using System;
using System.Collections;
using System.Collections.Generic;
using Contents.Controller.Player;
using Contents.Mechanic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class NearbyMechaInfo : MonoBehaviour
{
    public Player player;
    private AMecha targetMecha;

    public ItemInfo itemInfo;
    private ItemInfo[] itemInfos;
    private int itemCountLimit = 5;

    void Awake()
    {
        targetMecha = player.target;

        itemInfos = new ItemInfo[itemCountLimit];
    }

    void Start()
    {
        StartCoroutine(LoadItemInfo());
    }

    IEnumerator LoadItemInfo()
    {
        for (int i = 0; i < itemInfos.Length; i++)
        {
            itemInfos[i] = Instantiate(itemInfo, transform, false);
            itemInfos[i].gameObject.SetActive(false);   
        }
        
        yield return null;
    }
    
    private void FixedUpdate()
    {
        #region ItemView
        //Append Item Info
        var nearbyMechaData = targetMecha.itemNearbyMechaInfo;
        var itemNearbyMechaInfo = nearbyMechaData.ToArray();

        var infoIndex = 0;
        
        for (var itemInfoIndex = 0; itemInfoIndex < itemCountLimit; itemInfoIndex++)
        {
            if (infoIndex >= itemNearbyMechaInfo.Length)
            {
                itemInfos[itemInfoIndex].gameObject.SetActive(false);
                continue;
            }

            var item = itemNearbyMechaInfo[infoIndex];
            
            itemInfos[itemInfoIndex].gameObject.SetActive(true);
            itemInfos[itemInfoIndex].target = item;
            itemInfos[itemInfoIndex].textGUI.text = item.itemName;

            infoIndex++;
        }

        
        #endregion
    }
}
