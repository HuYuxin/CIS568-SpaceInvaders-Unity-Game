using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusScoreUI : MonoBehaviour {
    GlobalController globalObj;
    GUIText bonusText;
    // Use this for initialization
    void Start () {
        GameObject g = GameObject.Find("GlobalController");
        globalObj = g.GetComponent<GlobalController>();
        bonusText = gameObject.GetComponent<GUIText>();
    }
	
	// Update is called once per frame
	void Update () {
        if (globalObj.scoreFactor>1)
        {
            bonusText.text = "Score Value Doubled";
        }else
        {
            bonusText.text = "";
        }
	}
}
