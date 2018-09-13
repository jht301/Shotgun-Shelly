using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    public static EnemyManager me;
	public GameObject explodingEnemyPrefab;
    float numEnemies;
    public GameObject enemyPrefab;
    public float spawnTime;

    public float safetyDistance; 

	// Use this for initialization
	void Start () {
		if(me == null) {
            me = this;
        }
        else {
            Destroy(this);
        }
        StartCoroutine(EnemySpawner());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator EnemySpawner ()
	{
		
		Vector3 spawnPoint = new Vector3 (Random.Range (-2f, 2f), Random.Range (-4.25f, 4.25f));

		yield return 0;

		if ((ManagerScript.me.player.transform.position - spawnPoint).magnitude >= safetyDistance) {
			int i = Random.Range (0, 100);
			if (i < 15) {
				Instantiate (explodingEnemyPrefab, spawnPoint, Quaternion.identity);
			} else {
				Instantiate (enemyPrefab, spawnPoint, Quaternion.identity);
			}
			yield return new WaitForSeconds (spawnTime);
		}
        StartCoroutine(EnemySpawner());
    }


}
