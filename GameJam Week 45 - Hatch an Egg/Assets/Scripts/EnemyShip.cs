using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class EnemyShip : MonoBehaviour {

	Transform ship;
	public float speed, health;
    public GameObject laser, explosion;
	public Transform nozzle;

	float firedTime, fireWaitTime, fireRate;

	bool canShoot;

    [FMODUnity.EventRef]
    public string laserFireSound, explodeSound;


	// Use this for initialization
	void Start () {
		ship = FindObjectOfType<ShipControl>().transform;
		fireRate = Random.Range(0.7f, 1.5f);
        //Egg.enemyNum++;

	}
	
	// Update is called once per frame
	void Update () {
        if (ship != null)
        {
            LookAtPlayer();
            transform.Translate(Vector3.up * Time.deltaTime * speed, Space.Self);

            Shoot();
        }
	}



	void LookAtPlayer(){
		Vector3 norTar = (ship.position - transform.position).normalized;
        float angle = Mathf.Atan2(norTar.y, norTar.x) * Mathf.Rad2Deg;
        Quaternion rotation = new Quaternion();
        rotation.eulerAngles = new Vector3(0, 0, angle - 90);
        transform.rotation = rotation;
	}


	void Shoot(){

		if (canShoot)
		{
			fireWaitTime = 1 / fireRate;
			if (Time.time >= firedTime + fireWaitTime)
			{
				firedTime = Time.time;
				GameObject firedLaser = Instantiate(laser, nozzle.position, Quaternion.identity);
				firedLaser.transform.rotation = transform.rotation;
                FMODUnity.RuntimeManager.PlayOneShot(laserFireSound, transform.position);

			}
		}
	}

	public void TakeDamage(float damage){
		health -= damage;
		if(health <= 0){
			Death();
		}
	}

	void Death(){
		Egg.enemyNum--;
        FMODUnity.RuntimeManager.PlayOneShot(explodeSound, transform.position);
        GameObject explosionInst = Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(explosionInst, 1.0f);
        Destroy(gameObject);
	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
		canShoot = true;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Player"){
			ShipControl Playership = collision.gameObject.GetComponent<ShipControl>();
			Playership.TakeDamage(2);
			Egg.enemyNum--;
			Destroy(gameObject);
		}
	}
}
