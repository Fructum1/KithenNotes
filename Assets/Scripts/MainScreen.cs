using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreen : MonoBehaviour
{
    public static RectTransform MainScreenDisplay;
    public static RectTransform NameSearchDisplay;
    public static RectTransform RecipeDisplay;
    public static RectTransform SearchByIngredientsDisplay;
    public static RectTransform FavoritesDisplay;
    public static RectTransform FoundRecipesDisplay;
    public static RectTransform ActiveDisplay;

    void Start()
    {
        Initializing();
    }

    void Initializing()
    {
        MainScreenDisplay = GameObject.Find("MainScreenDisplay").GetComponent<RectTransform>();
        NameSearchDisplay = GameObject.Find("NameSearchDisplay").GetComponent<RectTransform>();
        RecipeDisplay = GameObject.Find("RecipeDisplay").GetComponent<RectTransform>();
        SearchByIngredientsDisplay = GameObject.Find("SearchByIngredientsDisplay").GetComponent<RectTransform>();
        FavoritesDisplay = GameObject.Find("FavoritesDisplay").GetComponent<RectTransform>();
        FoundRecipesDisplay = GameObject.Find("FoundRecipesDisplay").GetComponent<RectTransform>();

        NameSearchDisplay.gameObject.SetActive(false);
        RecipeDisplay.gameObject.SetActive(false);
        SearchByIngredientsDisplay.gameObject.SetActive(false);
        FavoritesDisplay.gameObject.SetActive(false);
        FoundRecipesDisplay.gameObject.SetActive(false);

        ActiveDisplay = MainScreenDisplay;
    }
}
