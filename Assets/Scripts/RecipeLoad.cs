using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class RecipeLoad : MonoBehaviour
{
    [SerializeField]
    private GameObject _text;
    private Text _recipeName;
    private Text _recipeDescription;
    private Text _recipeCookingTime;
    private Text _textGuide;
    private Image _favoriteButtonImage;
    private Image _recipeImage;
    private Sprite _favoritedImage;
    private RectTransform _ingredientsContent;

    void Start()
    {
        _ingredientsContent = GameObject.Find("Ingredients").GetComponent<RectTransform>();
        _recipeCookingTime = GameObject.Find("CookingTime").GetComponent<Text>();
        _recipeDescription = GameObject.Find("RecipeDescription").GetComponent<Text>();
        _recipeName = GameObject.Find("RecipeName").GetComponent<Text>();
        _textGuide = GameObject.Find("TextGuide").GetComponent<Text>();       
        _favoriteButtonImage = GameObject.Find("AddFavorite").GetComponent<Image>();
        _recipeImage = GameObject.Find("Image").GetComponent<Image>();

        _recipeName.text = LoadData.SelectedRecipe.recipeName;
        _recipeDescription.text = LoadData.SelectedRecipe.description;
        _recipeCookingTime.text = LoadData.SelectedRecipe.cookingTime.ToString();
        _textGuide.text = LoadData.SelectedRecipe.guide;
        _recipeImage.sprite = Resources.Load<Sprite>(LoadData.SelectedRecipe.imagePath);

        if (Favorites.favoriteResipes.Any(r => r.id == LoadData.SelectedRecipe.id))
        {
            _favoritedImage = Resources.Load<Sprite>("Images/heartRED(1)");
            _favoriteButtonImage.sprite = _favoritedImage;
        }

    
        foreach (var item in LoadData.SelectedRecipe.ingredients)
        {
            GameObject recipe = Instantiate(_text, _ingredientsContent) as GameObject;
            recipe.GetComponent<Text>().text = item.ingredient.ingredientName + " - " + item.weight.ToString() +" Í„.";
        }
    }
}
