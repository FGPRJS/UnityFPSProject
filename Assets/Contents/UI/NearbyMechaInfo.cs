using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class NearbyMechaInfo : MonoBehaviour
{
    public Player player;
    private AMecha targetMecha;

    public ItemInfo itemInfo;
    public ItemInfo[] ItemInfos;
    private int itemCountLimit = 5;

    void Awake()
    {
        targetMecha = player.target;

        ItemInfos = new ItemInfo[itemCountLimit];
    }

    void Start()
    {
        StartCoroutine(LoadItemInfo());
    }

    IEnumerator LoadItemInfo()
    {
        for (int i = 0; i < ItemInfos.Length; i++)
        {
            ItemInfos[i] = Instantiate(itemInfo, transform, false);
            ItemInfos[i].gameObject.SetActive(false);   
        }
        
        yield return null;
    }
    
    private void FixedUpdate()
    {
        #region ItemView
        //Remove Item Info
        foreach (var child in transform.GetComponentsInChildren<ItemInfo>())
        {
            child.gameObject.SetActive(false);
        }
        
        //Append Item Info
        var itemInfoIndex = 0;

        var itemNearbyMechaInfo = targetMecha.itemNearbyMechaInfo;
        
        for (var i = 0; i < itemNearbyMechaInfo.Count; i++)
        {
            if (itemInfoIndex >= itemCountLimit) break;
            
            var item = itemNearbyMechaInfo[i];
            ItemInfos[itemInfoIndex].target = item;
            ItemInfos[itemInfoIndex].textGUI.text = item.itemName;

            itemInfoIndex++;
        }

        
        #endregion
    }
}
