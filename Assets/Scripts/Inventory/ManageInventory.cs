using UnityEngine;

public class ManageInventory : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] GameObject mush;
    public int currentSlot;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            currentSlot = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentSlot = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentSlot = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentSlot = 3;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            inventory.TryAddObject(mush);
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            inventory.TryRemoveObject(currentSlot);
        }
    }

}
