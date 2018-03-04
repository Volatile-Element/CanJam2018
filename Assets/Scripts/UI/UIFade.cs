using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : Singleton<UIFade>
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void FadeToBlack()
    {
        animator.SetTrigger("Fade to Black");
    }

    public void FadeToClear()
    {
        animator.SetTrigger("Fade to Clear");
    }

    public void InstantBlack()
    {
        UITool.Get<Image>(transform, "imgFade").color = Color.black;

        animator.SetTrigger("Instant Black");
    }
}
