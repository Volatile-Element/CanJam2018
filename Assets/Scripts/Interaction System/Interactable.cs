using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    public string _InteractionText;
    public string InteractionText { get { return _InteractionText; } set { _InteractionText = value; } }

    IOnInteraction onInteraction;

    private void Awake()
    {
        onInteraction = GetComponent<IOnInteraction>();
    }

    public void Interact()
    {
        onInteraction.OnInteraction(this);
    }
}