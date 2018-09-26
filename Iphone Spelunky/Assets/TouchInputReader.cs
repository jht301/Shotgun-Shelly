using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInputReader : MonoBehaviour {
    public Transform player;
    public Vector3 dir;
    Vector2 initPos;
    Vector2 finPos;
	public Vector3 fingerToPlayer;
	float fingerDownTimer;
	bool timerRunning;
	bool shoot;
	Vector3 [] storage = new Vector3[2];
	float [] fingerVelocityTime = new float[5];
	public Vector2 [] storedPosFinger = new Vector2[5];
	public float fingerVelocity;
    bool changeDir;
	int frames;
	public float fingerDistanceNumberThing;
	public float screenPercentSize;
	public bool usingRemote;
	// Use this for initialization
	void Start () {
	Debug.Log ("start");
	}
	
	// Update is called once per frame
	void Update () {
		frames++;
		if (player != null) { 
			/*if (changeDir) {
				dir = finPos - initPos;
				changeDir = false;
			}*/
			storage [0] = dir;
			storage [1] = fingerToPlayer;
			

			player.SendMessage ("Move", storage, SendMessageOptions.DontRequireReceiver);
        

			if (timerRunning) {
				fingerDownTimer++;
			}

			if (fingerDownTimer < 10 && (initPos - finPos).magnitude <= 0.3f  && shoot) {
			
				player.SendMessage ("Shoot", SendMessageOptions.DontRequireReceiver);
				shoot = false;
			}
		}
	}
    void OnTouchDown(Vector2 point) {
		
		if (player != null) { 
			for (int i = 0; i < storedPosFinger.Length; i++) {
				storedPosFinger [i] = point;
				fingerVelocityTime [i] = 0;
			}
			shoot = false;
			initPos = point;
			fingerToPlayer = point - (Vector2)player.position;
			fingerDownTimer = 0;
			timerRunning = true;
		}

    }



//	void OnTouchStay (Vector2 point)
//	{
//		Debug.Log ("stay");
//
//
//		finPos = point;
//		if ((finPos - initPos).magnitude > fingerDistanceNumberThing) {
//			dir = finPos - initPos;
//		}
//		changeDir = true;
//		initPos = point;
//
//
//	}



//	void OnTouchMoved (Vector2 point)
//	{
//		
//		/*	for (int i = storedPosFinger.Length-1; i > 0; i--) {
//			storedPosFinger [i] = storedPosFinger [i - 1];
//		}
//		for (int x = storedPosFinger.Length-2; x >= 0; x--) {
//			
//		}*/
//
//		/*if (frames % 10 == 0) {
//			storedPosFinger [1] = point;
//			Debug.Log (storedPosFinger [1].magnitude - storedPosFinger [0].magnitude);
//		}
//		if (Mathf.Abs(storedPosFinger [1].magnitude - storedPosFinger [0].magnitude) > fingerDistanceNumberThing) {
//			initPos = storedPosFinger [1];
//			storedPosFinger [0] = storedPosFinger [1];
//		}*/
//
////		Vector2 loc1 = point;
////		float time1 = fingerDownTimer;
//
//		storedPosFinger [0] = point; 
//		fingerVelocityTime [0] = fingerDownTimer;
//
//		for (int i = storedPosFinger.Length - 1; i > 0; i--) {
//			
//			
//			storedPosFinger [i] = storedPosFinger [i - 1];
//			fingerVelocityTime [i] = fingerVelocityTime [i - 1];
//		}
//		for (int i = 0; i < storedPosFinger.Length; i++) {
//			float swipeDistance;
//			if (usingRemote) {
//				 swipeDistance = (point - storedPosFinger [i]).magnitude;
//			} else {
//				 swipeDistance = (point - storedPosFinger [i]).magnitude/ Screen.dpi;
//			}
//			Debug.Log (swipeDistance);
//			if (swipeDistance > fingerDistanceNumberThing && swipeDistance / fingerVelocityTime [i] >= fingerVelocity) {
//				Debug.Log ("direction Change"); 
//				dir = (point - storedPosFinger [i]);
//				if (player != null) {
//					player.SendMessage ("Move", storage, SendMessageOptions.DontRequireReceiver);
//				}
//
//				
//				fingerDownTimer = 0;
//				break;
//			}
//		}
//	}
    void OnTouchUp (Vector2 point)
	{
		
		finPos = point;
		if ((finPos - initPos).magnitude > fingerDistanceNumberThing) {
			dir = finPos - initPos;
		}
		changeDir = true;
		shoot = true;
		
		timerRunning = false;
		if (player != null) {
			player.SendMessage ("BoostReset", SendMessageOptions.DontRequireReceiver);
		}
    }

	/*void OnMouseDown(){
		player.SendMessage ("Shoot", SendMessageOptions.DontRequireReceiver);
		shoot = false;
	}*/
}
