using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWinScript : MonoBehaviour {
    public void newGameButton(string mainMenu)
    {
        SceneManager.LoadScene(mainMenu);
    }

    public void exitGameButton()
    {
        Application.Quit();
    }
}
