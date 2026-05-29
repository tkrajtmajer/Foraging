using UnityEngine;
using UnityEngine.EventSystems; // for drags

public class DragUI : MonoBehaviour, IDragHandler
{
    [Header("Drag Setup")]
    [SerializeField] private float rotationSpeed = 0.5f;

    [Header("3d Item Render")]
    [SerializeField] private Transform spawnPointModel; // point where the model is going to spawn
    private GameObject currentSpawnedModel;

    public void SetupDragRender(ForageableData forageableData) {
        // 3d render part
        if (currentSpawnedModel != null)
        {
            Destroy(currentSpawnedModel);
        }
        
        if (forageableData.modelPrefab != null)
        {
            currentSpawnedModel = Instantiate(forageableData.modelPrefab, spawnPointModel.position, Quaternion.identity);
            currentSpawnedModel.transform.SetParent(spawnPointModel);
        }
    }

    // this method is called ON THIS UI object when you click and drag
    public void OnDrag(PointerEventData eventData)
    {
        if (currentSpawnedModel != null)
        {
            // eventData.delta gives us the mouse movement since the last frame
            float rotX = -eventData.delta.y * rotationSpeed;
            float rotY = -eventData.delta.x * rotationSpeed;

            // Rotate the object around the camera's axes so the rotation feels intuitive
            currentSpawnedModel.transform.Rotate(Vector3.up, rotY, Space.World);
            currentSpawnedModel.transform.Rotate(Vector3.right, rotX, Space.World);
        }
    }

    public void CleanUp() {
        // clean up 3d model
        if (currentSpawnedModel != null)
        {
            Destroy(currentSpawnedModel);
        }
    }
}
