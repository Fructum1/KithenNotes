using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class AdvencedSearch : MonoBehaviour
{
    AutoCompleteComboBox DesiredIngredients;
    AutoCompleteComboBox UndesiredIngredients;
    Button ArrowDownDesired;
    Button ArrowDownUndesired;
    Text desiredIngredientText;
    Text undesiredIngredientText;
    Text TextSearchDesired;
    Text TextSearchUndesired;
    Dropdown DropdownRecipeType;
    Dropdown DropddownRecipeNational;
    public static string RecipeType;
    public static string RecipeNational;
    public static List<string> selectedDesiredIngredients = new List<string>();
    public static List<string> selectedUndesiredIngredients = new List<string> ();
    List<string> m_DropdownRecipeTypeOptions => Enum.GetValues(typeof(FoodTypes)).Cast<FoodTypes>().Select(v => v.ToString()).ToList();
    List<string> m_DropdownRecipeNational => Enum.GetValues(typeof(NationalTypes)).Cast<NationalTypes>().Select(v => v.ToString()).ToList();
    List<string> m_Ingredients = LoadData.Ingredients;

    void Start()
    {
        FindAllComponents();
        ClearAllDropdownOptions();

        DesiredIngredients.SetAvailableOptions(m_Ingredients);
        UndesiredIngredients.SetAvailableOptions(m_Ingredients);
        DropdownRecipeType.AddOptions(m_DropdownRecipeTypeOptions);

        DropddownRecipeNational.ClearOptions();
        DropddownRecipeNational.AddOptions(m_DropdownRecipeNational);
    }

    public void OnSearchSubmit()
    {
        RecipeType = DropdownRecipeType.options[DropdownRecipeType.value].text;
        RecipeNational = DropddownRecipeNational.options[DropddownRecipeNational.value].text;
    }

    public void OnSelectDesired(string selectedItem, bool changed)
    {
        if (changed)
        {
            if (!selectedDesiredIngredients.Contains(selectedItem) && !selectedUndesiredIngredients.Contains(selectedItem))
            {
                selectedItem = char.ToUpper(selectedItem[0]) + selectedItem.Substring(1);

                DesiredIngredients.RemoveItem(selectedItem);
                UndesiredIngredients.RemoveItem(selectedItem);
                selectedDesiredIngredients.Add(selectedItem);
                desiredIngredientText.text += selectedItem + ", ";
                TextSearchDesired.text = "";
                ArrowDownDesired.onClick.Invoke();
                GetComponent<Button>().onClick.Invoke();
            }
        }
    }

    public void OnSelectUndesired(string selectedItem, bool changed)
    {
        if (changed)
        {
            if (!selectedUndesiredIngredients.Contains(selectedItem) && !selectedDesiredIngredients.Contains(selectedItem))
            {
                selectedItem = char.ToUpper(selectedItem[0]) + selectedItem.Substring(1);

                DesiredIngredients.RemoveItem(selectedItem);
                UndesiredIngredients.RemoveItem(selectedItem);
                selectedUndesiredIngredients.Add(selectedItem);
                undesiredIngredientText.text += selectedItem + ", ";
                TextSearchUndesired.text = "";
                ArrowDownUndesired.onClick.Invoke();
                GetComponent<Button>().onClick.Invoke();
            }
        }
    }

    public void HideSelectedUndesired()
    {
        TextSearchUndesired.gameObject.SetActive(true);
        undesiredIngredientText.gameObject.SetActive(false);
    }

    public void HideSelectedDesired()
    {
        TextSearchDesired.gameObject.SetActive(true);
        desiredIngredientText.gameObject.SetActive(false);
    }

    public void ShowTextOnEditEndDesired()
    {
        desiredIngredientText.gameObject.SetActive(true);
        TextSearchDesired.text = "";
        TextSearchDesired.gameObject.SetActive(false);
    }

    public void ShowTextOnEditEndUndesired()
    {
        undesiredIngredientText.gameObject.SetActive(true);
        TextSearchUndesired.text = "";
        TextSearchUndesired.gameObject.SetActive(false);
    }

    private void FindAllComponents()
    {
        undesiredIngredientText = GameObject.Find("UndesiredText").GetComponent<Text>();
        desiredIngredientText = GameObject.Find("DesiredText").GetComponent<Text>();
        undesiredIngredientText = GameObject.Find("UndesiredText").GetComponent<Text>();
        TextSearchDesired = GameObject.Find("TextSearchDesired").GetComponent<Text>();
        TextSearchUndesired = GameObject.Find("TextSearchUndesired").GetComponent<Text>();
        ArrowDownDesired = GameObject.Find("ArrowBtnDesired").GetComponent<Button>();
        ArrowDownUndesired = GameObject.Find("ArrowBtUndesired").GetComponent<Button>();
        DesiredIngredients = GameObject.Find("DesiredIngredients").GetComponent<AutoCompleteComboBox>();
        UndesiredIngredients = GameObject.Find("UndesiredIngredients").GetComponent<AutoCompleteComboBox>();
        DropdownRecipeType = GameObject.Find("DropdownType").GetComponent<Dropdown>();
        DropddownRecipeNational = GameObject.Find("DropdownNational").GetComponent<Dropdown>();
    }

    private void ClearAllDropdownOptions()
    {
        DesiredIngredients.AvailableOptions.Clear();
        selectedDesiredIngredients.Clear();
        selectedUndesiredIngredients.Clear();
        UndesiredIngredients.AvailableOptions.Clear();
        DropdownRecipeType.ClearOptions();
    }
}

//TODO: Сделать обобщенные методы для работы с любыми компонентами