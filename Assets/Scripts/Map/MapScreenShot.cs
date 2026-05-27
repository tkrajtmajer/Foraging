using System.Drawing;
using UnityEngine;

public class MapScreenShot : MonoBehaviour
{
    Camera camera;
    [SerializeField] TerrainData terrainData;
    [SerializeField] Terrain terrain;
    [SerializeField] Vector3 move;
    private void Awake()
    {
        camera = transform.GetComponent<Camera>();
        camera.aspect = (1 + 4f / 5f) * terrainData.size.x / (1 * terrainData.size.z);
        camera.orthographicSize = terrainData.size.z / 2f;
        //camera.transform.LookAt(terrain.GetPosition(), terrain.transform.forward);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            takeSS();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            transform.position += move;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            transform.position -= move;
        }
    }

    private void takeSS()
    {
        string m_Path = Application.dataPath;
        ScreenCapture.CaptureScreenshot(m_Path + "/MapPicture.png", 1);
        ScreenCapture.CaptureScreenshot(m_Path + "/MapPicture2x.png", 2);
        ScreenCapture.CaptureScreenshot(m_Path + "/MapPicture5x.png", 5);
        ScreenCapture.CaptureScreenshot(m_Path + "/MapPicture10x.png", 10);
    }
}
