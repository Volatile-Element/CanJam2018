using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInformation : Singleton<UIInformation>
{
    private List<string> messagesToDisplay = new List<string>();

    private bool currentlyDisplayingInformation;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

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

    public void FadeIn(string message)
    {
        UITool.Get<Text>(transform, "txtInformation").text = message;

        animator.SetTrigger("Fade In");
    }

    public void FadeOut()
    {
        animator.SetTrigger("Fade Out");
    }
}