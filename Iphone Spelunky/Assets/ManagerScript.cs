using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ManagerScript : MonoBehaviour {
	public static ManagerScript me;
	public bool tapped;
	public GameObject player;
	public Button Butt;
	public Text score;
	public int scoreInt;

	public Text bulletsText;
	public int bullets; 
	public int health;

	public float screenShakeTimer;
	public bool usingRemote;

 	// Use this for initialization
	void Awake () {
		if(me == null ){
			me = this;
		}else{
			Destroy ((this));
		}


	}
	
	// Update is called once per frame
	void Update () {
		if(player == null){
	
			Butt.gameObject.SetActive(true);

		}

		score.text = ""+ scoreInt;

		bulletsText.text = "Bullets: " + bullets;


	}
}
