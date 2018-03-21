using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScoreUI : MonoBehaviour {
    // Use this for initialization
    GUIText finalScoreText;
    void Start () {
        finalScoreText = gameObject.GetComponent<GUIText>();
    }
	
	// Update is called once per frame
	void Update () {
        GameObject obj = GameObject.Find("PlayerScore");
        PlayerScore ps= obj.GetComponent<PlayerScore>();
        finalScoreText.text = "Your Score: " + ps.score;
    }
}
