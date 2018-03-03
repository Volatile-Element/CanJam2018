using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : Singleton<PlayerInventory>
{
    public UnityEventFor<string> OnInvetoryItemAdded = new UnityEventFor<string>();
    public UnityEventFor<string> OnInvetoryItemRemoved = new UnityEventFor<string>();

    private List<string> InventoryItems = new List<string>(); //TOOD: Make this complex, haha it's a game jam.

    public void AddInventoryItem(string item)
    {
        InventoryItems.Add(item);

        OnInvetoryItemAdded.Invoke(item);
    }

    public void RemoveInventoryItem(string item)
    {
        InventoryItems.Remove(item);

        OnInvetoryItemAdded.Invoke(item);
    }

    public void RemoveInventoryItemAt(int position)
    {
        var item = InventoryItems[position];

        InventoryItems.RemoveAt(position);
        
        OnInvetoryItemAdded.Invoke(item);
    }

    public List<string> GetInventoryItems()
    {
        return InventoryItems;
    }
}