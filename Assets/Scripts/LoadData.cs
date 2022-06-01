using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadData : MonoBehaviour
{
    public static List<Recipe> Recipes = new List<Recipe>();

    void Start()
    {
        Recipes.Clear();
        string json = "";
        if(System.IO.File.Exists(Application.persistentDataPath + "/recipes.json"))
        {
            json = System.IO.File.ReadAllText(Application.persistentDataPath + "/recipes.json");
        }
        var recipess = JsonUtility.FromJson<RecipeSet>(json);
        for(int i = 0; i < recipess.recipes.Count; i++)
        {
            Recipes.Add(recipess.recipes[i]);
        }
        
    }
}