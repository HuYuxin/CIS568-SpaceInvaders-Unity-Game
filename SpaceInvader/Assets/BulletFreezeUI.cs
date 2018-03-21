using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFreezeUI : MonoBehaviour {
    GlobalController globalObj;
    GUIText warningText;
    // Use this for initialization
    void Start () {
        GameObject g = GameObject.Find("GlobalController");
        globalObj = g.GetComponent<GlobalController>();
        warningText = gameObject.GetComponent<GUIText>();
    }
	
	// Update is called once per frame
	void Update () {
        if (globalObj.bulletFreeze)
        {
            warningText.text = "Bullet Freezes";
        }
        else
        {
            warningText.text = "";
        }
    }
}
