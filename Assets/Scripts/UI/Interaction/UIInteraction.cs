using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInteraction : MonoBehaviour
{
    private void Awake()
    {
        SetupEvents();
    }

    private void SetupEvents()
    {
        var interactor = FindObjectOfType<Interactor>();
        interactor.OnInteractableInRange.AddListener(OnInteractableInRange);
        interactor.OnInteractableOutRange.AddListener(OnInteractableOutRange);
    }

    public void OnInteractableInRange(IInteractable interactable)
    {
        UITool.Get<Text>(transform, "txtInteraction").text = interactable.InteractionText;
    }

    public void OnInteractableOutRange()
    {
        UITool.Get<Text>(transform, "txtInteraction").text = "";
    }
}