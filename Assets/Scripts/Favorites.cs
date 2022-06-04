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
    private GameObject _recipe;
    private RectTransform _content;
    private Text _textSearch;
    private Sprite _onFavorite;
    private Sprite _offFavorite;
    private Image _favoriteButtonImage;

    private string fileName = "favoritesRecipes";
    private string FilePath => Path.Combine(Application.persistentDataPath, fileName + ".json");
    public static List<Recipe> favoriteResipes = new List<Recipe>();

    public void AddToFavorites()
    {
        if(LoadData.SelectedRecipe.id != 0) 
        {
            if (favoriteResipes!.All(r => r.id != LoadData.SelectedRecipe.id))
            {
                favoriteResipes.Add(LoadData.SelectedRecipe);
                _favoriteButtonImage.sprite = _onFavorite;

                string dataToFile = JsonUtility.ToJson(new RecipeSet(favoriteResipes), true);

                if (!File.Exists(FilePath))
                {
                    File.Create(FilePath);
                }

                File.WriteAllText(FilePath, dataToFile);
            }
            else if (favoriteResipes.Any(r => r.id == LoadData.SelectedRecipe.id))
            {
                favoriteResipes.Remove(favoriteResipes.FirstOrDefault(r => r.id == LoadData.SelectedRecipe.id));
                _favoriteButtonImage.sprite = _offFavorite;

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
        if(_textSearch.text != null || !_textSearch.text.Equals(""))
        {
            foreach (Transform child in _content.transform)
            {
                GameObject.Destroy(child.gameObject);
            }

            var data = Favorites.favoriteResipes.Intersect(LoadData.Recipes);
            data = data.Where(r => r.recipeName.StartsWith(_textSearch.text));

            foreach (var item in data)
            {
                GameObject btn = Instantiate(_recipe, _content);

                btn.GetComponentInChildren<Text>().text = item.recipeName;

                btn.GetComponent<Button>().onClick.AddListener(() => LoadData.SelectedRecipe = item);
                btn.GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("Recipe"));
            }
        }
        else
        {
            var data = Favorites.favoriteResipes.Intersect(LoadData.Recipes);

            foreach (var item in data)
            {
                GameObject btn = Instantiate(_recipe, _content);

                btn.GetComponentInChildren<Text>().text = item.recipeName;

                btn.GetComponent<Button>().onClick.AddListener(() => LoadData.SelectedRecipe = item);
                btn.GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("Recipe"));
            }
        }
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("Favorites"))
        {
            _textSearch = GameObject.Find("TextSearch").GetComponent<Text>();
            LoadData.SelectedRecipe = null;
            _content = GameObject.Find("Content").GetComponent<RectTransform>();

            var data = Favorites.favoriteResipes.Intersect(LoadData.Recipes);

            foreach (var item in data)
            {
                GameObject btn = Instantiate(_recipe, _content);

                btn.GetComponentInChildren<Text>().text = item.recipeName;

                btn.GetComponent<Button>().onClick.AddListener(() => LoadData.SelectedRecipe = item);
                btn.GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("Recipe"));
            }
        }
        else
        {
            _onFavorite = Resources.Load<Sprite>("Images/heartRED(1)");
            _offFavorite = Resources.Load<Sprite>("Images/heartBLACK(1)");
            _favoriteButtonImage = GameObject.Find("AddFavorite").GetComponent<Image>();
        }

        if (SceneManager.GetActiveScene().name.Equals("Favorites"))
        {
            LoadData.PreviousScene = "Favorites";
        }
    }
}
