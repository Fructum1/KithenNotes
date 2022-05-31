using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


[CreateAssetMenu(fileName = "New Ingredient", menuName = "Ingredient Data", order = 52)]
[System.Serializable]
public class ScriptableIngredient : ScriptableObject
{
    private string fileName = "ingredients";
    private string FilePath => Path.Combine(Application.persistentDataPath, fileName + ".json");
    public List<Ingredient> Ingredients => ingredients;
    [SerializeField]
    private List<Ingredient> ingredients;

    [ContextMenu("Сохранить в файл")]
    public void SaveToFile()
    {
        string dataString = JsonUtility.ToJson(new IngredientSet(ingredients), true);

        if (!File.Exists(FilePath))
        {
            File.Create(FilePath);
            Debug.Log("Создан файл " + FilePath);
        }

        File.WriteAllText(FilePath, dataString);
    }
}

[System.Serializable]
public class Ingredient
{
    private static int counter = 0;
    public int id;
    [SerializeField]
    private string ingredientName;

    public Ingredient()
    {
        id = counter;
        counter++;
    }
}

[System.Serializable]
public class IngredientSet
{
    public List<Ingredient> ingredients;

    public IngredientSet(List<Ingredient> ingredients)
    {
        this.ingredients = ingredients;
    }
}