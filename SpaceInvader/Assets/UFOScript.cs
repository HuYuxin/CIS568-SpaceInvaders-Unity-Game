using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOScript : MonoBehaviour {
    public float movement;
    private int point;
    private static int[] pointArray = { 50, 100, 150,300};
    private List<int> pointPool = new List<int>(pointArray);
    //private float maxDisplacement;

    // Use this for initialization
    void Start () {
        movement = 0.06f;
        //maxDisplacement = Screen.height;
        //Debug.Log("UFO Left Boundary is: "+ Camera.main.WorldToScreenPoint(gameObject.transform.position).x);
    }
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.Translate(-movement, 0, 0);
        if(Camera.main.WorldToScreenPoint(gameObject.transform.position).x < -10)
        {           
            Destroy(gameObject);
        }
    }

    public void setUFOPoint(int index)
    {
        point = pointPool[index];
    }

    public GameObject deathExplosion;
    public AudioClip explodeSound;
    public void Die()
    {
        //Destroy removes the gameObject from the scene and marks it for garbage collection
        AudioSource.PlayClipAtPoint(explodeSound, gameObject.transform.position,5);
        Instantiate(deathExplosion, gameObject.transform.position,
            Quaternion.identity);

        CameraShake cs = Camera.main.GetComponent<CameraShake>();
        cs.shakeDuration = 0.5f;

        GameObject obj = GameObject.Find("GlobalController");
        GlobalController g = obj.GetComponent<GlobalController>();
        GameObject.Find("PlayerScore").GetComponent<PlayerScore>().score += point;
        g.speedUpPlayer();

        Destroy(gameObject);
    }
}
