using NUnit.Framework;
using System;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class AreaMap : MonoBehaviour
{
    public static AreaMap Instance { get; private set; }
    private AreaType currentArea { get => MapManager.Instance.currentArea; set => MapManager.Instance.currentArea = currentArea; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public enum AreaType
    {
        Forest,
        Field,
        Beach,
        River,
    }

    [Serializable]
    public class AreaData
    {
        public AreaType type;
        public List<Item> forageables; // EDIT WHEN FORAGEABLES PUSHED
        public Sprite sprite;
        public GameObject obj;
    }

    public List<AreaData> AreaDataList;

    public AreaData GetAreaData(AreaType type) => AreaDataList[(int)type];
    public AreaData GetAreaData(int idx) => AreaDataList[idx];

    public void SetCurrentAreaMap(AreaType newAreaType)
    {
        AreaData newAreaData = GetAreaData(newAreaType);
        transform.GetComponent<UnityEngine.UI.Image>().sprite = newAreaData.sprite;
        currentArea = newAreaData.type;
    }
}
