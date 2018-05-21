using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;

public class Egg : MonoBehaviour {
    public GameManager gameManager;

	public float health;

	public Transform[] enemySpawnPoints;
	public GameObject enemy, smallEgg;

	public Transform eggSpawnTrans;


	float startHealth, newHealthBarSize, startHealthBarSize,healthRatio;
	public Image healthBar;
	Vector3 healthBarSize;

	public Animator movementAnim, enemyAnim;

	SpriteRenderer sP;
	public Sprite hatchedEggSprite, crackedEgg_1, crackedEgg_2;
	public GameObject finalExplosion;

	public static int enemyNum;
	bool isHiding, isDead;
	bool stage1, stage2, stage3, stage4, stage5, stage6;

    [FMODUnity.EventRef]
    public string eggEnterVO, eggExitVO, explosionSound;

	// Use this for initialization
	void Start () {
		sP = GetComponent<SpriteRenderer>();

		startHealth = health;

		healthBarSize = healthBar.transform.localScale;
		newHealthBarSize = healthBarSize.x;
		healthRatio = health / startHealth;


		movementAnim.SetBool("isHiding", isHiding);
	}
	
	// Update is called once per frame
	void Update () {
		AdjustHealthBar();
		BossStages();


	}


	public void TakeDamage(int damage){
		if (!isDead)
		{
			health -= damage;
			healthRatio = health / startHealth;
			newHealthBarSize = healthBarSize.x * healthRatio;
			enemyAnim.Play("Hit");
		}


	}


	void AdjustHealthBar (){
		Vector3 localScale = healthBar.transform.localScale;
        localScale.x = newHealthBarSize;
        healthBar.transform.localScale = localScale;
	}

	void Death(){
		CancelInvoke();
		isDead = true;
		enemyAnim.Play("Still");
		FMODUnity.RuntimeManager.PlayOneShot(explosionSound, transform.position);
		sP.sprite = hatchedEggSprite;
		Vector3 explosionPos = new Vector3(transform.position.x, transform.position.y, -10);
		GameObject eggsplosion = Instantiate(finalExplosion, explosionPos, Quaternion.identity);
		Destroy(eggsplosion, 5f);
        gameManager.WinGame();
	}




