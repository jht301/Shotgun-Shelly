using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicEnemyMovement : EnemyMovement {
	public float sizeIncrease;
	public ParticleSystem dirt;
	ParticleSystem thisBoy;
	public float timer;
	void Start(){
		base.Start ();
		thisBoy = Instantiate (dirt, transform.position, Quaternion.identity);
	}
	void Update(){
		timer += Time.deltaTime;
		if (transform.localScale.x <= .2f) {
			GetComponent<Collider2D>().enabled = false;
			transform.localScale = transform.localScale + Vector3.one * sizeIncrease;
			dir = Vector3.zero;
		}
		else {
			GetComponent<Collider2D>().enabled = true;
			if (thisBoy != null) {
				thisBoy.Stop ();
			}
			base.Update ();
		}

	}

}
