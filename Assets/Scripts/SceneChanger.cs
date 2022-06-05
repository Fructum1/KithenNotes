using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static void LoadDisplay(RectTransform display)
    {
        MainScreen.ActiveDisplay.gameObject.SetActive(false);
        display.gameObject.SetActive(true);
        MainScreen.ActiveDisplay = display;
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
