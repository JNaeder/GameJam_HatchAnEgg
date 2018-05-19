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
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 shipPos = transform.position;

        shipPos += new Vector3(speed * Time.deltaTime * h, speed * Time.deltaTime * v, 0);
        shipPos.x = Mathf.Clamp(shipPos.x, -23f, 23f);
        shipPos.y = Mathf.Clamp(shipPos.y, -18.7f, -7.6f);

        transform.position = shipPos;

        fireWaitTime = 1 / fireRate;


        if (Input.GetKey(KeyCode.Space)) {
            if (Time.time >= firedTime + fireWaitTime) {
                
                firedTime = Time.time;
                GameObject firedLaser = Instantiate(laser, transform.position, Quaternion.identity);
            }
        }
	}
}
