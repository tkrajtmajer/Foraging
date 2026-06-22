using UnityEngine;
using UnityEngine.UI;

public class TutorialAlmanacButton : MonoBehaviour
{

    [SerializeField] GameObject items;
    AlmanacItemUI item;
    Button button;

    void Start()
    {
        item = items.GetComponentInChildren<AlmanacItemUI>();
        button = transform.GetComponent<Button>();
        button.onClick.AddListener(item.SelectItem);
    }
}
