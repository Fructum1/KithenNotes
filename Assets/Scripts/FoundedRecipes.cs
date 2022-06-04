using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Globalization;

public class FoundedRecipes : MonoBehaviour
{
    [SerializeField]
    private GameObject _recipe;
    [SerializeField]
    private GameObject _nothingFoundText;
    private RectTransform _content;

    void Start()
    {
        LoadData.SelectedRecipe = null;
        _content = GameObject.Find("Content").GetComponent<RectTransform>();

        if (Search.nameSearch != null)
        {
            var data = LoadData.Recipes.Where(r => r.recipeName != null && r.recipeName
                                       .Split(' ').Any(r => r.StartsWith(Search.nameSearch, true, CultureInfo.CurrentCulture)));
            if (data.Count() > 0)
            {
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
                GameObject btn = Instantiate(_nothingFoundText, _content);
                btn.GetComponentInChildren<Text>().text = "<b>Ничего не найдено</b>";
            }

            Search.nameSearch = null;
        }
        else
        {
            IEnumerable<Recipe> data = LoadData.Recipes;
            if (LoadData.SelectedDesiredIngredients.Count > 0)
            {
                data = data.Where(r => r.ingredients.Select(r => r.ingredient.ingredientName)
                           .Intersect(LoadData.SelectedDesiredIngredients)
                           .Count() == LoadData.SelectedDesiredIngredients.Count());
            }
            if (LoadData.SelectedUndesiredIngredients.Count > 0)
            {
                data = data.Where(r => r.ingredients
                           .Select(r => r.ingredient.ingredientName)
                           .Intersect(LoadData.SelectedUndesiredIngredients).Count() == 0);
            }
            if (LoadData.SelectedRecipeType != FoodTypes.любая.ToString())
            {
                data = data.Where(r => Enum.GetName(typeof(FoodTypes), r.type).Equals(LoadData.SelectedRecipeType));
            }
            if (LoadData.SelectedRecipeNational != NationalTypes.любая.ToString())
            {
                data = data.Where(r => Enum.GetName(typeof(NationalTypes), r.national).Equals(LoadData.SelectedRecipeNational));
            }

            if (data.Count() > 0)
            {
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
                GameObject btn = Instantiate(_nothingFoundText, _content);
                btn.GetComponentInChildren<Text>().text = "<b>Ничего не найдено</b>";
            }
        }
  
    }

}
