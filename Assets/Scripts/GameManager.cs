using UnityEngine;
using System;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField] public int currentDay = 1;
    [SerializeField] private int maxDays = 7;

    [SerializeField] public ItemDatabase itemDatabase;
    [SerializeField] private List<Recipe> allRecipes = new List<Recipe>();
    internal Recipe currentRecipe; // used by UI
    //internal String[] discoveredItems; // maybe better hashmap? 
    private HashSet<ForageableData> discoveredItems = new HashSet<ForageableData>(); // hashset to prevent duplicate, also why internal before?


    public string mainSceneName = "MainScene";
    public string scoreSceneName = "ScoreScene";
    public string endSceneName = "EndScene";



    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        //Debug.Log(currentDay);
    }

    private void OnEnable()
    {
        TimeManager.OnDayEnded += UpdateTimeProgress;
        InitDay(1);
    }

    private void OnDisable()
    {
        TimeManager.OnDayEnded -= UpdateTimeProgress;
    }

    private void UpdateTimeProgress() {
        FinishDay();
        //currentDay++;

        //if (currentDay > maxDays) {
        //    Debug.Log("End game for now");
        //} 
    }


    public void SpawnRandomItem(Vector3 spawnPosition)
    {
        if (itemDatabase.allItemPrefabs.Count == 0) return;

        // Pick a random prefab from the ItemDatabase database
        int randomIndex = UnityEngine.Random.Range(0, itemDatabase.allItemPrefabs.Count);
        GameObject prefabToSpawn = itemDatabase.allItemPrefabs[randomIndex];

        Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
    }


    public void FinishDay()
    {
        GoToScoreScene();
    }

    public void InitDay(int day)
    {
        currentDay = day;
        currentRecipe = allRecipes[currentDay-1];
    }

    public void NextDay()
    {
        currentDay++;
        Debug.Log(currentDay);

        if (currentDay > allRecipes.Count){
            Debug.Log("Trigger finish game");
            GoToEndScene();
            return;
        }
        InitDay(currentDay);
        GoToMainScene();
    }

    public void RestartDay()
    {
        InitDay(currentDay);
        GoToMainScene();
    }

    public void RestartGame()
    {
        currentDay = 1;
        GoToMainScene();
    }

    public void GoToMainScene()
    {
        ScreenFader.Instance.FadeAndLoadScene(mainSceneName);
    }

    public void GoToScoreScene()
    {
        ScreenFader.Instance.FadeAndLoadScene(scoreSceneName);
    }

    public void GoToEndScene()
    {
        ScreenFader.Instance.FadeAndLoadScene(endSceneName);
    }




    // TODO: this has to be subscribed to the interaction event that will make discover the item (i still didnt understand when its going to be discovered D:)
    public void CheckIfDiscovered(ForageableInteractable interactedObject)
    {
        ForageableData itemData = interactedObject.Data;

        // HashSet.Add returns true if it's a new item, false if it already exists
        if (discoveredItems.Add(itemData))
        {
            Debug.Log($"New item discovered: {itemData.name}!");
            // unlock almanac entry, etc.
        }
        else
        {
            Debug.Log($"You already knew about: {itemData.name}");
        }
    }


    public static int GetRecipeScore()
    {
        int score = 0;

        // we copy the player inventory list to remove the matching items
        List<ForageableInteractable> playerForageables = new List<ForageableInteractable>();
        foreach (var item in Inventory.Instance.inventory)
        {
            playerForageables.Add(item.interactable);
        }

        foreach (ForageableInteractable neededItem in Instance.currentRecipe.forageablesInRecipe)
        {
            for (int i = 0; i < playerForageables.Count; i++)
            {
                ForageableInteractable playerItem =
                    playerForageables[i].GetComponent<ForageableInteractable>();

                if (playerItem != null &&
                    playerItem.Data.itemName == neededItem.Data.itemName)
                {
                    Debug.Log(playerItem.Data.itemName);
                    score++;
                    playerForageables.RemoveAt(i); // we remove to prevent double matching
                    break;
                }
            }
        }

        return score;
    }


}
