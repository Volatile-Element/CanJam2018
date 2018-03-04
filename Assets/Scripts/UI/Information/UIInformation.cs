using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInformation : Singleton<UIInformation>
{
    private List<string> messagesToDisplay = new List<string>();

    private bool currentlyDisplayingInformation;

	public void AddMessageToDisplay(string message)
    {
        messagesToDisplay.Add(message);

        if (!currentlyDisplayingInformation)
        {
            DisplayNextMessage();
        }
    }

    private void DisplayNextMessage()
    {
        currentlyDisplayingInformation = true;

        UITool.Get<Text>(transform, "txtInformation").text = messagesToDisplay[0];

        messagesToDisplay.RemoveAt(0);

        if (messagesToDisplay.Count > 0)
        {
            DisplayNextMessage();
        }
        else
        {
            currentlyDisplayingInformation = false;
        }
    }
}