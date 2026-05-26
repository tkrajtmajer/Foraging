using UnityEngine;

public class ShowPlayerInMap : MonoBehaviour
{
    // Need map size to accurately implement
    private GameObject player;
    private Camera cam;
    private RectTransform rect;
    private Vector3 playerPos;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        cam = Camera.main;
        rect = transform.GetComponent<RectTransform>();
    }

    private void Update()
    {
        rect.position = new Vector3(player.transform.position.x, player.transform.position.z, 0);
    }


}
