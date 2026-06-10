using UnityEngine;

public class ShowPlayerInMap : MonoBehaviour
{
    // Need map size to accurately implement
    private GameObject player;
    private Camera cam;
    private RectTransform rect;
    [SerializeField] private GameObject map;
    private RectTransform mapRect;

    [SerializeField] public Vector2 worldToMapScale;
    [SerializeField] Vector2 mapOriginVec;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        cam = Camera.main;
        rect = transform.GetComponent<RectTransform>();

        mapRect = map.GetComponent<RectTransform>();

        Vector2 mapSize = mapRect.rect.size;
        Vector2 worldSize = new Vector2(100, 100);
        worldToMapScale = new Vector2(mapSize.x / worldSize.x, mapSize.y / worldSize.y);
    }

    private void Update()
    {
        Vector2 pos = WorldToMapPos(player.transform.position);
        rect.anchoredPosition = pos;

        if (Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log(rect.position);
            Debug.Log(rect.anchoredPosition);
        }
    }

    private Vector2 WorldToMapPos(Vector3 playerWorldPos)
    {
        Vector2 worldOriginVec = new Vector2(player.transform.position.x, player.transform.position.z);
        mapOriginVec = new Vector2(worldOriginVec.x * worldToMapScale.x, worldOriginVec.y * worldToMapScale.y);
        Vector2 mapLocalPos = new Vector2(mapRect.localPosition.x, mapRect.localPosition.y);

        return mapLocalPos - mapRect.rect.size / 2 + mapOriginVec;
    }

}
