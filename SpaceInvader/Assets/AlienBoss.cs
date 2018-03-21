using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBoss : MonoBehaviour {
    public float movement;
    private float yLimit;
    private float bossTimer;
    private float bossStayTime;
    private bool bossStay;
    private bool bossBack;
	// Use this for initialization
	void Start () {
        movement = 0.005f;
        Vector3 originInScreenCoords = Camera.main.WorldToScreenPoint(new Vector3(0, 0, 0));
        yLimit = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, originInScreenCoords.z)).y;
        //Debug.Log("alienBoss yLimit is: " + yLimit);
        bossStayTime = 10.0f;
        bossTimer = 0.0f;
        bossStay = false;
        bossBack = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (bossStay)
        {
            bossTimer += Time.deltaTime;
            if (bossTimer > bossStayTime)
            {
                bossTimer = 0;
                bossStay = false;
                bossBack = true;
            }
        }else
        {
            if (bossBack)
            {
                gameObject.transform.Translate(0, -movement, 0);
                if(gameObject.transform.position.y < -2)
                {
                    Destroy(gameObject);
                }
            }else
            {
                if(gameObject.transform.position.y < yLimit)
                {
                    gameObject.transform.Translate(0, movement, 0);
                }else
                {
                    bossStay = true;
                }
                
            }
        }
    }

    public GameObject deathExplosion;
    public AudioClip explodeSound;
    public void Die()
    {
        AudioSource.PlayClipAtPoint(explodeSound, gameObject.transform.position);
        Instantiate(deathExplosion, gameObject.transform.position,
            Quaternion.identity);

        CameraShake cs = Camera.main.GetComponent<CameraShake>();
        cs.shakeDuration = 0.5f;

        //Kill a group of alienShips
        GameObject obj = GameObject.Find("AlienShipController");
        AlienShipController asc = obj.GetComponent<AlienShipController>();
        asc.bossKilled();

        Destroy(gameObject);
    }
}
