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
    private GameObject g_recipe;
    [SerializeField]
    private GameObject g_nothingFoundText;
    private RectTransform rt_content;
    private Button b_btnBack;

    public static RectTransform BackDisplayForRecipe;

    private void Awake()
    {
        rt_content = GameObject.Find("Recipes").GetComponent<RectTransform>();
        b_btnBack = GameObject.Find("ButtonBackRecipes").GetComponent<Button>();
    }

    void OnEnable()
    {
        LoadData.SelectedRecipe = null;

        foreach (Transform child in rt_content.transform)
        {
            Destroy(child.gameObject);
        }

        if (Search.nameSearch != null)
        {
            var recipes = LoadData.Recipes.Where(r => r.recipeName != null && r.recipeName
                                       .Split(' ').Any(r => r.StartsWith(Search.nameSearch, true, CultureInfo.CurrentCulture)));
            if (recipes.Count() > 0)
            {
                foreach (var item in recipes)
                {
                    GameObject recipe = Instantiate(g_recipe, rt_content);

                    recipe.GetComponentInChildren<Text>().text = item.recipeName;

                    recipe.GetComponent<Button>().onClick.AddListener(() => LoadData.SelectedRecipe = item);
                    recipe.GetComponent<Button>().onClick.AddListener(() => SceneChanger.LoadDisplay(MainScreen.RecipeDisplay));
                   
                }
            }
            else
            {
                GameObject t_nothingFound = Instantiate(g_nothingFoundText, rt_content);
                t_nothingFound.GetComponentInChildren<Text>().text = "<b>Ничего не найдено</b>";
            }

            BackDisplayForRecipe = MainScreen.NameSearchDisplay;
            b_btnBack.onClick.AddListener(() => SceneChanger.LoadDisplay(MainScreen.NameSearchDisplay));

            Search.nameSearch = null;
        }
        else
        {
            IEnumerable<Recipe> recipes = LoadData.Recipes;
            if (LoadData.SelectedDesiredIngredients.Count > 0)
            {
                recipes = recipes.Where(r => r.ingredients.Select(r => r.ingredient.ingredientName)
                                 .Intersect(LoadData.SelectedDesiredIngredients)
                                 .Count() == LoadData.SelectedDesiredIngredients.Count());
            }
            if (LoadData.SelectedUndesiredIngredients.Count > 0)
            {
                recipes = recipes.Where(r => r.ingredients
                                 .Select(r => r.ingredient.ingredientName)
                                 .Intersect(LoadData.SelectedUndesiredIngredients).Count() == 0);
            }
            if (LoadData.SelectedRecipeType != FoodTypes.любая.ToString())
            {
                recipes = recipes.Where(r => Enum.GetName(typeof(FoodTypes), r.type).Equals(LoadData.SelectedRecipeType));
            }
            if (LoadData.SelectedRecipeNational != NationalTypes.любая.ToString())
            {
                recipes = recipes.Where(r => Enum.GetName(typeof(NationalTypes), r.national).Equals(LoadData.SelectedRecipeNational));
            }

            if (recipes.Count() > 0)
            {
                foreach (var item in recipes)
                {
                    GameObject recipe = Instantiate(g_recipe, rt_content);

                    recipe.GetComponentInChildren<Text>().text = item.recipeName;

                    recipe.GetComponent<Button>().onClick.AddListener(() => LoadData.SelectedRecipe = item);
                    recipe.GetComponent<Button>().onClick.AddListener(() => SceneChanger.LoadDisplay(MainScreen.RecipeDisplay));
                }
            }
            else
            {
                GameObject t_nothingFound = Instantiate(g_nothingFoundText, rt_content);
                t_nothingFound.GetComponentInChildren<Text>().text = "<b>Ничего не найдено</b>";
            }

            BackDisplayForRecipe = MainScreen.SearchByIngredientsDisplay;
            b_btnBack.onClick.AddListener(() => SceneChanger.LoadDisplay(MainScreen.SearchByIngredientsDisplay));
            LoadData.SelectedDesiredIngredients.Clear();
            LoadData.SelectedUndesiredIngredients.Clear();
        }
  
    }

}
