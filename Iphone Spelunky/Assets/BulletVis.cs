using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletVis : MonoBehaviour {
	public SpriteRenderer[] bullets;
	int startingBullets;
	// Use this for initialization
	void Start () {
		bullets = new SpriteRenderer[ManagerScript.me.bullets];
		startingBullets = ManagerScript.me.bullets;
		bullets = GetComponentsInChildren<SpriteRenderer> ();

	}
	
	public void Shot(){
		for(int i = 0; i< startingBullets -ManagerScript.me.bullets; i++){

			bullets [i].enabled = false;
		}
	}


	public void Reloaded(){
		for(int x = 0; x< ManagerScript.me.bullets; x++){
			bullets [x].enabled = true;
		}
	}
}
