using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    public GameObject player;
    public Rigidbody2D rb;
    public Vector3 dir;
	public ParticleSystem blood;
    public float speed;

	// Use this for initialization
	public void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
		rb = this.gameObject.GetComponent<Rigidbody2D>();

	}
    public void Update() {
		if (player != null) {
			dir = (player.transform.position - transform.position).normalized;
		}
    }
    // Update is called once per frame
    public void FixedUpdate () {
		
        rb.MovePosition(transform.position + dir * speed);


	}
	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.tag == "Player"){
			col.gameObject.SendMessage ("Damage", SendMessageOptions.DontRequireReceiver);
			Destroy (gameObject);
		}
	}
	void Hit(Vector2 bulletDir){
		ManagerScript.me.scoreInt++;
		if (blood != null) {
			Instantiate (blood, transform.position,Quaternion.Euler(new Vector3(-Geo.ToAng(bulletDir),90, 0)));
			blood.Play ();
		}
		Destroy (gameObject);
		}
}
