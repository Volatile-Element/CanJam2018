using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    private void Awake()
    {
        if (!IsMainInstance())
        {
            return;
        }
    }

    public void PlaySound()
    {
        //TODO: Load from resources.
    }
}