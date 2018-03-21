using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fortress : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Die()
    {
        //Destroy removes the gameObject from the scene and marks it for garbage collection
        GameObject obj = GameObject.Find("FortressController");
        FortressController fc = obj.GetComponent<FortressController>();
        fc.deleteFortressPiece(gameObject);
        Destroy(gameObject);
    }
}
