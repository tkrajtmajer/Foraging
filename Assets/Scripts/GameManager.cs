using UnityEngine;
using System;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int currentDay = 1;
    [SerializeField] private int maxDays = 7;

    [SerializeField] private ItemDatabase itemDatabase;
    internal Recipe currentRecipe; // used by UI
    private List<Recipe> allRecipes = new();
    //internal String[] discoveredItems; // maybe better strings? we save the name of each discovered item
    private HashSet<ForageableData> discoveredItems = new HashSet<ForageableData>(); // hashset to prevent duplicate, also why internal before?


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
    }

    private void OnEnable()
    {
        TimeManager.OnDayEnded += UpdateTimeProgress;
    }

    private void OnDisable()
    {
        TimeManager.OnDayEnded -= UpdateTimeProgress;
    }

    private void UpdateTimeProgress() {
        currentDay++;

        if (currentDay > maxDays) {
            Debug.Log("End game for now");
        } 

        // prepare next recipe
        currentRecipe = GenerateNewRecipe();
        allRecipes.Add(currentRecipe);

        // update recipe UI

        // place items
        PlaceItemsOnMap(currentRecipe);
    }

    private Recipe GenerateNewRecipe() {
        // create next recipe based on difficulty, for example on first few days, easy ingredients, after that harder
        return null;
    }

    private void PlaceItemsOnMap(Recipe recipe) {
        // TODO: for item in recipe : place item on map based on location
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
}
