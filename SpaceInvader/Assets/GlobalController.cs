using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalController : MonoBehaviour {

    public GameObject UFO;
    private GameObject UFOObject;
    public GameObject playerShip;
    private GameObject playerShipObj;
    public GameObject russianRulletGun;
    private GameObject russianRulletGunObj;
    public GameObject alienBoss;
    private GameObject alienBossObj;
    public int scoreFactor;
    private float timer;
    private float UFOSpawnPeriod;
    //public float alienSpeed;
    //public int score;
    public Vector3 originInScreenCoords;
    private int playerLife;
    private System.Random rnd;
    private Vector3 playerStartPos;
    private Vector3 russianRulletGunPos;
    private float russianRulleTimer;
    private float russianRullePeriod;
    private float alienBossTimer;
    private float alienBossPeriod;
    private Vector3 alienBossStartPos;
    public bool bulletFreeze;

    // Use this for initialization
    void Start () {
        timer = 0;
        UFOSpawnPeriod = 20.0f;
        playerLife = GameObject.Find("PlayerLife").GetComponent<PlayerLife>().playerLifeNum;
        scoreFactor = 1;
        bulletFreeze = false;

        russianRulleTimer = 0;
        russianRullePeriod = 10.0f;

        alienBossTimer = 0;
        alienBossPeriod = 30.0f;

        //Initialize a Playership Object
        originInScreenCoords = Camera.main.WorldToScreenPoint(new Vector3(0, 0, 0));
        //playerStartPos = new Vector3(0, 0, -6);
        playerStartPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height / 12, originInScreenCoords.z));       
        playerShipObj = Instantiate(playerShip, playerStartPos,Quaternion.identity) as GameObject;
        PlayerShip p = playerShipObj.GetComponent<PlayerShip>();
        p.moveDistance = 0.05f;

        //Random Number Generator to generate UFO points
        rnd = new System.Random((int)System.DateTime.Now.Ticks & 0x0000FFFF);
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer > UFOSpawnPeriod)
        {
            timer = 0;
            UFOObject = Instantiate(UFO, Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height*0.95f, originInScreenCoords.z)), 
                Quaternion.AngleAxis(-90, Vector3.right)) as GameObject;
            UFOScript u =  UFOObject.GetComponent<UFOScript>();
            u.setUFOPoint(rnd.Next(0, 3));
        }

        russianRulleTimer += Time.deltaTime;
        if (russianRulleTimer > russianRullePeriod)
        {
            russianRulleTimer = 0;
            
            //Initialize a russianRulletGun Object
            russianRulletGunPos = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height / 5, originInScreenCoords.z - 3));
            russianRulletGunObj = Instantiate(russianRulletGun, russianRulletGunPos, Quaternion.identity) as GameObject;
            RussianRulletGun rrg = russianRulletGunObj.GetComponent<RussianRulletGun>();
            rrg.shootDirection.x = 0;
            rrg.shootDirection.y = playerStartPos.y - russianRulletGunPos.y;
            rrg.shootDirection.z = russianRulletGunPos.z - playerStartPos.z;
            //rrg.shootDirection.z = playerStartPos.z - russianRulletGunPos.z;
            rrg.transform.rotation = Quaternion.LookRotation(rrg.shootDirection);
        }

        alienBossTimer += Time.deltaTime;
        if(alienBossTimer > alienBossPeriod)
        {
            alienBossTimer = 0;

            //Initialize an alienBoss Object
            alienBossStartPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height*0.8f, originInScreenCoords.z + 1));
            //Debug.Log("alienBossStartPos is: " + alienBossStartPos);
            alienBossObj = Instantiate(alienBoss, alienBossStartPos, Quaternion.identity) as GameObject;

        }
    }

    public void speedUpPlayer()
    {
        playerShipObj.GetComponent<PlayerShip>().SpeedUp();
    }

    public void reducePlayerLife()
    {
        GameObject.Find("PlayerLife").GetComponent<PlayerLife>().playerLifeNum--;
        playerLife--;
    }

    public void checkPlayerLife()
    {
        if (playerLife > 0)
        {
            //Instantiate a new Life
            //Vector3 playerStartPos = new Vector3(0, 0, -6);
            playerShipObj = Instantiate(playerShip, playerStartPos, Quaternion.identity) as GameObject;
            PlayerShip p = playerShipObj.GetComponent<PlayerShip>();
            p.moveDistance = 0.05f;
        }else{
            SceneManager.LoadScene("GameOver");
        }
    }

    public int getPlayerLife()
    {
        return playerLife;
    }
}
