using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallEgg : MonoBehaviour {

	public float health;
	public int damage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void TakeDamage(int damage){
		health -= damage;
		if(health <= 0){
			Death();
		}
	}

	void Death(){
		Destroy(gameObject);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Player"){
			ShipControl ship = collision.gameObject.GetComponent<ShipControl>();
			ship.TakeDamage(damage);
			Destroy(gameObject);
		}
	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
		Destroy(gameObject);
	}
}
