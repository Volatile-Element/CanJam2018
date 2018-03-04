using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public UnityEventFor<float> OnDistanceChange = new UnityEventFor<float>();

    private float Distance;
    private Chronoium ClosestChronoium;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(ScanForChronoium());
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdateDistance();
    }

    private void UpdateDistance()
    {
        if (ClosestChronoium == null)
        {
            return;
        }

        var distance = Vector3.Distance(transform.position, ClosestChronoium.transform.position);
        if (distance != Distance)
        {
            Distance = distance;
            OnDistanceChange.Invoke(Distance);
        }
    }

    private IEnumerator ScanForChronoium()
    {
        while (true)
        {
            var chronoium = FindObjectsOfType<Chronoium>();

            foreach (var chrono in chronoium)
            {
                var distance = Vector3.Distance(transform.position, chrono.transform.position);
                if (ClosestChronoium == null || distance <= 0 || distance < Distance)
                {
                    Distance = distance;
                    ClosestChronoium = chrono;
                }
            }

            yield return new WaitForSeconds(1f);
        }
    }
}