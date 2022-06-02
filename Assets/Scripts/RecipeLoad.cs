using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class RecipeLoad : MonoBehaviour
{
    [SerializeField]
    private GameObject text;
    private Text t_RecipeName;
    private Text t_RecipeDescription;
    private Text t_RecipeCookingTime;
    private Text t_TextGuide;
    private Image FavoriteButtonImage;
    private RectTransform IngredientsContent;

    void Start()
    {
        IngredientsContent = GameObject.Find("Ingredients").GetComponent<RectTransform>();
        t_RecipeCookingTime = GameObject.Find("CookingTime").GetComponent<Text>();
        t_RecipeDescription = GameObject.Find("RecipeDescription").GetComponent<Text>();
        t_RecipeName = GameObject.Find("RecipeName").GetComponent<Text>();
        t_TextGuide = GameObject.Find("TextGuide").GetComponent<Text>();
        FavoriteButtonImage = GameObject.Find("AddFavorite").GetComponent<Image>();

        t_RecipeName.text = FoundedRecipes.selectedRecipe.recipeName;
        t_RecipeDescription.text = FoundedRecipes.selectedRecipe.description;
        t_RecipeCookingTime.text = FoundedRecipes.selectedRecipe.cookingTime.ToString();
        t_TextGuide.text = FoundedRecipes.selectedRecipe.guide;

        if(Favorites.favoriteResipes.Any(r => r.id == FoundedRecipes.selectedRecipe.id))
        {
            FavoriteButtonImage = Resources.Load("/images/heartRED(1)", typeof(Image)) as Image;
        }

        foreach (var item in FoundedRecipes.selectedRecipe.ingredients)
        {
            GameObject texts = Instantiate(text, IngredientsContent) as GameObject;
            texts.GetComponent<Text>().text = item.ingredient.ingredientName + " - " + item.weight.ToString() +" Í„.";
        }
    }
}
