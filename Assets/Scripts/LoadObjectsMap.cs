using UnityEngine;
using System.Collections.Generic;

public class LoadObjectsMap : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsPerDay;

    // void OnEnable()
    // {
    //     GameManager.LoadObjects += LoadObjectsPerDay; 
    // }
    // void OnDisable()
    // {
    //     GameManager.LoadObjects -= LoadObjectsPerDay;        
    // }

    void Start() {
        LoadObjectsPerDay(GameManager.Instance.currentDay);
    }

    void LoadObjectsPerDay(int currentDay) {
        GameObject dayContainer = objectsPerDay[currentDay-1];
        dayContainer.SetActive(true);
        Debug.Log("objects loaded");
    }
}
