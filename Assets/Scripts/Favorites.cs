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
    private GameObject recipe;
    private RectTransform content;


    private string fileName = "favoritesRecipes";
    private string FilePath => Path.Combine(Application.persistentDataPath, fileName + ".json");
    public static List<Recipe> favoriteResipes = new List<Recipe>();

    public void AddToFavorites()
    {
        if(FoundedRecipes.selectedRecipe.id != 0) 
        {
            if (favoriteResipes!.All(r => r.id != FoundedRecipes.selectedRecipe.id))
            {
                favoriteResipes.Add(FoundedRecipes.selectedRecipe);

                string dataToFile = JsonUtility.ToJson(new RecipeSet(favoriteResipes), true);

                if (!File.Exists(FilePath))
                {
                    File.Create(FilePath);
                }

                File.WriteAllText(FilePath, dataToFile);
            }
            else if (favoriteResipes.Any(r => r.id == FoundedRecipes.selectedRecipe.id))
            {
                favoriteResipes.Remove(favoriteResipes.FirstOrDefault(r => r.id == FoundedRecipes.selectedRecipe.id));

                string dataToFile = JsonUtility.ToJson(new RecipeSet(favoriteResipes), true);

                if (!File.Exists(FilePath))
                {
                    File.Create(FilePath);
                }

                File.WriteAllText(FilePath, dataToFile);
            }
        } 
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("Favorites"))
        {
            FoundedRecipes.selectedRecipe = null;
            content = GameObject.Find("Content").GetComponent<RectTransform>();

            var data = Favorites.favoriteResipes.Intersect(LoadData.Recipes).ToList();

            foreach (var item in data)
            {
                GameObject btn = Instantiate(recipe, content);

                btn.GetComponentInChildren<Text>().text = item.recipeName;

                btn.GetComponent<Button>().onClick.AddListener(() => FoundedRecipes.selectedRecipe = item);
                btn.GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("Recipe"));
            }
        }
    }
}
