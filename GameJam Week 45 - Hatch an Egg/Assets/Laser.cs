using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    public float speed;
    ShipControl ship;
    Egg egg;
    public Quaternion rot;

	// Use this for initialization
	void Start () {
        egg = FindObjectOfType<Egg>();
        ship = FindObjectOfType<ShipControl>();
        transform.localRotation = ship.transform.localRotation;
    }
	
	// Update is called once per frame
	void Update () {
        

        transform.position = Vector3.MoveTowards(transform.position, egg.transform.position, speed * Time.deltaTime);



	}

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);    
        
    }
}
    