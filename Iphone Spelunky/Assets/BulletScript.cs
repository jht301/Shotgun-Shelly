using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {
	Rigidbody2D rb;
	public float bulletSpeed;
	public float bulletStop;
	public float bulletAccel;
	Vector2 prevVel;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void FixedUpdate(){
		rb.velocity =  ( transform.right * bulletSpeed * Time.fixedDeltaTime);
		bulletSpeed -= bulletAccel;
		if(bulletSpeed<= bulletStop){
			Destroy (gameObject);
		}
		prevVel = rb.velocity;
	
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.layer == LayerMask.NameToLayer ("Enemy")){
			col.gameObject.SendMessage ("Hit", prevVel.normalized, SendMessageOptions.DontRequireReceiver);
			Destroy (gameObject);
		}
		if(col.gameObject.layer == LayerMask.NameToLayer ("Walls")){
			Destroy (gameObject);
		}
	}




}
