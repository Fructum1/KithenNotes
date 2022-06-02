using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeLoad : MonoBehaviour
{
    [SerializeField]
    private GameObject text;
    private Text t_RecipeName;
    private Text t_RecipeDescription;
    private Text t_RecipeCookingTime;
    private RectTransform ingredientsContent;
    private RectTransform guideContent;

    void Start()
    {
        ingredientsContent = GameObject.Find("Ingredients").GetComponent<RectTransform>();
        guideContent = GameObject.Find("Guide").GetComponent<RectTransform>();
        t_RecipeCookingTime = GameObject.Find("CookingTime").GetComponent<Text>();
        t_RecipeDescription = GameObject.Find("RecipeDescription").GetComponent<Text>();
        t_RecipeName = GameObject.Find("RecipeName").GetComponent<Text>();

        t_RecipeName.text = FoundedRecipes.selectedRecipe.recipeName;
        t_RecipeDescription.text = FoundedRecipes.selectedRecipe.description;
        t_RecipeCookingTime.text = FoundedRecipes.selectedRecipe.cookingTime.ToString();

        foreach (var item in FoundedRecipes.selectedRecipe.ingredients)
        {
            GameObject texts = Instantiate(text, ingredientsContent) as GameObject;
            texts.GetComponent<Text>().text = item.ingredient.ingredientName + " - " + item.weight.ToString() +" Í„.";
        }
    }

}
