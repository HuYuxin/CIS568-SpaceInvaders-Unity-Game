using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipBullet : MonoBehaviour {
    public Vector3 thrust;
    public Quaternion heading;

	// Use this for initialization
	void Start () {
        //travel straight in the Z-axis
        thrust.z = 600.0f;

        //do not passively decelerate
        GetComponent<Rigidbody>().drag = 0;

        //set the direction it will travel
        GetComponent<Rigidbody>().MoveRotation(heading);

        GetComponent<Rigidbody>().AddRelativeForce(thrust);
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Camera.main.WorldToScreenPoint(gameObject.transform.position).y > Screen.height + 10)
        {
            Destroy(gameObject);
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;
        if(collider.CompareTag("AlienShipA"))
        {
            AlienShipA alienShipA = collider.gameObject.GetComponent<AlienShipA>();
            alienShipA.Die();
            Destroy(gameObject);
        }
        else if (collider.CompareTag("AlienShipB"))
        {
            AlienShipB alienShipB = collider.gameObject.GetComponent<AlienShipB>();
            alienShipB.Die();
            Destroy(gameObject);
        }
        else if (collider.CompareTag("AlienShipC"))
        {
            AlienShipC alienShipC = collider.gameObject.GetComponent<AlienShipC>();
            alienShipC.Die();
            Destroy(gameObject);
        }
        else if (collider.CompareTag("Fortress"))
        {
            //Debug.Log("Collieded with Fortress!!");
            Fortress fortress = collider.gameObject.GetComponent<Fortress>();
            fortress.Die();
            Destroy(gameObject);
        }else if(collider.CompareTag("UFO"))
        {
            UFOScript ufo = collider.gameObject.GetComponent<UFOScript>();
            ufo.Die();
            Destroy(gameObject);
        }
        else if (collider.CompareTag("AlienBoss"))
        {
            AlienBoss ab = collider.gameObject.GetComponent<AlienBoss>();
            ab.Die();
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
