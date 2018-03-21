using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RussianRulletGun : MonoBehaviour {
    public Vector3 shootDirection;
    public float movement;
    private float fireTimer;
    private float firePeriod;
	// Use this for initialization
	void Start () {
        movement = 0.06f;
        fireTimer = 0;
        firePeriod = 1;
    }

    // Update is called once per frame
    void Update()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer > firePeriod)
        {
            fireTimer = 0;
            fire();
        }

        gameObject.transform.Translate(movement, 0, 0);
        if (Camera.main.WorldToScreenPoint(gameObject.transform.position).x > Screen.width+10)
        {
            Destroy(gameObject);
        }
    }

    public GameObject RussianRulletBullet;
    //public AudioClip RussianRulletShootSound;
    public void fire()
    {
        //AudioSource.PlayClipAtPoint(RussianRulletShootSound, gameObject.transform.position);
        //Debug.Log("Fire Russian Bullet!");
        Vector3 spawnPos = gameObject.transform.position;
        spawnPos.y -= 0.5f;
        GameObject obj = Instantiate(RussianRulletBullet, spawnPos,
            Quaternion.identity) as GameObject;
        RussianRulletBullet rrb = obj.GetComponent<RussianRulletBullet>();
        //set the direction the Bullet will travel in

        Quaternion rot = Quaternion.LookRotation(shootDirection);
        rrb.heading = rot;
    }
}
