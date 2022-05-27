using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

enum FoodTypes
{
    soup,
    salad,
    baking,
    pita,
    hotMeal,
    coldMeal,
    dessert,
    dairyDishes,
    sauses,
    porridge,
    drinks,
    snacks,
    seafood,
}

enum NationalTypes
{
    Russian,
    Italian,
    Japanese,
    European,
    Asian,
    PanAsian,
    Georgian,
    Mexican,
    Exotic,
}

[CreateAssetMenu(fileName = "New Recipe", menuName = "Recipe Data", order = 51)]
[System.Serializable]
public class ScriptableRecipes : ScriptableObject
{
    private static int id = 0;
    [SerializeField]
    private string recipeName;
    [SerializeField]
    private NationalTypes national;
    [SerializeField]
    private FoodTypes type;
    [SerializeField]
    private string description;
    [SerializeField]
    [Multiline]
    private string guide;
    [SerializeField]
    private string imagePath;
    [SerializeField]
    private int cookingTime;
    [SerializeField]
    private List<IngredientData> ingredients;

    [System.Serializable]
    public class IngredientData
    {
        private static int id = 0;
        [SerializeField]
        private string ingredientName;
        [SerializeField]
        private float weight;

        public IngredientData()
        {
            id++;
        }
    }

    public ScriptableRecipes()
    {
        id++;
    }
}
