using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeLoad : MonoBehaviour
{
    private Text t_RecipeName;
    private Text t_RecipeDescription;
    private Text t_RecipeCookingTime;
    private RectTransform ingredientsContent;
    private RectTransform guideContent;

    void Start()
    {
        ingredientsContent = GameObject.Find("Ingredients").GetComponent<RectTransform>();
        guideContent = GameObject.Find("Guide").GetComponent<RectTransform>();
        t_RecipeCookingTime = GameObject.Find("DropdownType").GetComponent<Text>();
        t_RecipeDescription = GameObject.Find("DropdownType").GetComponent<Text>();
        t_RecipeName = GameObject.Find("DropdownType").GetComponent<Text>();
    }

}
