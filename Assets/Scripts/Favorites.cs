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
    private Text TextSearch;
    private Sprite OnFavorite;
    private Sprite OffFavorite;
    private Image FavoriteButtonImage;

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
                FavoriteButtonImage.sprite = OnFavorite;

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
                FavoriteButtonImage.sprite = OffFavorite;

                string dataToFile = JsonUtility.ToJson(new RecipeSet(favoriteResipes), true);

                if (!File.Exists(FilePath))
                {
                    File.Create(FilePath);
                }

                File.WriteAllText(FilePath, dataToFile);
            }
        } 
    }

    public void Search()
    {
        if(TextSearch.text != null || !TextSearch.text.Equals(""))
        {
            foreach (Transform child in content.transform)
            {
                GameObject.Destroy(child.gameObject);
            }

            var data = Favorites.favoriteResipes.Intersect(LoadData.Recipes);
            data = data.Where(r => r.recipeName.StartsWith(TextSearch.text));

            foreach (var item in data)
            {
                GameObject btn = Instantiate(recipe, content);

                btn.GetComponentInChildren<Text>().text = item.recipeName;

                btn.GetComponent<Button>().onClick.AddListener(() => FoundedRecipes.selectedRecipe = item);
                btn.GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("Recipe"));
            }
        }
        else
        {
            var data = Favorites.favoriteResipes.Intersect(LoadData.Recipes);

            foreach (var item in data)
            {
                GameObject btn = Instantiate(recipe, content);

                btn.GetComponentInChildren<Text>().text = item.recipeName;

                btn.GetComponent<Button>().onClick.AddListener(() => FoundedRecipes.selectedRecipe = item);
                btn.GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("Recipe"));
            }
        }
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("Favorites"))
        {
            TextSearch = GameObject.Find("TextSearch").GetComponent<Text>();
            FoundedRecipes.selectedRecipe = null;
            content = GameObject.Find("Content").GetComponent<RectTransform>();

            var data = Favorites.favoriteResipes.Intersect(LoadData.Recipes);

            foreach (var item in data)
            {
                GameObject btn = Instantiate(recipe, content);

                btn.GetComponentInChildren<Text>().text = item.recipeName;

                btn.GetComponent<Button>().onClick.AddListener(() => FoundedRecipes.selectedRecipe = item);
                btn.GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("Recipe"));
            }
        }
        else
        {
            OnFavorite = Resources.Load<Sprite>("Images/heartRED(1)");
            OffFavorite = Resources.Load<Sprite>("Images/heartBLACK(1)");
            FavoriteButtonImage = GameObject.Find("AddFavorite").GetComponent<Image>();
        }
    }
}
