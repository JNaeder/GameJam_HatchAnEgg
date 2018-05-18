using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControl : MonoBehaviour {

    public float speed;
    public float fireRate;
    public Transform egg;
    public GameObject laser;

    float firedTime, fireWaitTime;

    // Use this for initialization
    void Start () {
        firedTime = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        float h = -Input.GetAxis("Vertical");
        transform.LookAt(egg);
        transform.RotateAround(egg.position, Vector3.forward, speed * Time.deltaTime *h);

        fireWaitTime = 1 / fireRate;


        if (Input.GetMouseButton(0)) {
            if (Time.time >= firedTime + fireWaitTime) {
                
                firedTime = Time.time;
                GameObject firedLaser = Instantiate(laser, transform.position, Quaternion.identity);
            }
        }
	}
}
