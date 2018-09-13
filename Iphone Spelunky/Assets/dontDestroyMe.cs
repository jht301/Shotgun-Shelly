using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dontDestroyMe : MonoBehaviour {
	public static dontDestroyMe me;
	// Use this for initialization
	void Start () {
		if (me == null) {
			me = this;

		}else{
			Destroy (this.gameObject);
		}
		DontDestroyOnLoad (this.gameObject);
	}
	

}
