using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineEnemyScript : MonoBehaviour {

	public float rotationSpeed; 
	public float speed;
	public GameObject explosionCircle;
	Rigidbody2D rb;
	bool readyToBlow;
	GameObject thisExplosionCircle;
	Vector3 moveDirection;
	GameObject bullet;
	bool hitWall;
	public bool blueOrRed;

	public bool why;
	// Use this for initialization
	void Awake () {
		rb = GetComponent <Rigidbody2D> ();
		blueOrRed = false;
	}
	void Update(){
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		

		if(readyToBlow && !hitWall){
			rb.MovePosition (transform.position + moveDirection * speed*Time.deltaTime);
		}
		else{
			rb.MoveRotation (rb.rotation + rotationSpeed*Time.fixedDeltaTime);
		}
		if(hitWall){
			BlowUp ();
		}
	}


	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Player") {
			why = true;
			BlowUp ();

		}

	}
	void OnCollisionEnter2D (Collision2D col){
		if(LayerMask.LayerToName (col.gameObject.layer) == "Walls"){
			hitWall = true;
		}
	}

	void Hit(Vector2 bulletVel){
		
		//play a fizzle particle 
		ManagerScript.me.scoreInt++;
		moveDirection = bulletVel;
		if (bulletVel == Vector2.zero) {
			
			why = true;
			BlowUp ();

		}else {
			readyToBlow = true;
			why  = false;
		}

	}



	void BlowUp(){
		
		thisExplosionCircle = Instantiate (explosionCircle, transform.position, Quaternion.identity);
		//thisExplosionCircle.GetComponent<ExplosionParticleScript> ().Exploding (blueOrRed);

		thisExplosionCircle.gameObject.SendMessage ("Exploding", why, SendMessageOptions.DontRequireReceiver );
		Destroy (this.gameObject);
	}

}
