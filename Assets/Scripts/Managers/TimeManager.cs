using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : Singleton<TimeManager>
{
    public UnityEventFor<int> OnTimeChanged = new UnityEventFor<int>();

    public int SecondsRemaining = 300;
    
	// Use this for initialization
	void Start ()
    {
        StartCoroutine(TickTime());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator TickTime()
    {
        OnTimeChanged.Invoke(SecondsRemaining);

        while (true)
        {
            yield return new WaitForSeconds(1f);

            SecondsRemaining -= 1;
            OnTimeChanged.Invoke(SecondsRemaining);
        }
    }
}
