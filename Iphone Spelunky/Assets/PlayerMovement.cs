using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    Vector3 dir;
    Rigidbody2D rb;
    public float speed;
    Vector3 storedDir;
    bool shoot;
	public float setShootDelay;
	public float shootDelay;
	public float numBullets;
	public GameObject bulletPrefab;
	Vector3 fingerToPlayer;
	public float shotgunOffset;
	//float[] bullSpawnPos;
	//float[] bullSpawnAng;
	float bullSpawnPos;
	float bullSpawnAng;
	bool reloading;
	bool reloaded;
	public float velBoost;
	public int magSize;
	GameObject[] bulletIndicators;
	float damageDelay;
	public float setDamageDelay;
	public float speedBoost;
	public float flashLength;
    public int something;
     int flashTimer;
    bool dead;
    public bool boosted; 
	public float shootShaking;
	public float damageShaking;
    SpriteRenderer spr;
    Color reg;
	public GameObject bulletVisHolder;
	public float boostDeceleration;

	public AudioClip shootingSound, reloadingSound;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
		shootDelay = setShootDelay;
		reloading = true;

		//for(int z = 0; z< bulletIndicators.Length; z++){
			//bulletIndicators [z] = GetComponentInChildren<GameObject> ("");
		//}

		spr = GetComponent<SpriteRenderer>();
        flashTimer = 999;
        reg = spr.color;
		//bullSpawnPos = new float[numBullets];
		//bullSpawnAng = new float[numBullets];
	}


	void Update ()
	{
		//Sets damage delay and flashing
		if (gameObject != null) { 
//			if (damageDelay > 0) {
//				damageDelay -= Time.deltaTime;
//				if (flashTimer % (something * 2) >= something) {
//					spr.color = Color.black;
//				} else {
//					spr.color = Color.white;
//				}
//				flashTimer++;
//			} else {
//				spr.color = reg;
//			}
			

		//Manages delay when reloading
			if (shootDelay >= 0 && reloading) {
				
				shootDelay -= Time.deltaTime;

			} else if (shootDelay <= 0 && reloading) {         // refils bullets
				ManagerScript.me.bullets = magSize;
				bulletVisHolder.GetComponent<BulletVis>().Reloaded();
				reloading = false;
			}
				

		//kills player when died and kills all running functions
			if (ManagerScript.me.health <= 0) {
				Destroy (gameObject);
				EnemyManager.me.StopAllCoroutines ();
			}
		}
	}

	/*void OnTouchMoved(Vector2 point)
	{
		dir = point;
	}*/


    void FixedUpdate ()
	{
		if (gameObject != null) { 
			if (velBoost > 1) {
				velBoost -= boostDeceleration;

			} else {
				velBoost = 1;
			}
			if (dir.x != 0 || dir.y != 0) {
				if (!boosted && dir != storedDir) {
					velBoost = speedBoost;
					boosted = true;
				} 
				storedDir = dir;
				rb.rotation = Geo.ToAng (dir.normalized) ;
				rb.MovePosition (transform.position + dir * velBoost *speed * Time.fixedDeltaTime);
			} else {
				rb.MovePosition (transform.position + storedDir * velBoost* speed * Time.fixedDeltaTime);
				rb.rotation = Geo.ToAng (storedDir.normalized) ;
				shoot = true;
			}
		}
	}

	void Move(Vector3 [] stor) {
		
		dir = stor[0].normalized;
		fingerToPlayer = stor[1];
    }
	void Shoot(){
		
		if (shootDelay <= 0 && ManagerScript.me.bullets >0) {
			for(int i =0; i<numBullets; i++){
				//bullSpawnAng [i] = (i / numBullets) * 30*((-1)^i);
				//bullSpawnPos [i] = (i / numBullets) * 50f*((-1)^i);
				bullSpawnAng  = (i / numBullets) * 20f * (Mathf.Pow (-1,i));
				bullSpawnPos  = (i / numBullets) * .3f*(Mathf.Pow (-1,i));

				Instantiate (bulletPrefab, transform.position +storedDir*shotgunOffset+ Geo.PerpVect(storedDir,true)*bullSpawnPos, Quaternion.Euler(0,0,Geo.ToAng (storedDir)- bullSpawnAng));

			}
			SoundManager.me.Play (shootingSound);
			ManagerScript.me.bullets -= 1;
			ManagerScript.me.screenShakeTimer = shootShaking;
			bulletVisHolder.SendMessage ("Shot", null, SendMessageOptions.DontRequireReceiver);

			if (ManagerScript.me.bullets <= 0) {
				reloading = true;
				SoundManager.me.Play (reloadingSound);
				shootDelay = setShootDelay;

			}

		}
	}

	void Damage ()
	{
		if (damageDelay <= 0) {
			ManagerScript.me.screenShakeTimer = damageShaking;
			ManagerScript.me.health--;
			damageDelay = setDamageDelay;
			flashTimer = 0;
		}

	}

	void BoostReset ()
	{
		boosted = false;
	}
}

