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

    [ContextMenu("��������� � ����")]
    public void SaveToFile()
    {
        string dataString = JsonUtility.ToJson(new IngredientSet(ingredients), true);

        if (!File.Exists(FilePath))
        {
            File.Create(FilePath);
            Debug.Log("������ ���� " + FilePath);
        }

        File.WriteAllText(FilePath, dataString);
    }
}

[System.Serializable]
public class Ingredient
{
    public int id;
    [SerializeField]
    private string ingredientName;
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