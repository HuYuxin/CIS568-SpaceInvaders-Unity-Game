using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerShip : MonoBehaviour {
    public float moveDistance;
    public bool freezeBulletFlag;
    public bool doubleScoreFlag;
    private float freezeBulTimer;
    private float freezeBulPeriod;
    private float doubleScoreTimer;
    private float doubleScorePeriod;
	// Use this for initialization
	void Start () {
        //moveDistance = 0.05f;
        GameObject obj = GameObject.Find("GlobalController");
        GlobalController g = obj.GetComponent<GlobalController>();
        freezeBulletFlag = g.bulletFreeze;
        doubleScoreFlag = g.scoreFactor > 1 ? true : false;
        freezeBulTimer = 0.0f;
        freezeBulPeriod = 10.0f;
        doubleScoreTimer = 0.0f;
        doubleScorePeriod = 15.0f;
    }

    public GameObject playerBullet;
    public AudioClip shootSound;
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Jump"))
        {
            if (!freezeBulletFlag)
            {
                AudioSource.PlayClipAtPoint(shootSound, gameObject.transform.position);
                Vector3 spawnPos = gameObject.transform.position;
                spawnPos.z += 0.5f;
                GameObject obj = Instantiate(playerBullet, spawnPos,
                        Quaternion.identity) as GameObject;
                PlayerShipBullet b = obj.GetComponent<PlayerShipBullet>();
                //set the direction the Bullet will travel in
                Quaternion rot = Quaternion.Euler(new Vector3(0, 0, 0));
                b.heading = rot;
            }            
        }

        if (freezeBulletFlag)
        {
            freezeBulTimer += Time.deltaTime;
            if (freezeBulTimer > freezeBulPeriod)
            {
                freezeBulTimer = 0;
                freezeBulletFlag = false;
                GameObject obj = GameObject.Find("GlobalController");
                GlobalController g = obj.GetComponent<GlobalController>();
                g.bulletFreeze = false;
            }
        }

        if (doubleScoreFlag)
        {
            doubleScoreTimer += Time.deltaTime;
            if(doubleScoreTimer > doubleScorePeriod)
            {
                doubleScoreTimer = 0;
                doubleScoreFlag = false;
                GameObject obj = GameObject.Find("GlobalController");
                GlobalController g = obj.GetComponent<GlobalController>();
                g.scoreFactor = 1;
            }
        }
	}

    //FixedUpdate() is called fixed times per second
    void FixedUpdate()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            if(Camera.main.WorldToScreenPoint(gameObject.transform.position).x < Screen.width - 20)
            {
                gameObject.transform.Translate(moveDistance, 0, 0);
            }
            
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            if (Camera.main.WorldToScreenPoint(gameObject.transform.position).x > 20)
            {
                gameObject.transform.Translate(-moveDistance, 0, 0);
            }
        }
    }

    public void SpeedUp()
    {
        moveDistance += 0.001f;
    }

    public GameObject deathExplosion;
    public AudioClip explodeSound;
    public void Die()
    {
        
        AudioSource.PlayClipAtPoint(explodeSound, gameObject.transform.position,1.0f);
        Instantiate(deathExplosion, gameObject.transform.position,
           Quaternion.identity);
        Destroy(gameObject);
        
        //Test Camera Shake
        CameraShake cs = Camera.main.GetComponent<CameraShake>();
        cs.shakeDuration = 0.5f;

        //Check GlobalController how many lives left
        //If more than 0 left, destroy the current object and instantiate a new one
        //If it equals 0, then detroy the current object and game over.
        GameObject obj = GameObject.Find("GlobalController");
        GlobalController g = obj.GetComponent<GlobalController>();
        g.reducePlayerLife();
        g.checkPlayerLife();
    }

    public GameObject freezeExplosion;
    public AudioClip freezeSound;
    public void freezeBullet()
    {
        AudioSource.PlayClipAtPoint(freezeSound, gameObject.transform.position, 1.0f);
        Instantiate(freezeExplosion, gameObject.transform.position,
           Quaternion.identity);
        freezeBulletFlag = true;
        GameObject obj = GameObject.Find("GlobalController");
        GlobalController g = obj.GetComponent<GlobalController>();
        g.bulletFreeze = true;
    }

    public GameObject bonusExplosion;
    public AudioClip bonusSound;
    public void doublePoint()
    {
        AudioSource.PlayClipAtPoint(bonusSound, gameObject.transform.position, 5.0f);
        Instantiate(bonusExplosion, gameObject.transform.position,
           Quaternion.identity);
        doubleScoreFlag = true;
        GameObject obj = GameObject.Find("GlobalController");
        GlobalController g = obj.GetComponent<GlobalController>();
        g.scoreFactor = 2;
    }
}
