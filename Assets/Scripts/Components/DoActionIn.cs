using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoActionIn : MonoBehaviour
{
    public void StartDoActionIn(Action action, float secondsUntil)
    {
        StartCoroutine(RunCoroutineThenAction(action, secondsUntil));
    }

    public static void Create(Action action, float secondsUntil)
    {
        var newGameObject = new GameObject($"Do Action In: {secondsUntil}s - {action.Method.Name}");
        var doActionIn = newGameObject.AddComponent<DoActionIn>();

        doActionIn.StartDoActionIn(action, secondsUntil);
    }

    private IEnumerator RunCoroutineThenAction(Action action, float secondsUntil)
    {
        yield return new WaitForSeconds(secondsUntil);

        action.Invoke();

        Destroy(gameObject);
    }
}