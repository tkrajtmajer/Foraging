using System;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private Material outlineMaterial;
    [SerializeField] private float interactionRange = 1.5f;
    [SerializeField] private LayerMask layerInteractable;
    private IInteractable canInteract;

    void Update()
    {
        if (UIManager.Instance.currentUIState == UIState.None) {
            CheckInteractables();
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!IsNotNull(canInteract)) return;

                if (canInteract.isHouse())
                {
                    canInteract.Interact();
                }
                else if(!Inventory.Instance.CheckInventoryFull()) {
                    //TryInteract();
                    canInteract.Interact();
                    canInteract = null;
                }
            }
        }
    }

    //void TryInteract()
    //{
    //    Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionRange, layerInteractable);

    //    foreach (var hit in hitColliders)
    //    {
    //        IInteractable interactable = hit.GetComponent<IInteractable>();
    //        if (interactable != null)
    //        {
    //            interactable.Interact();
    //            break; // avoid interaction with multiple interactables
    //        }
    //    }
    //}


    // draw a sphere gizmo in the editor to visually debug the interaction range
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }


    private void CheckInteractables()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionRange, layerInteractable);

        foreach (var hit in hitColliders)
        {
            IInteractable interactable = hit.GetComponent<IInteractable>();
            if (interactable != null)
            {   
                if (canInteract != interactable)
                {
                    if (IsNotNull(canInteract) && canInteract.isHouse()) return;

                    if (IsNotNull(canInteract)) canInteract.ChangeMaterial(outlineMaterial, false);
                    canInteract = interactable;
                    canInteract.ChangeMaterial(outlineMaterial, true);
                }
                return; // avoid interaction with multiple interactables
            }
        }

        // Nothing to interact with -> remove reference to object if not null on previous frame
        if (IsNotNull(canInteract))
        {
            if (!canInteract.isHouse()) canInteract.ChangeMaterial(outlineMaterial, false);
            canInteract = null;
        }
    }

    private bool IsNotNull(IInteractable interactable)
    {
        return (interactable != null) && !interactable.Equals(null);
    }
}
