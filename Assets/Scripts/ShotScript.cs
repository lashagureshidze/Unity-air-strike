using UnityEngine;

public class ShotScript : MonoBehaviour
{
	public int damage = 1;
	
	public bool isEnemyShot = false;
	
	void Start()
	{
		// 2 - Limited time to live to avoid any leak
		Destroy(gameObject, 15); // 20sec
	}

	void OnTriggerEnter2D(Collider2D otherCollider) {
		//check bullets intercept
		ShotScript shot = otherCollider.GetComponent<ShotScript> ();
		if (shot != null) {
			//visual effect
			SpecialEffectsHelper.Instance.Explosion(transform.position);
			//sounds effect
			SoundEffectsHelper.Instance.MakeExplosionSound();

			//destroy bullets
			Destroy(otherCollider.gameObject);
			Destroy(gameObject);
		}
	}
}