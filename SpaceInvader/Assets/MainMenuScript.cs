using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {
    public void newGameButton(string newGameLevel)
    {
        GameObject.Find("AlienStartSpeed").GetComponent<AlienStartSpeed>().alienStartSpeed = 0.8f;
        GameObject.Find("PlayerScore").GetComponent<PlayerScore>().score = 0;
        GameObject.Find("PlayerLife").GetComponent<PlayerLife>().playerLifeNum = 3;
        SceneManager.LoadScene(newGameLevel);
    }

    public void exitGameButton()
    {
        Application.Quit();
    }
}
