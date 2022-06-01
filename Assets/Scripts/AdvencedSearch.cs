using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AdvencedSearch : MonoBehaviour
{
    Dropdown m_DropdownRecipeType;
    Dropdown m_DropddownRecipeNational;
    List<string> m_DropdownRecipeTypeOptions => Enum.GetValues(typeof(FoodTypes)).Cast<FoodTypes>().Select(v => v.ToString()).ToList();
    List<string> m_DropdownRecipeNational => Enum.GetValues(typeof(NationalTypes)).Cast<NationalTypes>().Select(v => v.ToString()).ToList();
    // Start is called before the first frame update
    void Start()
    {
        m_DropdownRecipeType = GameObject.Find("DropdownType").GetComponent<Dropdown>();
        m_DropdownRecipeType.ClearOptions();
        m_DropdownRecipeType.AddOptions(m_DropdownRecipeTypeOptions);

        m_DropddownRecipeNational = GameObject.Find("DropdownNational").GetComponent<Dropdown>();
        m_DropddownRecipeNational.ClearOptions();
        m_DropddownRecipeNational.AddOptions(m_DropdownRecipeNational);
    }

}
