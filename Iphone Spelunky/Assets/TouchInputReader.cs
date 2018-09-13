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
	public Vector2 [] storedFingerPos = new Vector2[5];
    bool changeDir;
	int frames;
	public float fingerDistanceNumberThing;
	// Use this for initialization
	void Start () {
		
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
			for (int i = 0; i < storedFingerPos.Length; i++) {
				storedFingerPos [i] = point;
			}
			shoot = false;
			initPos = point;
			fingerToPlayer = point - (Vector2)player.position;
			fingerDownTimer = 0;
			timerRunning = true;
		}
    }
	void OnTouchMoved(Vector2 point) {
		
		/*	for (int i = storedFingerPos.Length-1; i > 0; i--) {
			storedFingerPos [i] = storedFingerPos [i - 1];
		}
		for (int x = storedFingerPos.Length-2; x >= 0; x--) {
			
		}*/

		/*if (frames % 10 == 0) {
			storedFingerPos [1] = point;
			Debug.Log (storedFingerPos [1].magnitude - storedFingerPos [0].magnitude);
		}
		if (Mathf.Abs(storedFingerPos [1].magnitude - storedFingerPos [0].magnitude) > fingerDistanceNumberThing) {
			initPos = storedFingerPos [1];
			storedFingerPos [0] = storedFingerPos [1];
		}*/
		storedFingerPos [0] = point;
		for (int i = storedFingerPos.Length-1; i > 0; i--) {
			storedFingerPos [i] = storedFingerPos [i - 1];
		}
		for (int i = 0; i < storedFingerPos.Length; i++) {
		if ((point - storedFingerPos [i]).magnitude > fingerDistanceNumberThing && fingerDownTimer< 10) {
				dir = (point - storedFingerPos [i]);
				break;
			}
		}
		
			
		finPos = point;
		
		changeDir = true;


	}
    void OnTouchUp(Vector2 point) {
		
        finPos = point;
        changeDir = true;
		shoot = true;
		
		timerRunning = false;
    }

	/*void OnMouseDown(){
		player.SendMessage ("Shoot", SendMessageOptions.DontRequireReceiver);
		shoot = false;
	}*/
}
