using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float interactionPointDistance = 1f;
    [SerializeField] private LayerMask interactionLayer;
    [SerializeField] private UIPrompt interactionPromptUI; 

    private readonly Collider[] colliders = new Collider[3];
    [SerializeField]private int interactablesFound;

    private IInteractable interactable; 
    private void Update()
    {
        interactablesFound = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionPointDistance, colliders, interactionLayer);

        if (interactablesFound > 0)
        {
            var interactable = colliders[0].GetComponent<IInteractable>();

            if (interactable != null)
            {
                if (!interactionPromptUI.displayed) interactionPromptUI.SetUp(interactable.InteractPrompt);

                if (Keyboard.current.eKey.wasPressedThisFrame) interactable.Interact(this);
            }
        }
        else
        {
            if (interactable !=null) interactable = null;
            if (interactionPromptUI.displayed) interactionPromptUI.Close();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionPointDistance);
    }
}
