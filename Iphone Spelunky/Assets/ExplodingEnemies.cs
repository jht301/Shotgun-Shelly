using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingEnemies : EnemyMovement {
	public float explodingDistance;
	bool readyToBlow;
	public GameObject explosionPrefab;
	Vector3 distToPlayer;
	public float detTimer;



	void Update () {
		
		base.Update ();

		if (player != null) {
			distToPlayer = player.transform.position - transform.position;
			if (Mathf.Abs (distToPlayer.magnitude) < explodingDistance) {
				readyToBlow = true;

			}
		}
	}

	void FixedUpdate(){
		if (!readyToBlow) {
			
			base.FixedUpdate ();

		} else {
			detTimer -= Time.deltaTime;
			if (detTimer <= 0) {
				Instantiate (explosionPrefab, transform.position, Quaternion.identity);
				Destroy (this.gameObject);
			}
		}
	}


	void Hit(){
		ManagerScript.me.scoreInt++;
		readyToBlow = true;
	}
	void OnCollisionEnter2D(Collision2D col){
		if(readyToBlow && col.gameObject.tag != "Bullet"){
			detTimer = 0;
		}
	}
}
