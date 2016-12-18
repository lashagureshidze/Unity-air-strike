using UnityEngine;
using System.Collections;

public class SoundEffectsHelper : MonoBehaviour
{
	
	/// Singleton
	public static SoundEffectsHelper Instance;
	
	public AudioClip explosionSound;
	public AudioClip playerShotSound;
	public AudioClip enemyShotSound;
	public AudioClip buttonSound;
	public AudioClip bonusSound;

	public bool soundEnabled = true;
	
	void Awake()
	{
		// Register the singleton
		if (Instance != null && Instance != this) {
			Debug.Log("reinstanciete SoundEffectHalper");

			Destroy (this.gameObject);

			return;
		} else {
			Instance = this;
		}

		//maintain all in scenes
		DontDestroyOnLoad(gameObject);
	}

	public void MakeBonusSound() {
		if (soundEnabled) MakeSound (bonusSound);
	}
	
	public void MakeExplosionSound()
	{
		if (soundEnabled) MakeSound(explosionSound);
	}
	
	public void MakePlayerShotSound()
	{
		if (soundEnabled) MakeSound(playerShotSound);
	}
	
	public void MakeEnemyShotSound()
	{
		if (soundEnabled) MakeSound(enemyShotSound);
	}

	public void MakeButtonSound()
	{
		MakeSound(buttonSound);
	}
	
	private void MakeSound(AudioClip originalClip)
	{
		AudioSource.PlayClipAtPoint(originalClip, transform.position);
	}
}