using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienShipBullet : MonoBehaviour {
    public Vector3 thrust;
    public Quaternion heading;
    // Use this for initialization
    void Start () {
        //travel straight in the -Z-axis, downwards
        thrust.z = -500.0f;

        //do not passively decelerate
        GetComponent<Rigidbody>().drag = 0;

        //set the direction it will travel
        GetComponent<Rigidbody>().MoveRotation(heading);

        GetComponent<Rigidbody>().AddRelativeForce(thrust);
    }
	
	// Update is called once per frame
	void Update () {
		if(Camera.main.WorldToScreenPoint(gameObject.transform.position).y < -10)
        {
            Destroy(gameObject);
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;
        if (collider.CompareTag("Playership"))
        {
            PlayerShip playerShip = collider.gameObject.GetComponent<PlayerShip>();
            playerShip.Die();
            Destroy(gameObject);
        }
        else if (collider.CompareTag("Fortress"))
        {
            //Debug.Log("Collieded with Fortress!!");
            Fortress fortress = collider.gameObject.GetComponent<Fortress>();
            fortress.Die();
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
