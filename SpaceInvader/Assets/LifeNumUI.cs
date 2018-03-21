using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeNumUI : MonoBehaviour {
    PlayerLife playerLifeObj;
    GUIText lifeNumText;
    
    // Use this for initialization
    void Start () {
        GameObject g = GameObject.Find("PlayerLife");
        playerLifeObj = g.GetComponent<PlayerLife>();
        lifeNumText = gameObject.GetComponent<GUIText>();
    }
	
	// Update is called once per frame
	void Update () {
        lifeNumText.text = "Life: " + playerLifeObj.playerLifeNum.ToString();
    }
}
