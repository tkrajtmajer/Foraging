using System;
using UnityEngine;

public class ForageableInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private ForageableData forageableData;
    public ForageableData Data => forageableData; // public getter, cool

    public static event Action<ForageableInteractable> OnForageableInteracted;

    public void Interact()
    {
        Debug.Log("Interacted with forageable object!");
        // emit signal to UI to display forageable data
        OnForageableInteracted?.Invoke(this);

        // TODO: Disable player movement?
        // Already done in InspectUI, right?
    }

    public void Collect()
    {
        // if the player chooses to collect, UI calls this method to destroy object
        Destroy(gameObject);
    }

    public void ChangeMaterial(Material mat, bool add)
    {
        MeshRenderer[] rendererList = transform.GetComponentsInChildren<MeshRenderer>();
        if (rendererList.Length > 0) Debug.Log("found some");
        foreach (MeshRenderer renderer in rendererList)
        {
            int lengthMats = renderer.materials.Length;
            Debug.Log(lengthMats);
            if (add)
            {
                Debug.Log("Added Material");
                renderer.materials[lengthMats - 1] = mat;
                Debug.Log(renderer.materials);
            }
            else
            {
                renderer.materials[lengthMats - 1] = renderer.materials[lengthMats - 2];
            }
        }
    }
}
