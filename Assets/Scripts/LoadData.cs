using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum FoodTypes
{
    любая,
    супы,
    салаты,
    выпечка,
    лаваш,
    горячие,
    холодные,
    дессерты,
    молочные,
    соусы,
    каши,
    напитки,
    закуски,
    морские,
}
public enum NationalTypes
{
    любая,
    Русская,
    Итальянская,
    Японская,
    Европейская,
    Азиатская,
    Паназиатская,
    Кавказская,
    Мексиканская,
    Экзотическая,
}

public class LoadData : MonoBehaviour
{
    public static string SelectedRecipeType;
    public static string SelectedRecipeNational;
    public static string PreviousScene;
    public static Recipe SelectedRecipe = null;
    public static List<string> SelectedDesiredIngredients = new List<string>();
    public static List<string> SelectedUndesiredIngredients = new List<string>();
    public static List<Recipe> Recipes = new List<Recipe>();
    public static List<string> Ingredients = new List<string>();

    void Start()
    {
        Recipes.Clear();
        Ingredients.Clear();
        Favorites.favoriteResipes.Clear();
        string recipesJson = "";
        string ingredientsJson = "";
        string favoritesJson = "";

        if (System.IO.File.Exists(Application.persistentDataPath + "/favoritesRecipes.json"))
        {
            favoritesJson = System.IO.File.ReadAllText(Application.persistentDataPath + "/favoritesRecipes.json");
        }

        ingredientsJson = Resources.Load<TextAsset>("Data/ingredients").text;
        recipesJson = Resources.Load<TextAsset>("Data/recipes").text;


        var ingredientss = JsonUtility.FromJson<IngredientSet>(ingredientsJson);
        for (int i = 0; i < ingredientss.ingredients.Count; i++)
        {
            Ingredients.Add(ingredientss.ingredients[i].ingredientName);
        }

        var recipess = JsonUtility.FromJson<RecipeSet>(recipesJson);
        for(int i = 0; i < recipess.recipes.Count; i++)
        {
            Recipes.Add(recipess.recipes[i]);
        }

        if (!favoritesJson.Equals(""))
        {
            var favoritesRecipes = JsonUtility.FromJson<RecipeSet>(favoritesJson);
            for (int i = 0; i < favoritesRecipes.recipes.Count; i++)
            {
                Favorites.favoriteResipes.Add(favoritesRecipes.recipes[i]);
            }
        }

        SceneManager.LoadScene("MainScreen");

    }
}