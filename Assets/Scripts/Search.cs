using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Search : MonoBehaviour
{
    Button _buttonSearch;
    Text _searchedName;
    public static string nameSearch;

    void Start() 
    { 
        _searchedName = GameObject.Find("SearchedName").GetComponent<Text>();
        _buttonSearch = GameObject.Find("SubmitSearch").GetComponent<Button>();
        _buttonSearch.onClick.AddListener(() => nameSearch = _searchedName.text);

        LoadData.PreviousScene = SceneManager.GetActiveScene().name;
    }
}
