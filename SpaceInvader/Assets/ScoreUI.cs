using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUI : MonoBehaviour {
    PlayerScore scoreObj;
    GUIText scoreText;
    private int playerScore;

	// Use this for initialization
	void Start () {
        GameObject g = GameObject.Find("PlayerScore");
        scoreObj = g.GetComponent<PlayerScore>();
        scoreText = gameObject.GetComponent<GUIText>();
        playerScore = scoreObj.score;

    }
	
	// Update is called once per frame
	void Update () {
        playerScore = scoreObj.score;
        scoreText.text = "Score: "+ playerScore.ToString();
	}
}
