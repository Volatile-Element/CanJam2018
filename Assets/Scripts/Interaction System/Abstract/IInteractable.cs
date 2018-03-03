using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    string InteractionText { get; set; }
    void Interact();
}