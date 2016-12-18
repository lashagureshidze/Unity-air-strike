using UnityEngine;
using System.Collections;

public class SpawnBonusItemsScript : MonoBehaviour {

	public float itemsSpawningRate = 10.0f; 
	
	private float itemsSpawnCooldown;
	
	private System.Random random;
	
	void Start() {
		random = new System.Random ();
		itemsSpawnCooldown = itemsSpawningRate;
	}
	
	void FixedUpdate() {
		if (itemsSpawnCooldown > 0)
		{
			itemsSpawnCooldown -= Time.fixedDeltaTime;
			
		}
		if (canSpawn ()) {
			itemsSpawnCooldown = itemsSpawningRate;
			//spawn enemy
			spawnItem();
		}
	}
	
	void spawnItem () {
		
		GameObject itemTransform = Instantiate (Resources.Load("Prefabs/Bonus")) as GameObject;
		float leftBorder = Camera.main.ViewportToWorldPoint (new Vector2(0,0)).x;
		float rightBorder = Camera.main.ViewportToWorldPoint (new Vector2(1,0)).x;
		float topBorder = Camera.main.ViewportToWorldPoint (new Vector2(0,1)).y;
		float bottomBorder = Camera.main.ViewportToWorldPoint (new Vector2(0,0)).y;

		int randomX = random.Next ((int)leftBorder + 1, (int)rightBorder - 1);
		int randomY = random.Next ((int)bottomBorder + 1, (int)topBorder - 1);
		itemTransform.transform.position = new Vector3 (randomX, randomY, transform.position.z);
		
	}
	
	
	private bool canSpawn() {
		return itemsSpawnCooldown <= 0f;
	}
}
