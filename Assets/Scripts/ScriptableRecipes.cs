using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

enum FoodTypes
{
    soup,
    salad,
    baking,
    pita,
    hotMeal,
    coldMeal,
    dessert,
    dairyDishes,
    sauses,
    porridge,
    drinks,
    snacks,
    seafood,
}

enum NationalTypes
{
    Russian,
    Italian,
    Japanese,
    European,
    Asian,
    PanAsian,
    Georgian,
    Mexican,
    Exotic,
}

[CreateAssetMenu(fileName = "New Recipe", menuName = "Recipe Data", order = 51)]
[System.Serializable]
public class ScriptableRecipes : ScriptableObject
{
    private string fileName = "recipes";
    private string FilePath => Path.Combine(Application.persistentDataPath, fileName + ".json");
    public List<Recipe> Recipes => recipes;
    [SerializeField]
    private List<Recipe> recipes;

    [ContextMenu("Сохранить в файл")]
    public void SaveToFile()
    {
        string dataString = JsonUtility.ToJson(new RecipeSet(recipes), true);

        if (!File.Exists(FilePath))
        {
            File.Create(FilePath);
            Debug.Log("Создан файл " + FilePath);
        }

        File.WriteAllText(FilePath, dataString);
    }

}

[System.Serializable]
public class RecipeSet
{
    public List<Recipe> recipes;

    public RecipeSet(List<Recipe> recipes)
    {
        this.recipes = recipes;
    }
}
[System.Serializable]
public class Recipe
{
    private int id;
    private static int counter = 0;
    [SerializeField]
    private string recipeName;
    [SerializeField]
    private NationalTypes national;
    [SerializeField]
    private FoodTypes type;
    [SerializeField]
    private string description;
    [SerializeField]
    [Multiline]
    private string guide;
    [SerializeField]
    private string imagePath;
    [SerializeField]
    private int cookingTime;
    [SerializeField]
    private List<IngredientData> ingredients;

    public Recipe()
    {
        id = counter;
        counter++;
    }
}

[System.Serializable]
public class IngredientData
{
    [SerializeField]
    private Ingredient ingredient;
    [SerializeField]
    private float weight;
}