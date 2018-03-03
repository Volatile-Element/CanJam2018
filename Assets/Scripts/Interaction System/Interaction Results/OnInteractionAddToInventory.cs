using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnInteractionAddToInventory : MonoBehaviour, IOnInteraction
{
    public void OnInteraction(Interactable interactable)
    {
        PlayerInventory.Instance.AddInventoryItem(interactable.gameObject.name);

        Destroy(interactable.gameObject);
    }
}