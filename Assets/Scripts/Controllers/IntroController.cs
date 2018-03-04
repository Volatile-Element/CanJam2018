using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class IntroController : MonoBehaviour
{
    private void Start()
    {
        FindObjectOfType<FirstPersonController>().enabled = false;
        
        UIFade.Instance.InstantBlack();
        
        DoActionIn.Create(() => { UIInformation.Instance.FadeIn("You're a TimeX Agent from the year 2035."); }, 1f);
        DoActionIn.Create(() => { UIInformation.Instance.FadeOut(); }, 4f);
        
        DoActionIn.Create(() => { UIInformation.Instance.FadeIn("You just crashed whilst investigating a time tremor for President Musk."); }, 6f);
        DoActionIn.Create(() => { UIInformation.Instance.FadeOut(); }, 9f);
        
        DoActionIn.Create(() => { UIInformation.Instance.FadeIn("Luckily, Chronoium, the fuel for your time ship is still abundant in the Stone Age."); }, 11f);
        DoActionIn.Create(() => { UIInformation.Instance.FadeOut(); }, 14f);
        
        DoActionIn.Create(() => { UIInformation.Instance.FadeIn("You need to avoid the natives and collect enough Chronoium to power your ship."); }, 16f);
        DoActionIn.Create(() => { UIInformation.Instance.FadeOut(); }, 19f);
        
        DoActionIn.Create(() => { UIInformation.Instance.FadeIn("Oh, and your ship is about to explode."); }, 21f);
        DoActionIn.Create(() => { UIInformation.Instance.FadeOut(); }, 24f);
        
        DoActionIn.Create(() => { UIFade.Instance.FadeToClear(); }, 26f);
        
        DoActionIn.Create(() => { FindObjectOfType<FirstPersonController>().enabled = true; }, 27f);
    }
}