using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour {
    public void newGameButton(string newGamePlay)
    {
        GameObject.Find("AlienStartSpeed").GetComponent<AlienStartSpeed>().alienStartSpeed = 0.8f;
        GameObject.Find("PlayerScore").GetComponent<PlayerScore>().score = 0;
        GameObject.Find("PlayerLife").GetComponent<PlayerLife>().playerLifeNum = 3;
        SceneManager.LoadScene(newGamePlay);        
    }

    public void exitGameButton()
    {
        Application.Quit();
    }
}
