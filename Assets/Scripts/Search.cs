using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Search : MonoBehaviour
{
    Button m_buttonSearch;
    Text t_searchedName;
    public static string nameSearch;

    void Start() 
    { 
        t_searchedName = GameObject.Find("SearchedName").GetComponent<Text>();
        m_buttonSearch = GameObject.Find("SubmitSearch").GetComponent<Button>();
        m_buttonSearch.onClick.AddListener(() => nameSearch = t_searchedName.text);
    }
}
