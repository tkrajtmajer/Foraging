using UnityEngine;
using System;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField] public int currentDay = 1;
    [SerializeField] private int maxDays = 7;

    [SerializeField] public ItemDatabase itemDatabase;
    internal Recipe currentRecipe; // used by UI
    //internal String[] discoveredItems; // maybe better hashmap? 
    private HashSet<ForageableData> discoveredItems = new HashSet<ForageableData>(); // hashset to prevent duplicate, also why internal before?

    public int finalMainSceneIdx = 2;
    public int mainSceneIdx = 3; //"MainScene";
    public int scoreSceneIdx = 4; //"ScoreScene";
    public int endSceneIdx = 5; //"EndScene";



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
        LevelManager.LevelLoaded += LoadLevel;
        InitDay(1, LevelManager.Instance.levelList[1].recipe);
    }

    private void OnDisable()
    {
        TimeManager.OnDayEnded -= UpdateTimeProgress;
        LevelManager.LevelLoaded -= LoadLevel;
    }

    private void UpdateTimeProgress() {
        FinishDay();
        //currentDay++;

        //if (currentDay > maxDays) {
        //    Debug.Log("End game for now");
        //} 
    }

    private void LoadLevel(LevelData levelData)
    {
        InitDay(levelData.levelNumber, levelData.recipe);
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

    public void InitDay(int day, Recipe recipe)
    {
        currentDay = day;
        if (currentRecipe != recipe) currentRecipe = recipe;
        //currentRecipe =  allRecipes[currentDay-1];
    }

    public void NextDay()
    {
        currentDay++;
        Debug.Log(currentDay);

        if (currentDay > maxDays){
            Debug.Log("Trigger finish game");
            GoToEndScene();
            return;
        }
        InitDay(currentDay, LevelManager.Instance.levelList[currentDay].recipe);
        GoToMainScene();
    }

    public void RestartDay()
    {
        InitDay(currentDay, currentRecipe);
        GoToMainScene();
    }

    public void RestartGame()
    {
        currentDay = 1;
        GoToMainScene();
    }

    public void GoToMainScene()
    {
        //ScreenFader.Instance.FadeAndLoadScene(mainSceneIdx);
        ScreenFader.Instance.FadeAndLoadScene(finalMainSceneIdx);
    }

    public void GoToScoreScene()
    {
        ScreenFader.Instance.FadeAndLoadScene(scoreSceneIdx);
    }

    public void GoToEndScene()
    {
        ScreenFader.Instance.FadeAndLoadScene(endSceneIdx);
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
