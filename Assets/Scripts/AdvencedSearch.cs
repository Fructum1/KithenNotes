using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class AdvencedSearch : MonoBehaviour
{
    [SerializeField]
    private GameObject g_ingredient;
    private RectTransform rt_ingredientsDesired;
    private RectTransform rt_ingredientsUnDesired;
    private RectTransform rt_scrollViewDesired;
    private RectTransform rt_scrollViewUnDesired;
    private Dropdown d_dropdownRecipeType;
    private Dropdown d_dropddownRecipeNational;
    private Text t_desiredList;
    private Text t_unDesiredList;
    private Text t_textSearchUnDesired;
    private Text t_textSearchDesired;
    private List<Button> b_listOfIngredientsDesired;
    private List<Button> b_listOfIngredientsUndesired;
    private List<string> _dropdownRecipeTypeOptions => Enum.GetValues(typeof(FoodTypes)).Cast<FoodTypes>()
                                                                                 .Select(v => v.ToString()).ToList();
    private List<string> _dropdownRecipeNational => Enum.GetValues(typeof(NationalTypes)).Cast<NationalTypes>()
                                                                                 .Select(v => v.ToString()).ToList();
    private bool _dropdownUnDesiredToggled = false;
    private bool _dropdownDesiredToggled = false;

    public void HideOnInput(bool desired)
    {
        if (desired)
        {
            t_desiredList.gameObject.SetActive(false);
            t_textSearchDesired.gameObject.SetActive(true);
        }
        else
        {
            t_unDesiredList.gameObject.SetActive(false);
            t_textSearchUnDesired.gameObject.SetActive(true);
        }
    }

    public void ShowOnEndInput(bool desired)
    {
        if (desired)
        {
            t_desiredList.gameObject.SetActive(true);
            t_textSearchDesired.gameObject.SetActive(false);
        }
        else
        {
            t_unDesiredList.gameObject.SetActive(true);
            t_textSearchUnDesired.gameObject.SetActive(false);
        }
    }

    public void ToggleDropdownMenuDesired()
    {   
        if (_dropdownDesiredToggled)
        {
            rt_scrollViewDesired.gameObject.SetActive(false);
            _dropdownDesiredToggled = !_dropdownDesiredToggled;
        }
        else
        {
            rt_scrollViewDesired.gameObject.SetActive(true);
            _dropdownDesiredToggled = !_dropdownDesiredToggled;
        }
    }
    public void ToggleDropdownMenuUnDesired()
    {    
        if (_dropdownUnDesiredToggled)
        {
            rt_scrollViewUnDesired.gameObject.SetActive(false);
            _dropdownUnDesiredToggled = !_dropdownUnDesiredToggled;
        }
        else
        {
            rt_scrollViewUnDesired.gameObject.SetActive(true);
            _dropdownUnDesiredToggled = !_dropdownUnDesiredToggled;
        }
    }
    public void LiveSearchUnDesired(string searchedItem)
    {
        rt_scrollViewUnDesired.gameObject.SetActive(true);
        rt_scrollViewDesired.gameObject.SetActive(false);
        _dropdownUnDesiredToggled = true;
        if (!searchedItem.Equals(""))
        {
            var data = b_listOfIngredientsUndesired.Where(i => i.GetComponentInChildren<Text>().text.Split(' ')
                                                  .All(i => !i.StartsWith(searchedItem, true, CultureInfo.CurrentCulture)));
            foreach (var item in data)
            {
                item.gameObject.SetActive(false);
            }

            if (data.Count() == b_listOfIngredientsUndesired.Count())
            {
                ShowAllItemsInDropdownMenuUnDesired();
            }
        }
        else
        {
            ShowAllItemsInDropdownMenuUnDesired();
        }

    }

    public void LiveSearchDesired(string searchedItem)
    {
        rt_scrollViewDesired.gameObject.SetActive(true);
        rt_scrollViewUnDesired.gameObject.SetActive(false);
        _dropdownDesiredToggled = true;
        if (!searchedItem.Equals(""))
        {
            var data = b_listOfIngredientsDesired.Where(i => i.GetComponentInChildren<Text>().text.Split(' ')
                                                .All(i => !i.StartsWith(searchedItem, true, CultureInfo.CurrentCulture)));
            foreach (var item in data)
            {
                item.gameObject.SetActive(false);
            }

            if (data.Count() == b_listOfIngredientsDesired.Count())
            {
                ShowAllItemsInDropdownMenuDesired();
            }
        }
        else
        {
            ShowAllItemsInDropdownMenuDesired();
        }
    }

    public void OnSearchSubmit()
    {
        LoadData.SelectedRecipeType = d_dropdownRecipeType.options[d_dropdownRecipeType.value].text;
        LoadData.SelectedRecipeNational = d_dropddownRecipeNational.options[d_dropddownRecipeNational.value].text;
        t_desiredList.text = String.Empty;
        t_unDesiredList.text = String.Empty;
        t_textSearchUnDesired.text = String.Empty;
        t_textSearchDesired.text = String.Empty;
    }

    public void ClearListDesired()
    {
        LoadData.SelectedDesiredIngredients.Clear();
        t_desiredList.text = String.Empty;
    }

    public void ClearListUnDesired()
    {
        LoadData.SelectedUndesiredIngredients.Clear();
        t_unDesiredList.text = String.Empty;
    }

    private void Awake()
    {
        rt_scrollViewDesired = GameObject.Find("ScrollViewDesired").GetComponent<RectTransform>();
        rt_scrollViewUnDesired = GameObject.Find("ScrollViewUnDesired").GetComponent<RectTransform>();
        rt_ingredientsDesired = GameObject.Find("IngredientsDesired").GetComponent<RectTransform>();
        rt_ingredientsUnDesired = GameObject.Find("IngredientsUnDesired").GetComponent<RectTransform>();
        d_dropdownRecipeType = GameObject.Find("DropdownType").GetComponent<Dropdown>();
        d_dropddownRecipeNational = GameObject.Find("DropdownNational").GetComponent<Dropdown>();
        t_desiredList = GameObject.Find("DesiredList").GetComponent<Text>();
        t_textSearchDesired = GameObject.Find("TextSearchDesired").GetComponent<Text>();
        t_unDesiredList = GameObject.Find("UnDesiredList").GetComponent<Text>();
        t_textSearchUnDesired = GameObject.Find("TextSearchUnDesired").GetComponent<Text>();

        foreach (var item in LoadData.Ingredients)
        {
            GameObject desired = Instantiate(g_ingredient, rt_ingredientsDesired);

            desired.GetComponentInChildren<Text>().text = item;
            desired.GetComponent<Button>().onClick.AddListener(() => OnIngredientClick(item, true));

            GameObject unDesired = Instantiate(g_ingredient, rt_ingredientsUnDesired);

            unDesired.GetComponentInChildren<Text>().text = item;
            unDesired.GetComponent<Button>().onClick.AddListener(() => OnIngredientClick(item, false));
        }

        b_listOfIngredientsDesired = rt_ingredientsDesired.GetComponentsInChildren<Button>().ToList<Button>();
        b_listOfIngredientsUndesired = rt_ingredientsUnDesired.GetComponentsInChildren<Button>().ToList<Button>();
        d_dropdownRecipeType.ClearOptions();
        d_dropdownRecipeType.AddOptions(_dropdownRecipeTypeOptions);
        d_dropddownRecipeNational.ClearOptions();
        d_dropddownRecipeNational.AddOptions(_dropdownRecipeNational);

        rt_scrollViewUnDesired.gameObject.SetActive(false);
        rt_scrollViewDesired.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        LoadData.SelectedDesiredIngredients.Clear();
        LoadData.SelectedUndesiredIngredients.Clear();
    }

    private void ShowAllItemsInDropdownMenuDesired()
    {
        foreach (var item in b_listOfIngredientsDesired)
        {
            item.gameObject.SetActive(true);
        }
    }
    private void ShowAllItemsInDropdownMenuUnDesired()
    {
        foreach (var item in b_listOfIngredientsUndesired)
        {
            item.gameObject.SetActive(true);
        }
    }

    private void OnIngredientClick(string ingredientName, bool desired)
    {
        if (desired)
        {
            if (!LoadData.SelectedDesiredIngredients.Contains(ingredientName))
            {
                LoadData.SelectedDesiredIngredients.Add(ingredientName);               
                t_desiredList.text = String.Join(", ", LoadData.SelectedDesiredIngredients);
                t_textSearchDesired.text = String.Empty;
                ShowAllItemsInDropdownMenuDesired();
            }
            else
            {
                LoadData.SelectedDesiredIngredients.Remove(ingredientName);
                t_desiredList.text = String.Join(", ", LoadData.SelectedDesiredIngredients);
                t_textSearchDesired.text = String.Empty;
                ShowAllItemsInDropdownMenuDesired();
            }
        }
        else
        {
            if (!LoadData.SelectedUndesiredIngredients.Contains(ingredientName))
            {
                LoadData.SelectedUndesiredIngredients.Add(ingredientName);
                t_unDesiredList.text = String.Join(", ", LoadData.SelectedUndesiredIngredients);
                t_textSearchUnDesired.text = String.Empty;
                ShowAllItemsInDropdownMenuUnDesired();
            }
            else
            {
                LoadData.SelectedUndesiredIngredients.Remove(ingredientName);
                t_unDesiredList.text = String.Join(", ", LoadData.SelectedUndesiredIngredients);
                t_textSearchUnDesired.text = String.Empty;
                ShowAllItemsInDropdownMenuUnDesired();
            }
        }
    }
}
//TODO: Сделать обобщенные методы для работы с любыми компонентами