using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    public float speed;
	public int damage;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.up * speed * Time.deltaTime);




	}

    private void OnCollisionEnter2D (Collision2D collision)
    {
        Destroy(gameObject);
		if (collision.gameObject.tag == "Egg")
		{
			//print("Egg Hit");
			Egg egg = collision.gameObject.GetComponent<Egg>();
			egg.TakeDamage(damage);
		}
		if (collision.gameObject.tag == "Enemy")
        {
			//print("Enemy Hit");
			EnemyShip enemy = collision.gameObject.GetComponent<EnemyShip>();
			enemy.TakeDamage(damage);
        }
		if (collision.gameObject.tag == "SmallEgg")
        {
			SmallEgg smallEgg = collision.gameObject.GetComponent<SmallEgg>();
			smallEgg.TakeDamage(damage);
        }
        
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Destroy(gameObject);
		//print("Laser Destroy");
	}
}
    