	void BossStages(){
		//Stage 1
		if(healthRatio == 1.0f){
			if (!stage1)
			{
				movementAnim.Play("Enter_1");
				InvokeRepeating("SpawnEggs", 0.01f, 1.5f);
				stage1 = true;
                FMODUnity.RuntimeManager.PlayOneShot(eggEnterVO, transform.position);
            }

		}

        //Stage 2
		if (healthRatio <= 0.80f)
        {
			if (!stage2)
			{
				if (!isHiding)
				{
					CancelInvoke();
					isHiding = true;
                    FMODUnity.RuntimeManager.PlayOneShot(eggExitVO, transform.position);
                    movementAnim.SetBool("isHiding", isHiding);
					stage2 = true;
					SpawnEnemies(2);
					enemyNum = 2;
				}
			}


			if (stage2)
			{
				if (enemyNum == 0)
				{
					if (isHiding)
					{
                        FMODUnity.RuntimeManager.PlayOneShot(eggEnterVO, transform.position);
                        isHiding = false;
						movementAnim.SetBool("isHiding", isHiding);
						InvokeRepeating("SpawnEggs", 0.01f, 1f);
					}
				}
			}
        }

        //Stage 3
		if (healthRatio <= 0.60f)
        {
			if (!stage3)
            {
                if (!isHiding)
                {
					sP.sprite = crackedEgg_1;
                    enemyAnim.SetInteger("Progress", 1);
                    CancelInvoke();
                    isHiding = true;
                    FMODUnity.RuntimeManager.PlayOneShot(eggExitVO, transform.position);
                    movementAnim.SetBool("isHiding", isHiding);
                    stage3 = true;
                    SpawnEnemies(3);
                    enemyNum = 3;
                }
            }


            if (stage3)
            {
                if (enemyNum == 0)
                {
                    if (isHiding)
                    {
                        FMODUnity.RuntimeManager.PlayOneShot(eggEnterVO, transform.position);
                        isHiding = false;
                        movementAnim.SetBool("isHiding", isHiding);
                        InvokeRepeating("SpawnEggs", 0.01f, .5f);
                    }
                }
            }

        }


        //Stage 4
		if (healthRatio <= 0.45f)
        {
			if (!stage4)
            {
                if (!isHiding)
                {
					CancelInvoke();
                    isHiding = true;
                    FMODUnity.RuntimeManager.PlayOneShot(eggExitVO, transform.position);
                    movementAnim.SetBool("isHiding", isHiding);
                    stage4 = true;
                    SpawnEnemies(4);
                    enemyNum = 4;
                }
            }


            if (stage4)
            {
                if (enemyNum == 0)
                {
                    if (isHiding)
                    {
                        FMODUnity.RuntimeManager.PlayOneShot(eggEnterVO, transform.position);
                        isHiding = false;
                        movementAnim.SetBool("isHiding", isHiding);
						InvokeRepeating("SpawnEggs", 0.01f, .25f);
                    }
                }
            }

        }


        //Stage 5
		if (healthRatio <= 0.35f)
        {
			if (!stage5)
            {
                if (!isHiding)
                {
					CancelInvoke();
                    isHiding = true;
                    FMODUnity.RuntimeManager.PlayOneShot(eggExitVO, transform.position);
                    movementAnim.SetBool("isHiding", isHiding);
					sP.sprite = crackedEgg_2;
                    enemyAnim.SetInteger("Progress", 2);
                    stage5 = true;
                    SpawnEnemies(5);
                    enemyNum = 5;
                }
            }


            if (stage5)
            {
                if (enemyNum == 0)
                {
                    if (isHiding)
                    {
                        FMODUnity.RuntimeManager.PlayOneShot(eggEnterVO, transform.position);
                        isHiding = false;
                        movementAnim.SetBool("isHiding", isHiding);
                        InvokeRepeating("SpawnEggs", 0.01f, 0.1f);
                        
                    }
                }
            }

        }


        //Stage 6
		if (healthRatio <= 0.10f)
        {
            
			if (!stage6)
            {
                if (!isHiding)
                {
					CancelInvoke();
                    isHiding = true;
                    FMODUnity.RuntimeManager.PlayOneShot(eggExitVO, transform.position);
                    movementAnim.SetBool("isHiding", isHiding);
                    stage6 = true;
                    SpawnEnemies(6);
                    enemyNum = 6;
                }
            }


            if (stage6)
            {
                if (enemyNum == 0)
                {
                    if (isHiding)
                    {
                        FMODUnity.RuntimeManager.PlayOneShot(eggEnterVO, transform.position);
                        isHiding = false;
                        movementAnim.SetBool("isHiding", isHiding);
						InvokeRepeating("SpawnEggs", 0.01f, 0.01f);
                       
                    }
                }
            }
        }


		//Stage 7
        if (healthRatio <= 0)
        {
			if (!isDead)
			{
				Death();
			}

        }



	}


	void SpawnEnemies(int numberOfEnemies){

		for (int i = 0; i < numberOfEnemies; i++){
			Instantiate(enemy, enemySpawnPoints[i].position, Quaternion.identity);
		}
	}



	public void SpawnEggs(){
		GameObject spawnedEgg = Instantiate(smallEgg, eggSpawnTrans.position, Quaternion.identity);
		Rigidbody2D eggRB = spawnedEgg.GetComponent<Rigidbody2D>();
		Vector2 eggDirection = new Vector2(Random.Range(-7,7), Random.Range(2,5));
		eggRB.AddForce(eggDirection, ForceMode2D.Impulse);
		eggRB.AddTorque(Random.Range(10, 100));
	}
}
