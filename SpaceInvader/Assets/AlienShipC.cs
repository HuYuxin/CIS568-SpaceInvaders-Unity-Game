using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlienShipC : MonoBehaviour {

    // Use this for initialization
    private int pointValue;
    //private float widthBoundaryRight;
    //private float widthBoundaryLeft;
    //private float heightBoundaryUp;
    //private float heightBoundaryDown;
    //private Vector3 cameraViewPos;
    public int colNum; //0-10,used to track the boundary condition for different alien ships
                       // Use this for initialization
    void Start()
    {
        //widthBoundaryRight = Screen.width - 10;
        //widthBoundaryLeft = 0 + 10;
        //heightBoundaryUp = Screen.height - 30;
        //heightBoundaryDown = 0 + 10;
    }

    public void setPointValue(int pointVal)
    {
        pointValue = pointVal;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void MoveAlienShipC(Vector3 movement)
    {
        Vector3 updatedPos = gameObject.transform.position;
        updatedPos = updatedPos + movement;
        gameObject.transform.position = updatedPos;
    }

    public GameObject deathExplosion;
    public AudioClip explodeSound;
    public void Die()
    {
        //Destroy removes the gameObject from the scene and marks it for garbage collection
        AudioSource.PlayClipAtPoint(explodeSound, gameObject.transform.position);
        Instantiate(deathExplosion, gameObject.transform.position,
            Quaternion.identity);

        GameObject obj = GameObject.Find("GlobalController");
        GlobalController g = obj.GetComponent<GlobalController>();
        GameObject.Find("PlayerScore").GetComponent<PlayerScore>().score += g.scoreFactor*pointValue;
        g.speedUpPlayer();       

        GameObject alienShipObj = GameObject.Find("AlienShipController");
        AlienShipController ac = alienShipObj.GetComponent<AlienShipController>();
        ac.deleteAlienShip(gameObject);

        Destroy(gameObject);
    }

    public GameObject alienBullet;
    public void fire()
    {
        Vector3 spawnPos = gameObject.transform.position;
        spawnPos.z -= 0.5f;
        GameObject obj = Instantiate(alienBullet, spawnPos,
            Quaternion.identity) as GameObject;
        AlienShipBullet b = obj.GetComponent<AlienShipBullet>();
        //set the direction the Bullet will travel in
        Quaternion rot = Quaternion.Euler(new Vector3(0, 0, 0));
        b.heading = rot;
    }

    //Alien hits fortress, game over
    void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;
        if (collider.CompareTag("Fortress"))
        {
            Fortress fortress = collider.gameObject.GetComponent<Fortress>();
            fortress.Die();
            Destroy(gameObject);
            SceneManager.LoadScene("GameOver");
        }
    }
}
