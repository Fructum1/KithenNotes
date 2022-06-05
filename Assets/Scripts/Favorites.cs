using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Favorites : MonoBehaviour
{
    [SerializeField]
    private GameObject g_recipe;
    private RectTransform rt_content;
    private Text t_textSearch;

    public void Search()
    {
        foreach (Transform child in rt_content.transform)
        {
            Destroy(child.gameObject);
        }

        if (t_textSearch.text != null && !t_textSearch.text.Equals(String.Empty))
        {
            var favoriteRecipes = LoadData.favoriteResipes.Intersect(LoadData.Recipes);
            favoriteRecipes = favoriteRecipes.Where(r => r.recipeName.Split(' ')
                       .Any(r => r.StartsWith(t_textSearch.text, true, System.Globalization.CultureInfo.CurrentCulture)));

            foreach (var item in favoriteRecipes)
            {
                GameObject recipe = Instantiate(g_recipe, rt_content);

                recipe.GetComponentInChildren<Text>().text = item.recipeName;

                recipe.GetComponent<Button>().onClick.AddListener(() => LoadData.SelectedRecipe = item);
                recipe.GetComponent<Button>().onClick.AddListener(() => SceneChanger.LoadDisplay(MainScreen.RecipeDisplay));
            }
        }
        else
        {
            var favoriteRecipes = LoadData.favoriteResipes.Intersect(LoadData.Recipes);

            foreach (var item in favoriteRecipes)
            {
                GameObject recipe = Instantiate(g_recipe, rt_content);

                recipe.GetComponentInChildren<Text>().text = item.recipeName;

                recipe.GetComponent<Button>().onClick.AddListener(() => LoadData.SelectedRecipe = item);
                recipe.GetComponent<Button>().onClick.AddListener(() => SceneChanger.LoadDisplay(MainScreen.RecipeDisplay));
            }
        }
    }


    private void Awake()
    {
        rt_content = GameObject.Find("Favorites").GetComponent<RectTransform>();
        t_textSearch = GameObject.Find("TextSearch").GetComponent<Text>();
    }

    private void OnEnable()
    {
        FoundedRecipes.BackDisplayForRecipe = MainScreen.FavoritesDisplay;

        if(LoadData.SelectedRecipe != null) LoadData.SelectedRecipe = null;
        
        foreach (Transform child in rt_content.transform)
        {
            Destroy(child.gameObject);
        }

        var favoriteRecipes = LoadData.favoriteResipes.Intersect(LoadData.Recipes);

        foreach (var item in favoriteRecipes)
        {
            GameObject recipe = Instantiate(g_recipe, rt_content);

            recipe.GetComponentInChildren<Text>().text = item.recipeName;

            recipe.GetComponent<Button>().onClick.AddListener(() => LoadData.SelectedRecipe = item);
            recipe.GetComponent<Button>().onClick.AddListener(() => SceneChanger.LoadDisplay(MainScreen.RecipeDisplay));
        }
    }
}
