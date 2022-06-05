using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.IO;

public class RecipeLoad : MonoBehaviour
{
    [SerializeField]
    private GameObject g_text;
    private Text t_recipeName;
    private Text t_recipeDescription;
    private Text t_recipeCookingTime;
    private Text t_textGuide;
    private Text t_textGuideFullScreen;
    private Image i_favoriteButtonImage;
    private Image i_recipeImage;
    private Sprite s_offFavorite;
    private Sprite s_onFavorite;
    private Button b_btnBack;
    private RectTransform rt_ingredientsContent;
    private RectTransform rt_scrollViewFullScreen;
    private RectTransform rt_scrollViewMain;
    private string fileName = "favoritesRecipes";
    private string FilePath => Path.Combine(Application.persistentDataPath, fileName + ".json");

    public void FullScreenGuide()
    {
        rt_scrollViewFullScreen.gameObject.SetActive(!rt_scrollViewFullScreen.gameObject.activeSelf);
        rt_scrollViewMain.gameObject.SetActive(!rt_scrollViewMain.gameObject.activeSelf);
    }

    public void Favorite()
    {
        if (LoadData.SelectedRecipe.id != 0)
        {
            if (LoadData.favoriteResipes!.All(r => r.id != LoadData.SelectedRecipe.id))
            {
                LoadData.favoriteResipes.Add(LoadData.SelectedRecipe);
                i_favoriteButtonImage.sprite = s_onFavorite;

                string dataToFile = JsonUtility.ToJson(new RecipeSet(LoadData.favoriteResipes), true);

                WriteToExistFileOrCreateIt(FilePath, dataToFile);
            }

            else if (LoadData.favoriteResipes.Any(r => r.id == LoadData.SelectedRecipe.id))
            {
                LoadData.favoriteResipes.Remove(LoadData.favoriteResipes.FirstOrDefault(r => r.id == LoadData.SelectedRecipe.id));
                i_favoriteButtonImage.sprite = s_offFavorite;

                string dataToFile = JsonUtility.ToJson(new RecipeSet(LoadData.favoriteResipes), true);

                WriteToExistFileOrCreateIt(FilePath, dataToFile);
            }
        }
    }

    private void WriteToExistFileOrCreateIt(string filePath, string dataToWrite)
    {
        if (!File.Exists(filePath))
        {
            File.Create(FilePath);
        }

        File.WriteAllText(FilePath, dataToWrite);
    }

    void Awake()
    {
        rt_ingredientsContent = GameObject.Find("Ingredients").GetComponent<RectTransform>();
        rt_scrollViewFullScreen = GameObject.Find("ScrollViewFullScreen").GetComponent<RectTransform>();
        rt_scrollViewMain = GameObject.Find("ScrollViewMain").GetComponent<RectTransform>();
        t_recipeCookingTime = GameObject.Find("CookingTime").GetComponent<Text>();
        t_recipeDescription = GameObject.Find("RecipeDescription").GetComponent<Text>();
        t_recipeName = GameObject.Find("RecipeName").GetComponent<Text>();
        t_textGuide = GameObject.Find("TextGuide").GetComponent<Text>();
        t_textGuideFullScreen = GameObject.Find("TextGuideFullScreen").GetComponent<Text>();
        i_favoriteButtonImage = GameObject.Find("AddFavorite").GetComponent<Image>();
        i_recipeImage = GameObject.Find("RecipeImage").GetComponent<Image>();
        b_btnBack = GameObject.Find("ButtonBackRecipe").GetComponent<Button>();

        rt_scrollViewFullScreen.gameObject.SetActive(false);
    }

    void OnEnable()
    {
        t_recipeName.text = LoadData.SelectedRecipe.recipeName;
        t_recipeDescription.text = LoadData.SelectedRecipe.description;
        t_recipeCookingTime.text = LoadData.SelectedRecipe.cookingTime.ToString() + " минут" ;
        t_textGuide.text = LoadData.SelectedRecipe.guide;
        t_textGuideFullScreen.text = LoadData.SelectedRecipe.guide;       
        i_recipeImage.sprite = Resources.Load<Sprite>(LoadData.SelectedRecipe.imagePath);

        s_onFavorite = Resources.Load<Sprite>("Images/heartRED(1)");
        s_offFavorite = Resources.Load<Sprite>("Images/heartBLACK(1)");

        if (LoadData.favoriteResipes.Any(r => r.id == LoadData.SelectedRecipe.id))
        {
            i_favoriteButtonImage.sprite = s_onFavorite;
        }
        else
        {
            i_favoriteButtonImage.sprite = s_offFavorite;
        }

        foreach (Transform child in rt_ingredientsContent.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in LoadData.SelectedRecipe.ingredients)
        {
            GameObject recipe = Instantiate(g_text, rt_ingredientsContent);
            recipe.GetComponent<Text>().text = item.ingredient.ingredientName + " - " + item.weight.ToString() +" кг.";
        }
        b_btnBack.onClick.AddListener(() => SceneChanger.LoadDisplay(FoundedRecipes.BackDisplayForRecipe));
    }
}
