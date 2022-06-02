using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class FoundedRecipes : MonoBehaviour
{
    [SerializeField]
    private GameObject recipe;
    private RectTransform content;
    public static Recipe selectedRecipe = null;

    void Start()
    {
        selectedRecipe = null;
        content = GameObject.Find("Content").GetComponent<RectTransform>();

        if (Search.nameSearch != null)
        {
            var data = LoadData.Recipes.Where(r => r.recipeName != null && r.recipeName.Contains(Search.nameSearch)).ToList();
            foreach (var item in data)
            {
                GameObject btn = Instantiate(recipe, content);

                btn.GetComponentInChildren<Text>().text = item.recipeName;

                btn.GetComponent<Button>().onClick.AddListener(() => selectedRecipe = item);
                btn.GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("Recipe"));
                Search.nameSearch = null;
            }
        }
        else
        {
            IEnumerable<Recipe> data = LoadData.Recipes;
            if (AdvencedSearch.selectedDesiredIngredients.Count > 0)
            {
                data = data.Where(r => r.ingredients.Select(r => r.ingredient.ingredientName).Intersect(AdvencedSearch.selectedDesiredIngredients).Count() == AdvencedSearch.selectedDesiredIngredients.Count());
            }
            if (AdvencedSearch.selectedUndesiredIngredients.Count > 0)
            {
                data = data.Where(r => r.ingredients.Select(r => r.ingredient.ingredientName).Intersect(AdvencedSearch.selectedUndesiredIngredients).Count() == 0);
            }
            if(AdvencedSearch.RecipeType != FoodTypes.любая.ToString())
            {
                data = data.Where(r => Enum.GetName(typeof(FoodTypes), r.type).Equals(AdvencedSearch.RecipeType));
            }
            if (AdvencedSearch.RecipeNational != NationalTypes.любая.ToString())
            {
                data = data.Where(r => Enum.GetName(typeof(NationalTypes), r.type).Equals(AdvencedSearch.RecipeNational));
            }

            foreach (var item in data)
            {
                GameObject btn = Instantiate(recipe, content);

                btn.GetComponentInChildren<Text>().text = item.recipeName;

                btn.GetComponent<Button>().onClick.AddListener(() => selectedRecipe = item);
                btn.GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("Recipe"));
            }
        }
    }

}
