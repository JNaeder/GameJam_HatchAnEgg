using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;

public class ShipControl : MonoBehaviour {

    public GameManager gameManager;

    public float speed;
    public float fireRate;
	public float health;
    public Transform egg;
    public GameObject laser;


	public float shootingCapacityNum;
	public Image shootingChargeMeter, healthBar;
	float newShootChargeSize, startShootCap, startShootChargeSize, shootChargeRatio;   
    Vector3 shootChargeSize;
	float startHealth, newHealthBarSize, startHealthBarSize, healthRatio;
	Vector3 healthBarSize;


    float firedTime, fireWaitTime;

    Animator anim;

    [FMODUnity.EventRef]
    public string fireLaserSound, shootingChargeSound, playerHitSound;

    FMOD.Studio.EventInstance chargingSound;


    // Use this for initialization
    void Start () {
        firedTime = Time.time;
		startShootCap = shootingCapacityNum;
		shootChargeSize = shootingChargeMeter.transform.localScale;
		startShootChargeSize = shootChargeSize.x;
		newShootChargeSize = shootChargeSize.x;
		startHealth = health;      
        healthBarSize = healthBar.transform.localScale;
		startHealthBarSize = healthBar.transform.localScale.x;
        newHealthBarSize = healthBarSize.x;
        healthRatio = health / startHealth;

        anim = GetComponent<Animator>();

        chargingSound = FMODUnity.RuntimeManager.CreateInstance(shootingChargeSound);
        chargingSound.setParameterValue("isShooting", 0);
        chargingSound.start();
    }
	
	// Update is called once per frame
	void Update () {
		shootChargeRatio = shootingCapacityNum / startShootCap;

		Movement();
		AdjustShootChargeMeter();
		Shooting();
		AdjustHealthBar();
      




	}



	void Movement(){
		Vector3 shipPos = transform.position;
		float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        shipPos += new Vector3(speed * Time.deltaTime * h, speed * Time.deltaTime * v, 0);
        shipPos.x = Mathf.Clamp(shipPos.x, -23f, 23f);
        shipPos.y = Mathf.Clamp(shipPos.y, -18.7f, -7.6f);
		transform.position = shipPos;
	}


	void AdjustShootChargeMeter(){
		Vector3 shootCapScale = shootingChargeMeter.transform.localScale;
        newShootChargeSize = Mathf.Lerp(shootCapScale.x, startShootChargeSize * shootChargeRatio, 0.1f);
        shootCapScale.x = newShootChargeSize;
        shootingChargeMeter.transform.localScale = shootCapScale;
        chargingSound.setParameterValue("ChargeRatio", shootChargeRatio);
	}
	void AdjustHealthBar()
    {
        Vector3 localScale = healthBar.transform.localScale;
		newHealthBarSize = Mathf.Lerp(localScale.x, startHealthBarSize * healthRatio, 0.1f);
        localScale.x = newHealthBarSize;
        healthBar.transform.localScale = localScale;
    }

	void Shooting(){
		fireWaitTime = 1 / fireRate;
        if (Input.GetKey(KeyCode.Space))
        {
            chargingSound.setParameterValue("isShooting", 0);
            if (shootingCapacityNum > 0)
            {
                if (Time.time >= firedTime + fireWaitTime)
                {               
                    firedTime = Time.time;
                    shootingCapacityNum--;
                    Instantiate(laser, transform.position, Quaternion.identity);
                    FMODUnity.RuntimeManager.PlayOneShot(fireLaserSound, transform.position);
                }
            }
        }
        else
        {

            if (shootingCapacityNum < startShootCap)
            {
                chargingSound.setParameterValue("isShooting", 1);
                if (Time.time >= firedTime + fireWaitTime / 2)
                {
                    firedTime = Time.time;
                    shootingCapacityNum++;
                }
            }
            else {
                chargingSound.setParameterValue("isShooting", 0);
            }
        }
	}



	public void TakeDamage(int damage){
		if (health > 0)
		{
			health -= damage;
			healthRatio = health / startHealth;
			newHealthBarSize = healthBarSize.x * healthRatio;
            FMODUnity.RuntimeManager.PlayOneShot(playerHitSound, transform.position);
            anim.Play("Hit");
		}

		if(health <= 0){
			Death();
		}
	}


	void Death(){
        gameManager.LoseGame();
		print("Game Over!");
        Destroy(gameObject);
	}



}
