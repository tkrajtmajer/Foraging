using System;
using UnityEngine;

public class ForageableInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private ForageableData forageableData;
    public ForageableData Data => forageableData; // public getter, cool

    public static event Action<ForageableData> OnForageableInteracted;

    public void Interact()
    {
        Debug.Log("Interacted with forageable object!");
        // emit signal to UI to display forageable data
        OnForageableInteracted?.Invoke(forageableData);

        // TODO: Disable player movement?
        // Already done in InspectUI, right?
    }

    public void Collect()
    {
        // if the player chooses to collect, UI calls this method to destroy object
        Inventory.Instance.TryAddObject(this);
        Destroy(gameObject);
    }

}
