using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class RestartScript : MonoBehaviour {

	Button btn;
	// Use this for initialization
	void Start () {
		btn = gameObject.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void TaskOnClick(){
		
		SceneManager.LoadScene (0);
		ManagerScript.me.Butt.gameObject.SetActive (false);
		ManagerScript.me.scoreInt = 0;
	}
}
