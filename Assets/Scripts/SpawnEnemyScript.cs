using UnityEngine;
using System.Collections;

public class SpawnEnemyScript : MonoBehaviour {



	public float enemiesSpawningRate = 4.0f; 
	
	private float enemiesSpawnCooldown;

	private int spawnCounter;

	private System.Random random;
	
	void Start() {
		random = new System.Random ();
		enemiesSpawnCooldown = 0f;
		spawnCounter = 0;
	}
	
	void Update() {
//		Debug.Log ("u");

	}

	void FixedUpdate() {
		if (enemiesSpawnCooldown > 0)
		{
			enemiesSpawnCooldown -= Time.fixedDeltaTime;

		}
		if (canSpawn ()) {
			enemiesSpawnCooldown = enemiesSpawningRate;
			//spawn enemy
			spawnEnemy();
		}
	}

	void spawnEnemy () {
		//make game more hard
		spawnCounter ++;
		if (spawnCounter > 10 && enemiesSpawningRate > 1.0) {
			enemiesSpawningRate -= 0.1f;
		}

		GameObject enemyTransform = Instantiate (Resources.Load(getRandomEnemy())) as GameObject;
		float leftBorder = Camera.main.ViewportToWorldPoint (new Vector2(0,0)).x;
		float rightBorder = Camera.main.ViewportToWorldPoint (new Vector2(1,0)).x;
		int randomX = random.Next ((int)leftBorder + 1, (int)rightBorder - 1);

		var topBorder = Camera.main.ViewportToWorldPoint(
			new Vector2(0, 1)
			).y;
		enemyTransform.transform.position = new Vector3 (randomX, topBorder, transform.position.z);

	}

	private string getRandomEnemy() {
		return "Prefabs/Enemy" + random.Next (1,5);
	}
	
	private bool canSpawn() {
		return enemiesSpawnCooldown <= 0f;
	}
}
