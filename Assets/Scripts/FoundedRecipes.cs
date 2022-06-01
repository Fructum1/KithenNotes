using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FoundedRecipes : MonoBehaviour
{
    [SerializeField]
    private GameObject recipe;
    private RectTransform content;
    public static Recipe selectedRecipe = null;

    void Start()
    {
        content = GameObject.Find("Content").GetComponent<RectTransform>();

        if(Search.nameSearch != null)
        {
            var data = LoadData.Recipes.Where(r => r.recipeName != null && r.recipeName.Contains(Search.nameSearch)).ToList();
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
