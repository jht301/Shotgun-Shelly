using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenShake : MonoBehaviour {
	public float shakeIntensity;
	public float decreaseAmount;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (ManagerScript.me.screenShakeTimer > 0) {
			gameObject.transform.localPosition = Random.insideUnitCircle * shakeIntensity;
			gameObject.transform.position = new Vector3 (transform.position.x, transform.position.y, -10);
			ManagerScript.me.screenShakeTimer -= Time.deltaTime * decreaseAmount;
		}
	}
}
