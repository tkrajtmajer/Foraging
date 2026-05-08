using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private float interactionRange = 1.5f;
    [SerializeField] private LayerMask layerInteractable;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }
    }

    void TryInteract()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionRange, layerInteractable);

        foreach (var hit in hitColliders)
        {
            IInteractable interactable = hit.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
                break; // avoid interaction with multiple interactables
            }
        }
    }


    // draw a sphere gizmo in the editor to visually debug the interaction range
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }

}
