using UnityEngine;

public interface IInteractable
{
    void Interact();
    void ChangeMaterial(Material mat, bool add);

    bool isHouse() { return false; }
}
