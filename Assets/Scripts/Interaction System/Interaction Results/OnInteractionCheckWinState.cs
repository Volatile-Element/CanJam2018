using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnInteractionCheckWinState : MonoBehaviour, IOnInteraction
{
    public void OnInteraction(Interactable interactable)
    {
        var piecesLeft = GameManager.Instance.ShipPiecesToCollect - PlayerInventory.Instance.GetInventoryItems().Count;
        if (piecesLeft <= 0)
        {
            SceneManager.LoadScene("Game Win");
        }
        else
        {
            var s = piecesLeft == 1 ? "" : "s";
            UIInformation.Instance.AddMessageToDisplay($"You still need to find {piecesLeft} piece{s}.");
        }
    }
}