using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour {

    public static UiController instance;
    public GameObject mainMenu;

    private void Awake()
    {
        instance = this;
    }

    public void PlayButtonPressed()
    {
        mainMenu.SetActive(false);    

    }
}
