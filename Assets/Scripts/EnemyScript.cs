using UnityEngine;

/// Enemy generic behavior
public class EnemyScript : MonoBehaviour
{
	private WeaponScript[] weapons;
	private bool hasSpawn;
	private MoveScript moveScript;
	
	void Awake()
	{
		// Retrieve the weapon only once
		weapons = GetComponentsInChildren<WeaponScript>();

		// Retrieve scripts to disable when not spawn
		moveScript = GetComponent<MoveScript>();
	}

	//disable everything
	void Start() {
		hasSpawn = false;

		GetComponent<Collider2D> ().enabled = false;
		moveScript.enabled = false;
		foreach (WeaponScript weapon in weapons) {
			weapon.enabled = false;
		}
	}
	
	void Update()
	{
		if (! hasSpawn) {
			if (GetComponent<Renderer> ().IsVisibleFrom (Camera.main)) {
				spawn ();
			}
		} else {
			//fire
			foreach (WeaponScript weapon in weapons) {
				// Auto-fire
				if (weapon != null && weapon.enabled && weapon.CanAttack) {
					weapon.Attack (true);
					SoundEffectsHelper.Instance.MakeEnemyShotSound();
				}
			}
		}

		//destroy if out from camera
		if ( ! GetComponent<Renderer> ().IsVisibleFrom (Camera.main)) {
			Destroy(gameObject);
		}
	}

	// 3 - Activate itself.
	private void spawn()
	{
		hasSpawn = true;
		
		// Enable everything
		// -- Collider
		GetComponent<Collider2D>().enabled = true;
		// -- Moving
		moveScript.enabled = true;
		// -- Shooting
		foreach (WeaponScript weapon in weapons)
		{
			weapon.enabled = true;
		}
	}
}