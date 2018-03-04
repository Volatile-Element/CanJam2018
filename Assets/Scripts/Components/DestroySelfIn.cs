using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelfIn : MonoBehaviour
{
    public float SecondsToLive = 1;

    private void Start()
    {
        StartCoroutine(DestroySelf());
    }

    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(SecondsToLive);

        Destroy(gameObject);
    }
}