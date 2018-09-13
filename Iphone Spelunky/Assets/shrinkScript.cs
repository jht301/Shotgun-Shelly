using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shrinkScript : MonoBehaviour {
	public float minSize;
	public float growSpeed;
	 SpriteRenderer spr;

	// Use this for initialization
	void Awake () {
		spr = GetComponent<SpriteRenderer> ();
		float randomScale = Random.Range (.1f, .5f);
		transform.localScale = new Vector3 (randomScale, randomScale, 1);
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.localScale.x > minSize) {
			transform.localScale = new Vector3 (transform.localScale.x - growSpeed, transform.localScale.y - growSpeed, 1);
		}else{
			Destroy (this.gameObject);
		}
	}

	void Color(Color ths){
		spr.color = ths;

	}
}
