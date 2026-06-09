using UnityEngine;

public class ShowPlayerInMap : MonoBehaviour
{
    // Need map size to accurately implement
    private GameObject player;
    private Camera cam;
    private RectTransform rect;
    [SerializeField] private GameObject env;

    [SerializeField] Vector2 bottomLeft = new Vector2(-209.4f, -182.6f); // +- 50
    [SerializeField] Vector2 topRight = new Vector2(210.8f, 188.4f);
    [SerializeField] public Vector2 worldToMapScale;
    [SerializeField] Vector2 mapOriginVec;
    //[SerializeField] Vector2 moveScale = new Vector2(1, 1);

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        cam = Camera.main;
        rect = transform.GetComponent<RectTransform>();

        Vector2 mapSize = new Vector2(topRight.x - bottomLeft.x, topRight.y - bottomLeft.y);
        Vector2 worldSize = new Vector2(100, 100);
        worldToMapScale = new Vector2(mapSize.x / worldSize.x, mapSize.y / worldSize.y);
    }

    private void Update()
    {
        Vector2 pos = WorldToMapPos(player.transform.position + env.transform.position);
        rect.anchoredPosition = pos;

        if (Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log(rect.position);
            Debug.Log(rect.anchoredPosition);
        }
    }

    private Vector2 WorldToMapPos(Vector3 playerWorldPos)
    {
        Vector2 worldOriginVec = new Vector3(player.transform.position.x, player.transform.position.z);
        mapOriginVec = new Vector2(worldOriginVec.x * worldToMapScale.x, worldOriginVec.y * worldToMapScale.y);

        return bottomLeft + mapOriginVec;
    }

}
