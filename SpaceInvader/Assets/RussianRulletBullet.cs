using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RussianRulletBullet : MonoBehaviour {
    public Vector3 thrust;
    public Quaternion heading;
    public enum Property { FreezeBullet, DoublePoint};
    private int randomProperty;
    private System.Random rnd;
    public Renderer rend;

    // Use this for initialization
    void Start () {
        //travel straight in the negative Y-axis downwards to player ship
        thrust.y = -200.0f;

        //do not passively decelerate
        GetComponent<Rigidbody>().drag = 0;

        //set the direction it will travel
        GetComponent<Rigidbody>().MoveRotation(heading);

        GetComponent<Rigidbody>().AddRelativeForce(thrust);

        rnd = new System.Random((int)System.DateTime.Now.Ticks & 0x0000FFFF);

        randomProperty = rnd.Next(0,2);//either 0 or 1
        rend = GetComponent<Renderer>();
        if( randomProperty == (int)(Property.DoublePoint) )
        {
            rend.material.color = Color.green;
        }else
        {
            rend.material.color = Color.red;
        }
        
    }
	
	// Update is called once per frame
	void Update () {
        if (gameObject.transform.position.y < -10)
        {
            Destroy(gameObject);
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        Collider colliderOther = collision.collider;
        if (colliderOther.CompareTag("Playership"))
        {
            PlayerShip playerShip = colliderOther.gameObject.GetComponent<PlayerShip>();
            if (randomProperty== (int)(Property.FreezeBullet))
            {
                playerShip.freezeBullet();
            }else
            {
                playerShip.doublePoint();
            }
            Destroy(gameObject);
            
        }
        else if(colliderOther.CompareTag("RussianRulletGun"))
        {

        }else
        {
            Destroy(gameObject);
        }
    }
}
