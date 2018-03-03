using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnInteractionDestroy : MonoBehaviour, IOnInteraction
{
    public void OnInteraction(Interactable interactable)
    {
        Destroy(interactable.gameObject);
    }
}