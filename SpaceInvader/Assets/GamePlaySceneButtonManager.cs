using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlaySceneButtonManager : MonoBehaviour {
    public void exitButton(string newGameLevel)
    {
        SceneManager.LoadScene(newGameLevel);
    }
}
