using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour {
    public float scaleNum;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Laser") {
            transform.localScale *= scaleNum;
        }
    }
}
