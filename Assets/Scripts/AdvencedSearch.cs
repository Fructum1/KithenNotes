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
    private GameObject _ingredient;
    RectTransform _ingredientsDesired;
    RectTransform _ingredientsUnDesired;
    RectTransform _scrollViewDesired;
    RectTransform _scrollViewUnDesired;
    Dropdown _dropdownRecipeType;
    Dropdown _dropddownRecipeNational;
    Text _desiredList;
    Text _unDesiredList;
    Text _textSearchUnDesired;
    Text _textSearchDesired;
    List<Button> _listOfIngredientsDesired;
    List<Button> _listOfIngredientsUndesired;
    List<string> _dropdownRecipeTypeOptions => Enum.GetValues(typeof(FoodTypes)).Cast<FoodTypes>()
                                                                                 .Select(v => v.ToString()).ToList();
    List<string> _dropdownRecipeNational => Enum.GetValues(typeof(NationalTypes)).Cast<NationalTypes>()
                                                                                  .Select(v => v.ToString()).ToList();
    bool _dropdownUnDesiredToggled = false;
    bool _dropdownDesiredToggled = false;

    private void Start()
    {
        LoadData.SelectedDesiredIngredients.Clear();
        LoadData.SelectedUndesiredIngredients.Clear();

        _scrollViewDesired = GameObject.Find("ScrollViewDesired").GetComponent<RectTransform>();
        _scrollViewUnDesired = GameObject.Find("ScrollViewUnDesired").GetComponent<RectTransform>();
        _ingredientsDesired = GameObject.Find("IngredientsDesired").GetComponent<RectTransform>();
        _ingredientsUnDesired = GameObject.Find("IngredientsUnDesired").GetComponent<RectTransform>();
        _dropdownRecipeType = GameObject.Find("DropdownType").GetComponent<Dropdown>();
        _dropddownRecipeNational = GameObject.Find("DropdownNational").GetComponent<Dropdown>();
        _desiredList = GameObject.Find("DesiredList").GetComponent<Text>();
        _textSearchDesired = GameObject.Find("TextSearchDesired").GetComponent<Text>();
        _unDesiredList = GameObject.Find("UnDesiredList").GetComponent<Text>();
        _textSearchUnDesired = GameObject.Find("TextSearchUnDesired").GetComponent<Text>();

        foreach (var item in LoadData.Ingredients)
        {
            GameObject desired = Instantiate(_ingredient, _ingredientsDesired);

            desired.GetComponentInChildren<Text>().text = item;
            desired.GetComponent<Button>().onClick.AddListener(() => OnIngredientClick(item, true));

            GameObject unDesired = Instantiate(_ingredient, _ingredientsUnDesired);

            unDesired.GetComponentInChildren<Text>().text = item;
            unDesired.GetComponent<Button>().onClick.AddListener(() => OnIngredientClick(item, false));
        }

        _listOfIngredientsDesired = _ingredientsDesired.GetComponentsInChildren<Button>().ToList<Button>();
        _listOfIngredientsUndesired = _ingredientsUnDesired.GetComponentsInChildren<Button>().ToList<Button>();
        _dropdownRecipeType.ClearOptions();
        _dropdownRecipeType.AddOptions(_dropdownRecipeTypeOptions);
        _dropddownRecipeNational.ClearOptions();
        _dropddownRecipeNational.AddOptions(_dropdownRecipeNational);

        _scrollViewUnDesired.gameObject.SetActive(false);
        _scrollViewDesired.gameObject.SetActive(false);

        LoadData.PreviousScene = SceneManager.GetActiveScene().name;
    }

    public void HideOnInput(bool desired)
    {
        if (desired)
        {
            _desiredList.gameObject.SetActive(false);
            _textSearchDesired.gameObject.SetActive(true);
        }
        else
        {
            _unDesiredList.gameObject.SetActive(false);
            _textSearchUnDesired.gameObject.SetActive(true);
        }
    }

    public void ShowOnEndInput(bool desired)
    {
        if (desired)
        {
            _desiredList.gameObject.SetActive(true);
            _textSearchDesired.gameObject.SetActive(false);
        }
        else
        {
            _unDesiredList.gameObject.SetActive(true);
            _textSearchUnDesired.gameObject.SetActive(false);
        }
    }

    public void ToggleDropdownMenuDesired()
    {   
        if (_dropdownDesiredToggled)
        {
            _scrollViewDesired.gameObject.SetActive(false);
            _dropdownDesiredToggled = !_dropdownDesiredToggled;
        }
        else
        {
            _scrollViewDesired.gameObject.SetActive(true);
            _dropdownDesiredToggled = !_dropdownDesiredToggled;
        }
    }
    public void ToggleDropdownMenuUnDesired()
    {    
        if (_dropdownUnDesiredToggled)
        {
            _scrollViewUnDesired.gameObject.SetActive(false);
            _dropdownUnDesiredToggled = !_dropdownUnDesiredToggled;
        }
        else
        {
            _scrollViewUnDesired.gameObject.SetActive(true);
            _dropdownUnDesiredToggled = !_dropdownUnDesiredToggled;
        }
    }
    public void LiveSearchUnDesired(string searchedItem)
    {
        _scrollViewUnDesired.gameObject.SetActive(true);
        _scrollViewDesired.gameObject.SetActive(false);
        _dropdownUnDesiredToggled = true;
        if (!searchedItem.Equals(""))
        {
            var data = _listOfIngredientsUndesired.Where(i => i.GetComponentInChildren<Text>().text.Split(' ')
                                                  .All(i => !i.StartsWith(searchedItem, true, CultureInfo.CurrentCulture)));
            foreach (var item in data)
            {
                item.gameObject.SetActive(false);
            }

            if (data.Count() == _listOfIngredientsUndesired.Count())
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
        _scrollViewDesired.gameObject.SetActive(true);
        _scrollViewUnDesired.gameObject.SetActive(false);
        _dropdownDesiredToggled = true;
        if (!searchedItem.Equals(""))
        {
            var data = _listOfIngredientsDesired.Where(i => i.GetComponentInChildren<Text>().text.Split(' ')
                                                .All(i => !i.StartsWith(searchedItem, true, CultureInfo.CurrentCulture)));
            foreach (var item in data)
            {
                item.gameObject.SetActive(false);
            }

            if (data.Count() == _listOfIngredientsDesired.Count())
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
        LoadData.SelectedRecipeType = _dropdownRecipeType.options[_dropdownRecipeType.value].text;
        LoadData.SelectedRecipeNational = _dropddownRecipeNational.options[_dropddownRecipeNational.value].text;
    }

    public void ClearListDesired()
    {
        LoadData.SelectedDesiredIngredients.Clear();
        _desiredList.text = "";
    }
    public void ClearListUnDesired()
    {
        LoadData.SelectedUndesiredIngredients.Clear();
        _unDesiredList.text = "";
    }

    void ShowAllItemsInDropdownMenuDesired()
    {
        foreach (var item in _listOfIngredientsDesired)
        {
            item.gameObject.SetActive(true);
        }
    }
    void ShowAllItemsInDropdownMenuUnDesired()
    {
        foreach (var item in _listOfIngredientsUndesired)
        {
            item.gameObject.SetActive(true);
        }
    }

    void OnIngredientClick(string ingredientName, bool desired)
    {
        if (desired)
        {
            if (!LoadData.SelectedDesiredIngredients.Contains(ingredientName))
            {
                LoadData.SelectedDesiredIngredients.Add(ingredientName);               
                _desiredList.text = String.Join(", ", LoadData.SelectedDesiredIngredients);
                _textSearchDesired.text = "";
                ShowAllItemsInDropdownMenuDesired();
            }
            else
            {
                LoadData.SelectedDesiredIngredients.Remove(ingredientName);
                _desiredList.text = String.Join(", ", LoadData.SelectedDesiredIngredients);
                _textSearchDesired.text = "";
                ShowAllItemsInDropdownMenuDesired();
            }
        }
        else
        {
            if (!LoadData.SelectedUndesiredIngredients.Contains(ingredientName))
            {
                LoadData.SelectedUndesiredIngredients.Add(ingredientName);
                _unDesiredList.text = String.Join(", ", LoadData.SelectedUndesiredIngredients);
                _textSearchUnDesired.text = "";
                ShowAllItemsInDropdownMenuUnDesired();
            }
            else
            {
                LoadData.SelectedUndesiredIngredients.Remove(ingredientName);
                _unDesiredList.text = String.Join(", ", LoadData.SelectedUndesiredIngredients);
                _textSearchUnDesired.text = "";
                ShowAllItemsInDropdownMenuUnDesired();
            }
        }
    }
}
//TODO: Сделать обобщенные методы для работы с любыми компонентами