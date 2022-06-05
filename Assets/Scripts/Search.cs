using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Search : MonoBehaviour
{
    public static string nameSearch;

    private Text t_searchedName;

    void Awake() 
    { 
        t_searchedName = GameObject.Find("SearchedName").GetComponent<Text>();
    }

    public void OnSubmit()
    {
        nameSearch = t_searchedName.text;
        t_searchedName.text = String.Empty;
    }
}
