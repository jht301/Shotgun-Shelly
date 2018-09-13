using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionParticleScript : MonoBehaviour {
	public float maxSize;
	public float growSpeed;
	public Color redLight;
	public Color blueLight;
	Light myLight;
	public GameObject bubbles;
	public float intenseGrowth;
	public float rangeGrowth;
	float randomDistFromCenter;
	// Use this for initialization
	void Awake () {
		myLight = GetComponentInChildren<Light> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (transform.localScale.x < maxSize) {
			transform.localScale = new Vector3 (transform.localScale.x + growSpeed, transform.localScale.y + growSpeed, 1);
			ManagerScript.me.screenShakeTimer = .1f;

			myLight.intensity += intenseGrowth;
			myLight.range += rangeGrowth;

		} else {
			ManagerScript.me.screenShakeTimer = 0;
			int numBubbles = Random.Range (4, 10);
			for(int i = 0; i< numBubbles; i++){
				randomDistFromCenter = Random.Range (.1f, 3f);
				GameObject bub = Instantiate (bubbles, transform.position + new Vector3(Random.insideUnitCircle.x, Random.insideUnitCircle.y, 0)* randomDistFromCenter , Quaternion.identity);
				bub.SendMessage ("Color", myLight.color, SendMessageOptions.DontRequireReceiver);
			}
			Destroy (this.gameObject);
		}
	}


	void OnTriggerEnter2D(Collider2D col){
		if (LayerMask.LayerToName (col.gameObject.layer) == "Player") {
			col.gameObject.SendMessage ("Damage", SendMessageOptions.DontRequireReceiver);
			Destroy (gameObject);
		}
		if(LayerMask.LayerToName (col.gameObject.layer) == "Enemy"){
			col.gameObject.SendMessage ("Hit", Vector2.zero, SendMessageOptions.DontRequireReceiver);
		}
	}


	public void Exploding(bool because){
		bool test = because;
		Debug.Log (because);
		if(because){
			
			myLight.color = redLight;
		}
		if(!because){
			myLight.color = blueLight;
		}
	}
}
