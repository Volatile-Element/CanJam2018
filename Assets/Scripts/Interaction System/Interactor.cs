using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactor : MonoBehaviour
{
    public UnityEventFor<IInteractable> OnInteractableInRange = new UnityEventFor<IInteractable>();
    public UnityEvent OnInteractableOutRange = new UnityEvent();

    private Camera camera;
    private IInteractable interactableLookingAt;

    private void Awake()
    {
        camera = GetComponent<Camera>();
    }
	
	void Update ()
    {
        LookForInteractables();
        CheckInteractInput();
    }

    private void LookForInteractables()
    {
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        var result = Physics.Raycast(ray, out hit, 50f);
        if (!result)
        {
            interactableLookingAt = null;
            OnInteractableOutRange.Invoke();

            return;
        }

        var interactable = hit.collider.gameObject.GetComponent<IInteractable>();
        if (interactable == null)
        {
            return;
        }

        interactableLookingAt = interactable;
        OnInteractableInRange.Invoke(interactable);
    }

    private void CheckInteractInput()
    {
        if (interactableLookingAt == null)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            interactableLookingAt.Interact();
        }
    }
}