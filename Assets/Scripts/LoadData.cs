using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadData : MonoBehaviour
{
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
        if (System.IO.File.Exists(Application.persistentDataPath + "/ingredients.json"))
        {
            ingredientsJson = System.IO.File.ReadAllText(Application.persistentDataPath + "/ingredients.json");
        }
        if (System.IO.File.Exists(Application.persistentDataPath + "/recipes.json"))
        {
            recipesJson = System.IO.File.ReadAllText(Application.persistentDataPath + "/recipes.json");
        }

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

    }
}