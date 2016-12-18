using UnityEngine;

public class HealthScript : MonoBehaviour
{
	public int hp = 1;
	
	public bool isEnemy = true;
	
	public void Damage(int damageCount)
	{
		hp -= damageCount;

		//visual effect
		SpecialEffectsHelper.Instance.Explosion(transform.position);
		//sounds effect
		SoundEffectsHelper.Instance.MakeExplosionSound();
		
		if (hp <= 0)
		{
			//score
			if (isEnemy) {
				ScoreScript[] scoreScripts = FindObjectsOfType(typeof(ScoreScript)) as ScoreScript[];
				foreach (ScoreScript script in scoreScripts) {
					script.increase(10);
				}
			} else {
				//end game
				Time.timeScale = 0;
				
				Transform popUp = Instantiate(Resources.Load("Prefabs/EndPopUp")) as Transform;


			}

			// Dead!
			Destroy(gameObject);
		}
	}
	
	void OnTriggerEnter2D(Collider2D otherCollider)
	{
		// Is this a shot?
		ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript>();
		if (shot != null)
		{
			// Avoid friendly fire
			if (shot.isEnemyShot != isEnemy)
			{
				Damage(shot.damage);
				
				// Destroy the shot
				Destroy(shot.gameObject); // Remember to always target the game object, otherwise you will just remove the script
			}
		}
	}
}