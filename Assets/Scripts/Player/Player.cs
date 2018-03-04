using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Player : Singleton<Player>
{
    private bool clobbering;

    private void OnTriggerEnter(Collider other)
    {
        if (clobbering)
        {
            return;
        }

        var enemy = other.GetComponent<Caveman>();
        if (enemy == null)
        {
            return;
        }

        clobbering = true;
        FindObjectOfType<FirstPersonController>().enabled = false;
        GetComponent<Animator>().enabled = true;
        GetComponent<Animator>().SetTrigger("Clobbered");

        DoActionIn.Create(() => { UIFade.Instance.FadeToBlack(); UIInformation.Instance.AddMessageToDisplay("Clobbered!"); }, 1.45f);
        DoActionIn.Create(() =>
        {
            transform.position = WorldManager.Instance.GetRandomPointInWorld() + new Vector3(0, 1, 0);

            FindObjectOfType<FirstPersonController>().enabled = true;
            GetComponent<Animator>().enabled = false;

            TimeManager.Instance.SecondsRemaining -= 30;

            UIFade.Instance.FadeToClear();
            
            clobbering = false;
        }, 3f);
    }
}