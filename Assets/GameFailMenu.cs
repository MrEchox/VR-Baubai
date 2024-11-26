using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameFailMenu : MonoBehaviour
{
    [Header("UI Pages")]
    public GameObject mainMenu;


    [Header("Main Menu Buttons")]
    public Button ReturnToMenuButton;


    // Start is called before the first frame update
    void Start()
    {
        EnableMainMenu();
        ReturnToMenuButton.onClick.AddListener(ReturnToMenu);

    }


    public void ReturnToMenu()
    {
        HideAll();
        SceneTransitionManager.singleton.GoToSceneAsync(0);
    }

    public void HideAll()
    {
        mainMenu.SetActive(false);

    }

    public void EnableMainMenu()
    {
        mainMenu.SetActive(true);

    }


}
