using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInformation : IMessage
{
    public ItemInformation(AItem item)
    {
        this.item = item;
    }
    
    public AItem item;
    public void DoAction()
    {
        
    }
}
