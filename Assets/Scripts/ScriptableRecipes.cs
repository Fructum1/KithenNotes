using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Recipe Data", order = 51)]
[System.Serializable]
public class ScriptableRecipes : ScriptableObject
{
    private string _fileName = "recipes";
    private string FilePath => Path.Combine("Assets/Resources/Data", _fileName + ".json");
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
            File.WriteAllText(FilePath, dataString);
            Debug.Log("Successfully");
        }
        else
        {
            File.WriteAllText(FilePath, dataString);
            Debug.Log("Successfully");
        }
        
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
    public int id;
    public string recipeName;
    public NationalTypes national;
    public FoodTypes type;
    public string description;
    [Multiline]
    public string guide;
    public string imagePath;
    public int cookingTime;
    public List<IngredientData> ingredients;

    public override bool Equals(object obj)
    {
        if (obj is Recipe recipe) return id == recipe.id;
        return false;
    }
    public override int GetHashCode() => id.GetHashCode();
}

[System.Serializable]
public class IngredientData
{
    public Ingredient ingredient;
    public double weight;
}